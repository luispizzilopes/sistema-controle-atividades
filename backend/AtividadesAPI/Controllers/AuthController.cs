using AtividadesAPI.Context;
using AtividadesAPI.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AtividadesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IConfiguration configuration, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = configuration;
            _context = context;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "AuthController :: Acessado em : " + DateTime.Now.ToString();
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] NovoUsuarioDTO model)
        {
            var user = new IdentityUser
            {
                UserName = model.Nome,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(user, false);
            return Ok(await GeraToken(new UsuarioDTO { Email = model.Email, Password = model.Password }));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UsuarioDTO userInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok(await GeraToken(userInfo));
            }
            else
            {
                ModelState.AddModelError("Erro", "Verifique as credenciais informadas e tente novamente!");
                return BadRequest(ModelState);
            }
        }

        private async Task<UsuarioTokenDTO> GeraToken(UsuarioDTO userInfo)
        {
            //define declarações do usuário
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim("meuPet", "pipoca"),
                new Claim("meuPet", "mel"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //gera uma chave com base em um algoritmo simetrico
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:key"]!));

            //gera uma assinatura digital do token usando o algoritmo hmac e a chave privada
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Tempo de expiração do token
            var expiracao = _config["TokenConfiguration:ExpireHours"]!;
            var expiration = DateTime.Now.AddHours(double.Parse(expiracao));

            //Classe que representa o token e gera o token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["TokenConfiguration:Issuer"],
                audience: _config["TokenConfiguration:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credenciais);

            //retorna os dados com o token e informações
            return new UsuarioTokenDTO()
            {
                Id = await _context.Users.Where(u => u.Email == userInfo.Email).Select(u => u!.Id).FirstOrDefaultAsync(), 
                Name = await _context.Users.Where(u => u.Email == userInfo.Email).Select(u => u!.UserName).FirstOrDefaultAsync(),
                Email = userInfo.Email,
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Token JWT OK"
            };
        }
    }
}

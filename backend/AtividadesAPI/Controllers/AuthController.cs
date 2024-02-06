using AtividadesAPI.Context;
using AtividadesAPI.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

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
            bool verificaEmailExistente = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email) != null ? true : false;

            if(verificaEmailExistente == false)
            {
                var user = new IdentityUser
                {
                    UserName = model.Nome,
                    Email = model.Email,
                    EmailConfirmed = false
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                var resultConfirmEmail = await EnviarEmailConfirmacaoRegistro(model.Email);

                if (!result.Succeeded || !resultConfirmEmail)
                {
                    return BadRequest(result.Errors);
                }

                await _signInManager.SignInAsync(user, false);
                return Ok(await GeraToken(new UsuarioDTO { Email = model.Email, Password = model.Password }));
            }

            ModelState.AddModelError("Erro", "E-mail já registrado no sistema!");
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UsuarioDTO userInfo)
        {
            var isEmail = VerificarEmail(userInfo.Email);

            var emailUser = isEmail
                ? await _userManager.FindByEmailAsync(userInfo.Email)
                : await _userManager.FindByNameAsync(userInfo.Email);

            var result = await _signInManager.PasswordSignInAsync(emailUser!, userInfo.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                bool emailConfirmed = await _context.Users.Where(u => u.Email == emailUser!.Email).Select(e => e.EmailConfirmed).FirstOrDefaultAsync();

                if (emailConfirmed)
                {
                    return Ok(await GeraToken(userInfo));
                }

                ModelState.AddModelError("Erro", "Confirme seu e-mail e tente novamente!");
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == emailUser!.Email);

            if (user != null)
            {
                user.AccessFailedCount = user.AccessFailedCount + 1;
                _context.Users.Entry(user).State = EntityState.Modified; 

                await _context.SaveChangesAsync();
            }
                
            ModelState.AddModelError("Erro", "Verifique as credenciais informadas e tente novamente!");
            return BadRequest(ModelState);
        }

        [HttpGet("validar-token/{token}/{email}")]
        public async Task<ActionResult> ValidarTokenConfirmacaoEmail([FromRoute] string token, [FromRoute] string email)
        {
            if (ValidarTokenConfirmacaoEmail(token))
            {
                try
                {
                    string emailDescriptografado = Descriptografar(email);

                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == emailDescriptografado);
                    user!.EmailConfirmed = true;

                    _context.Users.Entry(user).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return Ok("Email verificado com sucesso!");
                }
                catch
                {
                    return BadRequest("Ocorreu um erro ao validar o seu email");
                }
            }

            return BadRequest("Link de validação invalido!");
        }

        private async Task<bool> EnviarEmailConfirmacaoRegistro(string email)
        {
            try
            {
                string smtpHost = "smtp.gmail.com";
                int smtpPort = 587;
                string smtpUsername = "sistema.extendfile@gmail.com";
                string smtpPassword = "iepkdbxhrfqtavzc ";
        
                string senderEmail = email;
                string subject = "Confirmação de Registro - eXtend File";

                string linkConfirmacao =
                    $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/Auth/validar-token/{GeraTokenConfirmacaoEmail(email)}/{Criptografar(email)}";

                string body = $@"
                    <!DOCTYPE html>
                    <html lang=""pt-br"">
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                        <title>Confirmação de Registro</title>
                        <style>
                            @import url('https://fonts.googleapis.com/css2?family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;1,100;1,300;1,400;1,500;1,700&display=swap');

                            body {{
                                font-family: 'Roboto', sans-serif;
                                background-color: #f4f4f4;
                                margin: 0;
                                padding: 0;
                                display: flex;
                                flex-direction: column;
                                justify-content: center;
                                align-items: center;
                                width: 100%;
                                height: 100vh;
                            }}

                            .container {{
                                text-align: center;
                                max-width: 400px;
                                display: flex;
                                flex-direction: column;
                                justify-content: center;
                                align-items: center;
                                padding: 20px;
                                background-color: #fff;
                                border-radius: 8px;
                                box-shadow: 2px 2px 2px 2px rgba(0.5, 0.5, 0.5, 0.5);
                            }}

                            p {{
                                margin: 10px 0;
                            }}

                            button {{
                                background-color: #42a5f5;
                                color: white;
                                padding: 10px 15px;
                                text-align: center;
                                text-decoration: none;
                                display: inline-block;
                                font-size: 16px;
                                margin: 10px 0;
                                cursor: pointer;
                                border: none;
                                border-radius: 4px;
                            }}

                            button:hover {{
                                background-color: #1279cc;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class=""container"">
                            <div class=""conteudo"">
                                <p>Obrigado por se registrar em nosso <br> <b>Sistema de Controle de Atividades</b>!</p>
                                <small>Clique no botão para confirmar seu email:</small>
                                <a href=""{linkConfirmacao}"">
                                    <button>Confirmar Email</button>
                                </a>
                            </div>
                        </div>
                    </body>
                    </html>
                    ";

                // Agora você pode usar a variável 'htmlBody' no corpo do seu e-mail


                // Cria o cliente SMTP
                using (SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort))
                {
                    // Configura as credenciais do SMTP
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = true;

                    // Cria a mensagem de email
                    using (MailMessage mailMessage = new MailMessage(senderEmail, email, subject, body))
                    {
                        // Configura o formato do corpo do email
                        mailMessage.IsBodyHtml = true;

                        // Envie o email de forma assíncrona
                        await smtpClient.SendMailAsync(mailMessage);
                        return true; 
                    }
                }
            }
            catch
            {
                return false;
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

        private string GeraTokenConfirmacaoEmail(string email)
        {
            //define declarações do usuário
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, email),
                new Claim("time", "manchesterCity"),
                new Claim("time", "corinthians"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //gera uma chave com base em um algoritmo simetrico
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:keyConfirmRegister"]!));

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
            return new JwtSecurityTokenHandler().WriteToken(token); 
        }

        private bool ValidarTokenConfirmacaoEmail(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:keyConfirmRegister"]!);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["TokenConfiguration:Issuer"],
                ValidAudience = _config["TokenConfiguration:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string Criptografar(string texto)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(_config["AesKey:key"]!);
                aesAlg.GenerateIV();

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(texto);
                        }
                    }

                    return Convert.ToBase64String(aesAlg.IV.Concat(msEncrypt.ToArray()).ToArray());
                }
            }
        }

        private string Descriptografar(string textoCriptografado)
        {
            using (Aes aesAlg = Aes.Create())
            {
                byte[] cipherText = Convert.FromBase64String(textoCriptografado.Replace("&", "/"));
                aesAlg.Key = Encoding.UTF8.GetBytes(_config["AesKey:key"]!);
                aesAlg.IV = cipherText.Take(16).ToArray();

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText.Skip(16).ToArray()))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        private bool VerificarEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}

using AtividadesAPI.Models;
using AtividadesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtividadesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtividadeController : ControllerBase
    {
        private readonly IAtividade _atividadeService; 

        public AtividadeController(IAtividade atividadeService)
        {
            _atividadeService = atividadeService;
        }

        [HttpGet]
        public async Task<ActionResult<Atividade>> GetAllAtividades()
        {
            try
            {
                var result = await _atividadeService.GetAllAtividades(); 

                if(result.Count() > 0)
                {
                    return Ok(result);
                }

                return Ok("Nenhum registro gravado na base de dados!"); 
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

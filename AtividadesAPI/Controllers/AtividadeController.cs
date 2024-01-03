using AtividadesAPI.Models;
using AtividadesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpGet("id:int")]
        public async Task<ActionResult<Atividade>> GetByIdAtividade(int id)
        {
            try
            {
                var result = await _atividadeService.GetByIdAtividade(id); 

                if(result != null)
                {
                    return Ok(result); 
                }

                return BadRequest("Não foi encontrar uma atividade com o Id informado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Atividade>>> GetAllAtividades()
        {
            try
            {
                var result = await _atividadeService.GetAllAtividades();

                if (result.Count() > 0)
                {
                    return Ok(result);
                }

                return Ok("Nenhum registro gravado na base de dados!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddAtividade([FromBody] Atividade atividade)
        {
            try
            {
                if (atividade != null)
                {
                    bool result = await _atividadeService.AddAtividade(atividade);

                    if (result)
                    {
                        return Ok("Atividade adicionada com sucesso!");
                    }
                }

                return BadRequest("Corpo da requisição é nulo");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAtividade([FromBody] Atividade atividade)
        {
            try
            {
                if (atividade != null)
                {
                    bool result = await _atividadeService.UpdateAtividade(atividade);

                    if (result)
                    {
                        return Ok("Atividade alterada com sucesso!");
                    }
                }

                return BadRequest("Não foi encontrar uma atividade com o Id informado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAtividade([FromRoute] int id)
        {
            try
            {
                bool result = await _atividadeService.DeleteAtividade(id);

                if (result)
                {
                    return Ok("Atividade removida com sucesso!");
                }

                return BadRequest("Não foi encontrar uma atividade com o Id informado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

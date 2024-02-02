using AtividadesAPI.Dto;
using AtividadesAPI.Filters;
using AtividadesAPI.Models;
using AtividadesAPI.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AtividadesAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ServiceFilter(typeof(FilterAtividade))]
    [Route("api/[controller]")]
    [ApiController]
    public class AtividadeController : ControllerBase
    {
        private readonly IAtividade _atividadeService;
        private readonly IMapper _mapper; 

        public AtividadeController(IAtividade atividadeService, IMapper mapper)
        {
            _atividadeService = atividadeService;
            _mapper = mapper;
        }

        [HttpGet("{userId}/{id:int}")]
        public async Task<ActionResult<AtividadeDTO>> GetByIdAtividade([FromRoute]string userId, [FromRoute]int id)
        {
            try
            {
                var result = await _atividadeService.GetByIdAtividade(userId, id); 

                if(result != null)
                {
                    var atividadeDto = _mapper.Map<AtividadeDTO>(result);

                    return Ok(atividadeDto); 
                }

                return BadRequest("Não foi encontrar uma atividade com o Id informado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<AtividadeDTO>>> GetAllAtividades([FromRoute]string userId)
        {
            try
            {
                var result = await _atividadeService.GetAllAtividades(userId);

                var atividadesDTO = _mapper.Map<IEnumerable<AtividadeDTO>>(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddAtividade([FromBody] NovaAtividadeDTO atividadeDto)
        {
            try
            {
                if (atividadeDto != null)
                {
                    var atividade = _mapper.Map<Atividade>(atividadeDto); 
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
        public async Task<ActionResult> UpdateAtividade([FromBody] NovaAtividadeDTO atividadeDto)
        {
            try
            {
                if (atividadeDto != null)
                {
                    var atividade = _mapper.Map<Atividade>(atividadeDto); 
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

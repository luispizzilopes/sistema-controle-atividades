using AtividadesAPI.Models;
using AtividadesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AtividadesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoria _categoriaService;

        public CategoriaController(ICategoria categoriaService)
        {
            _categoriaService = categoriaService; 
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Categoria>> GetByIdCategoria(int id)
        {
            try
            {
                var result = await _categoriaService.GetByIdCategoria(id);

                if (result != null)
                {
                    return Ok(result);
                }

                return BadRequest("Não foi encontrar uma categoria com o Id informado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAllCategoria()
        {
            try
            {
                var result = await _categoriaService.GetAllCategoria();

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
        public async Task<ActionResult> AddCategoria([FromBody] Categoria categoria)
        {
            try
            {
                if (categoria != null)
                {
                    bool result = await _categoriaService.AddCategoria(categoria);

                    if (result)
                    {
                        return Ok("Categoria adicionada com sucesso!");
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
        public async Task<ActionResult> UpdateCategoria([FromBody] Categoria categoria)
        {
            try
            {
                if (categoria != null)
                {
                    bool result = await _categoriaService.UpdateCategoria(categoria);

                    if (result)
                    {
                        return Ok("Categoria alterada com sucesso!");
                    }
                }

                return BadRequest("Não foi encontrar uma categoria com o Id informado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategoria([FromRoute] int id)
        {
            try
            {
                bool result = await _categoriaService.DeleteCategoria(id);

                if (result)
                {
                    return Ok("Categoria removida com sucesso!");
                }

                return BadRequest("Categoria vinculada a uma atividade existente ou categoria não encontrada!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

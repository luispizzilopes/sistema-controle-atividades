﻿using AtividadesAPI.Dto;
using AtividadesAPI.Filters;
using AtividadesAPI.Models;
using AtividadesAPI.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtividadesAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ServiceFilter(typeof(FilterCategoria))]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoria _categoriaService;
        private readonly IMapper _mapper; 

        public CategoriaController(ICategoria categoriaService, IMapper mapper)
        {
            _categoriaService = categoriaService; 
            _mapper = mapper;
        }

        [HttpGet("{userId}/{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> GetByIdCategoria([FromRoute] string userId, [FromRoute]int id)
        {
            try
            {
                var result = await _categoriaService.GetByIdCategoria(userId, id);

                if (result != null)
                {
                    var categoria = _mapper.Map<CategoriaDTO>(result); 
                    return Ok(categoria);
                }

                return BadRequest("Não foi encontrar uma categoria com o Id informado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetAllCategoria([FromRoute] string userId)
        {
            try
            {
                var result = await _categoriaService.GetAllCategoria(userId);

                var categorias = _mapper.Map<IEnumerable<CategoriaDTO>>(result);
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddCategoria([FromBody] CategoriaDTO categoriaDto)
        {
            try
            {
                if (categoriaDto != null)
                {
                    var categoria = _mapper.Map<Categoria>(categoriaDto); 
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
        public async Task<ActionResult> UpdateCategoria([FromBody] CategoriaDTO categoriaDto)
        {
            try
            {
                if (categoriaDto != null)
                {
                    var categoria = _mapper.Map<Categoria>(categoriaDto);
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

using AtividadesAPI.Context;
using AtividadesAPI.Dto;
using AtividadesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtividadesAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IAtividade _atividadeService;
        private readonly ICategoria _categoriaService; 

        public HomeController(IAtividade atividadeService, ICategoria categoriaService)
        {
            _atividadeService = atividadeService;
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<HomeInfoDTO>> HomeInfo()
        {
            try
            {
                DateTime hoje = DateTime.Now;
                var atividadesCadastradas = await _atividadeService.GetAllAtividades();
                var categoriasCadastradas = await _categoriaService.GetAllCategoria();

                double tempoTotalAtividades = atividadesCadastradas.Sum(a => (a.FinalAtividade - a.InicioAtividade).Hours);
                int quantidadeAtividadesCadastradasMesAtual = atividadesCadastradas.Where(a => a.InicioAtividade.Month == hoje.Month && a.InicioAtividade.Year == hoje.Year).ToList().Count();
                int quantidadeCategoriaCadastradasMesAtual = categoriasCadastradas.Where(c => c.DataCriacaoCategoria.Month == hoje.Month && c.DataCriacaoCategoria.Year == hoje.Year).ToList().Count();
                int quantidadeGeralAtividades = atividadesCadastradas.Count();

                var arrayGraficoQuantidadeAtividade = new int[12];
                var arrayGraficoQuantidadeCategoria = new int[12]; 

                for(int i = 0; i<12; i++)
                {
                    int quantidadeAtividade = atividadesCadastradas.Where(a => a.InicioAtividade.Year == hoje.Year && a.InicioAtividade.Month == i + 1).ToList().Count(); 
                    int quantidadeCategoria = categoriasCadastradas.Where(c => c.DataCriacaoCategoria.Year == hoje.Year && c.DataCriacaoCategoria.Month == i + 1).ToList().Count();

                    arrayGraficoQuantidadeAtividade[i] = quantidadeAtividade;
                    arrayGraficoQuantidadeCategoria[i] = quantidadeCategoria;
                }

                return Ok(new HomeInfoDTO
                {
                    AtividadesCadastradas = quantidadeAtividadesCadastradasMesAtual,
                    CategoriasCadastradas = quantidadeCategoriaCadastradasMesAtual,
                    TempoTotalAtividades = tempoTotalAtividades,
                    TotalGeralAtividades = quantidadeGeralAtividades,
                    GraficoAtividades = arrayGraficoQuantidadeAtividade, 
                    GraficoCategorias = arrayGraficoQuantidadeCategoria, 
                }); 
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro durante a execução do Endpoint");
            }
        }
    }
}

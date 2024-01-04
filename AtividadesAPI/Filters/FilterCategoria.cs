using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.CompilerServices;

namespace AtividadesAPI.Filters
{
    public class FilterCategoria : IActionFilter
    {
        private readonly ILogger _logger; 

        public FilterCategoria(ILogger<FilterCategoria> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("<-----------------Executando ação do EndPoint da Controller Categoria----------------->"); 
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("<-----------------Final da Execução do EndPoin da Controller Categoria----------------->");
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;

namespace AtividadesAPI.Filters
{
    public class FilterAtividade : IActionFilter
    {
        private readonly ILogger _logger;

        public FilterAtividade(ILogger<FilterAtividade> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("<-----------------Executando ação do EndPoint da Controller Atividade----------------->");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("<-----------------Ação do EndPoin da Controller Atividade----------------->");
        }
    }
}

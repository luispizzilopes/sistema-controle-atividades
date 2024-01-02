using AtividadesAPI.Context;
using AtividadesAPI.Models;
using AtividadesAPI.Repositories.Interfaces;

namespace AtividadesAPI.Repositories
{
    public class RepositoryAtividade : Repository<Atividade>, IRepositoryAtividade
    {
        public RepositoryAtividade(AppDbContext context) : base(context)
        {
        }
    }
}

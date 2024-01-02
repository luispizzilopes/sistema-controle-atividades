
using AtividadesAPI.Models;
using AtividadesAPI.Repositories;
using AtividadesAPI.Repositories.Interfaces;
using AtividadesAPI.Services.Interfaces;
using Microsoft.Data.SqlClient.DataClassification;

namespace AtividadesAPI.Services
{
    public class AtividadeService : IAtividade
    {
        private readonly IRepositoryAtividade _repositoryAtividade; 

        public AtividadeService(IRepositoryAtividade repositoryAtividade)
        {
            _repositoryAtividade = repositoryAtividade;
        }

        public async Task<IEnumerable<Atividade>> GetAllAtividades()
        {
            return await _repositoryAtividade.GetAll(); 
        }

        public async Task<Atividade> GetByIdAtividade(int id)
        {
            return await _repositoryAtividade.GetById(a => a.AtividadeId == id); 
        }

        public async Task<bool> AddAtividade(Atividade atividade)
        {
            if(atividade != null)
            {
                await _repositoryAtividade.Add(atividade);

                return true; 
            }

            return false; 
        }
    }
}

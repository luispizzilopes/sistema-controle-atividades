
using AtividadesAPI.Models;
using AtividadesAPI.Repositories;
using AtividadesAPI.Repositories.Interfaces;
using AtividadesAPI.Services.Interfaces;
using Microsoft.Data.SqlClient.DataClassification;

namespace AtividadesAPI.Services
{
    public class AtividadeService : IAtividade
    {
        private readonly IRepository<Atividade> _repositoryAtividade; 

        public AtividadeService(IRepository<Atividade> repositoryAtividade)
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
                atividade.DataCriacaoAtividade = DateTime.Now; 
                await _repositoryAtividade.Add(atividade);

                return true; 
            }

            return false; 
        }

        public async Task<bool> UpdateAtividade(Atividade atividade)
        {
            var atividadeExiste = await _repositoryAtividade.GetById(a => a.AtividadeId == atividade.AtividadeId) != null ? true : false;

            if (atividadeExiste)
            {
                atividade.DataAlteracaoAtividade = DateTime.Now; 

                await _repositoryAtividade.Update(atividade);

                return true; 
            }

            return false;
        }

        public async Task<bool> DeleteAtividade(int id) 
        {
            var atividade = await _repositoryAtividade.GetById(a => a.AtividadeId == id);

            if (atividade != null)
            {
                await _repositoryAtividade.Delete(atividade);

                return true; 
            }

            return false; 
        }
    }
}

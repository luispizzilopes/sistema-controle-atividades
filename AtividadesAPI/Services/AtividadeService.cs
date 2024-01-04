
using AtividadesAPI.Models;
using AtividadesAPI.Repositories.Interfaces;
using AtividadesAPI.Services.Interfaces;

namespace AtividadesAPI.Services
{
    public class AtividadeService : IAtividade
    {
        private readonly IRepository<Atividade> _repositoryAtividade;
        private readonly IRepository<RegistroLog> _repositoryRegistroLog; 

        public AtividadeService(IRepository<Atividade> repositoryAtividade, IRepository<RegistroLog> repositoryRegistroLog)
        {
            _repositoryAtividade = repositoryAtividade;
            _repositoryRegistroLog = repositoryRegistroLog;
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

                await _repositoryRegistroLog.Add(new RegistroLog
                {
                    DescricaoRegistro = $"Nova atividade '{atividade.DescricaoAtividade}' registrada na base de dados às {DateTime.Now.TimeOfDay} do dia {DateTime.Now.ToString("dd/MM/yyyy")}"
                }); 

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

                await _repositoryRegistroLog.Add(new RegistroLog
                {
                    DescricaoRegistro = $"Atividade de Id {atividade.AtividadeId} modificada na base de dados às {DateTime.Now.TimeOfDay} do dia {DateTime.Now.ToString("dd/MM/yyyy")}"
                });

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

                await _repositoryRegistroLog.Add(new RegistroLog
                {
                    DescricaoRegistro = $"Atividade de Id {atividade.AtividadeId} removida na base de dados às {DateTime.Now.TimeOfDay} do dia {DateTime.Now.ToString("dd/MM/yyyy")}"
                });

                return true; 
            }

            return false; 
        }
    }
}

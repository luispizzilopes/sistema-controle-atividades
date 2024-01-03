using AtividadesAPI.Models;

namespace AtividadesAPI.Services.Interfaces
{
    public interface IAtividade
    {
        Task<IEnumerable<Atividade>> GetAllAtividades();
        Task<Atividade> GetByIdAtividade(int id);
        Task<bool> AddAtividade(Atividade atividade);
        Task<bool> UpdateAtividade(Atividade atividade);
        Task<bool> DeleteAtividade(int id); 
    }
}

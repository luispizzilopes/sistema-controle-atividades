using AtividadesAPI.Dto;
using AtividadesAPI.Models;

namespace AtividadesAPI.Services.Interfaces
{
    public interface IAtividadeFutura
    {
        Task<IEnumerable<AtividadeDTO>> GetAllAtividadesFuturas(string userId);
        Task<AtividadeDTO?> GetByIdAtividadeFutura(string userId, int id);
        Task<bool> AddAtividadeFutura(Atividade atividade);
        Task<bool> UpdateAtividadeFutura(Atividade atividade);
        Task<bool> DeleteAtividadeFutura(int id);
    }
}

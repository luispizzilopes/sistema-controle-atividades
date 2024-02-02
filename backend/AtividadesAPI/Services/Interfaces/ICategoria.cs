using AtividadesAPI.Models;

namespace AtividadesAPI.Services.Interfaces
{
    public interface ICategoria
    {
        Task<IEnumerable<Categoria>> GetAllCategoria(string userId);
        Task<Categoria> GetByIdCategoria(string userId, int id);
        Task<bool> AddCategoria(Categoria categoria);
        Task<bool> UpdateCategoria(Categoria categoria);
        Task<bool> DeleteCategoria(int id);
    }
}

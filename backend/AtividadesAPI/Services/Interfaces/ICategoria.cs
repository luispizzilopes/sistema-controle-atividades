using AtividadesAPI.Models;

namespace AtividadesAPI.Services.Interfaces
{
    public interface ICategoria
    {
        Task<IEnumerable<Categoria>> GetAllCategoria();
        Task<Categoria> GetByIdCategoria(int id);
        Task<bool> AddCategoria(Categoria categoria);
        Task<bool> UpdateCategoria(Categoria categoria);
        Task<bool> DeleteCategoria(int id);
    }
}

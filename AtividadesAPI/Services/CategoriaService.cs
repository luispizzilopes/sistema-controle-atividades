using AtividadesAPI.Models;
using AtividadesAPI.Repositories.Interfaces;
using AtividadesAPI.Services.Interfaces;
using System.Linq;

namespace AtividadesAPI.Services
{
    public class CategoriaService : ICategoria
    {
        private readonly IRepository<Categoria> _repositoryCategoria;
        private readonly IRepository<Atividade> _repositoryAtividade; 

        public CategoriaService(IRepository<Categoria> repositoryCategoria, IRepository<Atividade> repositoryAtividade)
        {
            _repositoryCategoria = repositoryCategoria;
            _repositoryAtividade = repositoryAtividade;
        }

        public async Task<IEnumerable<Categoria>> GetAllCategoria()
        {
            return await _repositoryCategoria.GetAll();
        }

        public async Task<Categoria> GetByIdCategoria(int id)
        {
            return await _repositoryCategoria.GetById(c => c.CategoriaId == id);
        }

        public async Task<bool> AddCategoria(Categoria categoria)
        {
            if (categoria != null)
            {
                categoria.DataCriacaoCategoria = DateTime.Now;
                await _repositoryCategoria.Add(categoria);

                return true;
            }

            return false;
        }

        public async Task<bool> UpdateCategoria(Categoria categoria)
        {
            var categoriaExiste = await _repositoryCategoria.GetById(c => c.CategoriaId == categoria.CategoriaId) != null ? true : false;

            if (categoriaExiste)
            {
                categoria.DataAlteracaoCategoria = DateTime.Now;

                await _repositoryCategoria.Update(categoria);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteCategoria(int id)
        {
            var countAtividades = await _repositoryAtividade.GetAll();

            if (countAtividades.Where(a => a.CategoriaId == id).ToList().Count == 0)
            {
                var categoria = await _repositoryCategoria.GetById(c => c.CategoriaId == id);

                if (categoria != null)
                {
                    await _repositoryCategoria.Delete(categoria);

                    return true;
                }

                return false;
            }

            return false; 
        }
    }
}

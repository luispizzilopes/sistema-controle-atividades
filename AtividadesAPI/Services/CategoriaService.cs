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
        private readonly IRepository<RegistroLog> _repositoryRegistroLog;

        public CategoriaService(IRepository<Categoria> repositoryCategoria, IRepository<Atividade> repositoryAtividade, IRepository<RegistroLog> repositoryRegistroLog)
        {
            _repositoryCategoria = repositoryCategoria;
            _repositoryAtividade = repositoryAtividade;
            _repositoryRegistroLog = repositoryRegistroLog;
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

                await _repositoryRegistroLog.Add(new RegistroLog
                {
                    DescricaoRegistro = $"Nova categoria '{categoria.DescricaoCategoria}' registrada na base de dados às {DateTime.Now.TimeOfDay} do dia {DateTime.Now.ToString("dd/MM/yyyy")}"
                });

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

                await _repositoryRegistroLog.Add(new RegistroLog
                {
                    DescricaoRegistro = $"Categoria de Id {categoria.CategoriaId} modificada na base de dados às {DateTime.Now.TimeOfDay} do dia {DateTime.Now.ToString("dd/MM/yyyy")}"
                });

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

                    await _repositoryRegistroLog.Add(new RegistroLog
                    {
                        DescricaoRegistro = $"Categoria de Id {categoria.CategoriaId} modificada na base de dados às {DateTime.Now.TimeOfDay} do dia {DateTime.Now.ToString("dd/MM/yyyy")}"
                    });

                    return true;
                }

                return false;
            }

            return false; 
        }
    }
}

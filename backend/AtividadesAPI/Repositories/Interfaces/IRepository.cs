using System.Linq.Expressions;

namespace AtividadesAPI.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Expression<Func<T, bool>> preticate);
        Task Add(T entity); 
        Task Update(T entity);
        Task Delete(T entity);
    }
}

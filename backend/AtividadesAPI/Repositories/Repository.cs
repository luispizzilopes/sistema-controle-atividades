using AtividadesAPI.Context;
using AtividadesAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Linq.Expressions;

namespace AtividadesAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context; 

        public Repository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context
                .Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> GetById(Expression<Func<T, bool>> preticate)
        {
            return await _context
                .Set<T>()
                .AsNoTracking()
                .SingleOrDefaultAsync(preticate); 
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            await _context.SaveChangesAsync(); 
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(); 
        }
        
        public async Task Delete(T entity)
        {
            _context.Remove(entity); 
            await _context.SaveChangesAsync();
        }
    }
}

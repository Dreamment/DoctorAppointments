using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RepositoryContext _context;

        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entity)
            => await _context.Set<T>().AddAsync(entity);

        public async Task DeleteAsync(T entity)
            => await Task.FromResult(_context.Set<T>().Remove(entity));

        public async Task<IEnumerable<T>> FindAllAsync(bool trackChanges)
            => !trackChanges? 
            await _context.Set<T>().AsNoTracking().ToListAsync() : 
            await _context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> FindAllWithDetailsAsync(bool trackChanges, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsNoTracking().AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return !trackChanges ? await query.AsNoTracking().ToListAsync() : await query.ToListAsync();
        }


        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges)
        => !trackChanges ?
            await _context.Set<T>().Where(expression).AsNoTracking().ToListAsync() :
            await _context.Set<T>().Where(expression).ToListAsync();

        public async Task<IEnumerable<T>> FindByConditionWithDetailsAsync(Expression<Func<T, bool>> expression, bool trackChanges, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().Where(expression).AsNoTracking().AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return !trackChanges ? await query.AsNoTracking().ToListAsync() : await query.ToListAsync();
        }

        public async Task UpdateAsync(T entity)
            => await Task.FromResult(_context.Set<T>().Update(entity));
    }
}

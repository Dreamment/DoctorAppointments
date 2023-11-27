using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> FindAllAsync(bool trackChanges);

        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);

        Task CreateAsync(T entity);

        Task DeleteAsync(T entity);

        Task UpdateAsync(T entity);

        Task<IEnumerable<T>> FindAllWithDetailsAsync(bool trackChanges, params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> FindByConditionWithDetailsAsync(Expression<Func<T, bool>> expression, bool trackChanges, 
            params Expression<Func<T, object>>[] includes);
    }
}

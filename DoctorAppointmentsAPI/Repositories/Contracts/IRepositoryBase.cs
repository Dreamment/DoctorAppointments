using System.Linq.Expressions;

namespace DoctorAppointmentsAPI.Repositories.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> FindAllAsync(bool trackChanges);

        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);

        Task CreateAsync(T entity);

        Task DeleteAsync(T entity);

        Task UpdateAsync(T entity);
    }
}

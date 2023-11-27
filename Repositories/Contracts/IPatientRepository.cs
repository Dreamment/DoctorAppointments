using Entities.Models;
using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IPatientRepository : IRepositoryBase<Patients>
    {
        Task<IEnumerable<Patients>> GetAllPatientsAsync(bool trackChanges);

        Task<IEnumerable<Patients>> GetPatientsByConditionAsync(
            Expression<Func<Patients, bool>> expression, bool trackChanges);

        Task CreatePatientAsync(Patients patient);

        Task DeletePatientAsync(Patients patient);

        Task UpdatePatientAsync(Patients patient);

        Task<IEnumerable<Patients>> GetAllPatientsWithDetailsAsync(bool trackChanges, 
            params Expression<Func<Patients, object>>[] expression);

        Task<IEnumerable<Patients>> GettPatientsByConditionWithDetailsAsync(
            Expression<Func<Patients, bool>> expression, bool trackChanges,
            params Expression<Func<Patients, object>>[] expressionInclude);
    }
}

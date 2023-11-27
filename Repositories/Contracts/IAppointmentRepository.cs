using Entities.Models;
using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IAppointmentRepository : IRepositoryBase<Appointments>
    {
        Task<IEnumerable<Appointments>> GetAllAppointmentsAsync(bool trackChanges);

        Task<IEnumerable<Appointments>> GetAppointmentsByConditionAsync(
            Expression<Func<Appointments, bool>> expression, bool trackChanges);

        Task CreateAppointmentAsync(Appointments appointment);

        Task DeleteAppointmentAsync(Appointments appointment);

        Task UpdateAppointmentAsync(Appointments appointment);

        Task<IEnumerable<Appointments>> GetAllAppointmentsWithDetailsAsync(bool trackChanges, 
            params Expression<Func<Appointments, object>>[] includes);

        Task<IEnumerable<Appointments>> GetAppointmentsByConditionWithDetailsAsync(
            Expression<Func<Appointments, bool>> expression, bool trackChanges, 
            params Expression<Func<Appointments, object>>[] includes);
    }
}

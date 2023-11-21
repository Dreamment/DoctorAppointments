using DoctorAppointmentsAPI.Entities;
using System.Linq.Expressions;

namespace DoctorAppointmentsAPI.Repositories.Contracts
{
    public interface IAppointmentRepository : IRepositoryBase<Appointments>
    {
        Task<IEnumerable<Appointments>> GetAllAppointmentsAsync(bool trackChanges);
        Task<Appointments> GetAppointmentByCodeAsync(string appointmentCode, bool trackChanges);
        Task<IEnumerable<Appointments>> GetAppointmentsByConditionAsync(Expression<Func<Appointments, bool>> expression, bool trackChanges);
        Task CreateAppointmentAsync(Appointments appointment);
        Task DeleteAppointmentAsync(Appointments appointment);
        Task UpdateAppointmentAsync(Appointments appointment);
    }
}

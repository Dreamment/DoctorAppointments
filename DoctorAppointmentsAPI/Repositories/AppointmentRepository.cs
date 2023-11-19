using DoctorAppointmentsAPI.Repositories.Contracts;
using DoctorAppointmentsDomain.Entities;
using System.Linq.Expressions;

namespace DoctorAppointmentsAPI.Repositories
{
    public class AppointmentRepository : RepositoryBase<Appointments>, IAppointmentRepository
    {
        public AppointmentRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task CreateAppointment(Appointments appointment)
            => await CreateAsync(appointment);

        public async Task DeleteAppointment(Appointments appointment)
            => await DeleteAsync(appointment);

        public async Task<IEnumerable<Appointments>> GetAllAppointmentsAsync(bool trackChanges)
            => await FindAllAsync(trackChanges);

        public async Task<Appointments> GetAppointmentByCodeAsync(int appointmentCode, bool trackChanges)
        {
            var appointments = await FindByConditionAsync(a => a.AppointmentCode.Equals(appointmentCode), trackChanges);
            return appointments.FirstOrDefault();
        }

        public async Task<IEnumerable<Appointments>> GetAppointmentsByConditionAsync(Expression<Func<Appointments, bool>> expression, bool trackChanges)
            => await FindByConditionAsync(expression, trackChanges);

        public async Task UpdateAppointment(Appointments appointment)
            => await UpdateAsync(appointment);
    }
    {
    }
}

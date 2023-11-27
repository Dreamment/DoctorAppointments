using Repositories.Contracts;
using Entities.Models;
using System.Linq.Expressions;

namespace Repositories
{
    public class AppointmentRepository : RepositoryBase<Appointments>, IAppointmentRepository
    {
        public AppointmentRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task CreateAppointmentAsync(Appointments appointment)
            => await CreateAsync(appointment);

        public async Task DeleteAppointmentAsync(Appointments appointment)
            => await DeleteAsync(appointment);

        public async Task<IEnumerable<Appointments>> GetAllAppointmentsAsync(bool trackChanges)
            => await FindAllAsync(trackChanges);

        public async Task<IEnumerable<Appointments>> GetAllAppointmentsWithDetailsAsync(bool trackChanges, params Expression<Func<Appointments, object>>[] includes)
            => await FindAllWithDetailsAsync(trackChanges, includes);

        public async Task<IEnumerable<Appointments>> GetAppointmentsByConditionAsync(Expression<Func<Appointments, bool>> expression, bool trackChanges)
            => await FindByConditionAsync(expression, trackChanges);

        public async Task<IEnumerable<Appointments>> GetAppointmentsByConditionWithDetailsAsync(Expression<Func<Appointments, bool>> expression, bool trackChanges, params Expression<Func<Appointments, object>>[] includes)
            => await FindByConditionWithDetailsAsync(expression, trackChanges, includes);

        public async Task UpdateAppointmentAsync(Appointments appointment)
            => await UpdateAsync(appointment);
    }
}

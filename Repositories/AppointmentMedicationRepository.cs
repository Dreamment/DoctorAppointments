using Repositories.Contracts;
using Entities.Models;
using System.Linq.Expressions;

namespace Repositories
{
    public class AppointmentMedicationRepository : RepositoryBase<AppointmentMedications>, IAppointmentMedicationRepository
    {
        public AppointmentMedicationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task CreateAppointmentMedicationAsync(AppointmentMedications appointmentMedication)
            => await CreateAsync(appointmentMedication);

        public async Task DeleteAppointmentMedicationAsync(AppointmentMedications appointmentMedication)
            => await DeleteAsync(appointmentMedication);

        public async Task<IEnumerable<AppointmentMedications>> GetAllAppointmentMedicationsAsync(bool trackChanges)
            => await FindAllAsync(trackChanges);

        public async Task<IEnumerable<AppointmentMedications>> GetAppointmentMedicationsByConditionAsync(Expression<Func<AppointmentMedications, bool>> expression, bool trackChanges)
            => await FindByConditionAsync(expression, trackChanges);

        public async Task UpdateAppointmentMedicationAsync(AppointmentMedications appointmentMedication)
            => await UpdateAsync(appointmentMedication);
    }
}

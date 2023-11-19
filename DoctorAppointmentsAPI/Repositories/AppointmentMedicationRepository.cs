using DoctorAppointmentsAPI.Repositories.Contracts;
using DoctorAppointmentsDomain.Entities;
using System.Linq.Expressions;

namespace DoctorAppointmentsAPI.Repositories
{
    public class AppointmentMedicationRepository : RepositoryBase<AppointmentMedications>, IAppointmentMedicationRepository
    {
        public AppointmentMedicationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task CreateAppointmentMedication(AppointmentMedications appointmentMedication)
            => await CreateAsync(appointmentMedication);

        public async Task DeleteAppointmentMedication(AppointmentMedications appointmentMedication)
            => await DeleteAsync(appointmentMedication);

        public async Task<IEnumerable<AppointmentMedications>> GetAllAppointmentMedicationsAsync(bool trackChanges)
            => await FindAllAsync(trackChanges);

        public async Task<AppointmentMedications> GetAppointmentMedicationByIdAsync(int medicationId, bool trackChanges)
        {
            var appointmentMedications = await FindByConditionAsync(a => a.MedicationId.Equals(medicationId), trackChanges);
            return appointmentMedications.FirstOrDefault();
        }

        public async Task<IEnumerable<AppointmentMedications>> GetAppointmentMedicationsByConditionAsync(Expression<Func<AppointmentMedications, bool>> expression, bool trackChanges)
            => await FindByConditionAsync(expression, trackChanges);

        public async Task UpdateAppointmentMedication(AppointmentMedications appointmentMedication)
            => await UpdateAsync(appointmentMedication);
    }
}

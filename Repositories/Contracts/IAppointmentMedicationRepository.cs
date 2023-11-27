using Entities.Models;
using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IAppointmentMedicationRepository : IRepositoryBase<AppointmentMedications>
    {
        Task<IEnumerable<AppointmentMedications>> GetAllAppointmentMedicationsAsync(bool trackChanges);

        Task<IEnumerable<AppointmentMedications>> GetAppointmentMedicationsByConditionAsync(
            Expression<Func<AppointmentMedications, bool>> expression, bool trackChanges);

        Task CreateAppointmentMedicationAsync(AppointmentMedications appointmentMedication);

        Task DeleteAppointmentMedicationAsync(AppointmentMedications appointmentMedication);

        Task UpdateAppointmentMedicationAsync(AppointmentMedications appointmentMedication);

        Task<IEnumerable<AppointmentMedications>> GetAllAppointmentMedicationsWithDetailsAsync(bool trackChanges, 
            params Expression<Func<AppointmentMedications, object>>[] includes);

        Task<IEnumerable<AppointmentMedications>> GetAppointmentMedicationsByConditionWithDetailsAsync(
            Expression<Func<AppointmentMedications, bool>> expression,
            bool trackChanges, params Expression<Func<AppointmentMedications, object>>[] includes);
    }
}

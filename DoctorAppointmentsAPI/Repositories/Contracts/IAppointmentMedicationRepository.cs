using DoctorAppointmentsAPI.Entities;
using System.Linq.Expressions;

namespace DoctorAppointmentsAPI.Repositories.Contracts
{
    public interface IAppointmentMedicationRepository : IRepositoryBase<AppointmentMedications>
    {
        Task<IEnumerable<AppointmentMedications>> GetAllAppointmentMedicationsAsync(bool trackChanges);
        Task<AppointmentMedications> GetAppointmentMedicationByIdAsync(int medicationId, bool trackChanges);
        Task<IEnumerable<AppointmentMedications>> GetAppointmentMedicationsByConditionAsync(Expression<Func<AppointmentMedications, bool>> expression, bool trackChanges);
        Task CreateAppointmentMedicationAsync(AppointmentMedications appointmentMedication);
        Task DeleteAppointmentMedicationAsync(AppointmentMedications appointmentMedication);
        Task UpdateAppointmentMedicationAsync(AppointmentMedications appointmentMedication);
    }
}

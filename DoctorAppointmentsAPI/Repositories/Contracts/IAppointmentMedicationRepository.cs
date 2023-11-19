using DoctorAppointmentsDomain.Entities;
using System.Linq.Expressions;

namespace DoctorAppointmentsAPI.Repositories.Contracts
{
    public interface IAppointmentMedicationRepository : IRepositoryBase<AppointmentMedications>
    {
        Task<IEnumerable<AppointmentMedications>> GetAllAppointmentMedicationsAsync(bool trackChanges);
        Task<AppointmentMedications> GetAppointmentMedicationByIdAsync(int medicationId, bool trackChanges);
        Task<IEnumerable<AppointmentMedications>> GetAppointmentMedicationsByConditionAsync(Expression<Func<AppointmentMedications, bool>> expression, bool trackChanges);
        Task CreateAppointmentMedication(AppointmentMedications appointmentMedication);
        Task DeleteAppointmentMedication(AppointmentMedications appointmentMedication);
        Task UpdateAppointmentMedication(AppointmentMedications appointmentMedication);
    }
}

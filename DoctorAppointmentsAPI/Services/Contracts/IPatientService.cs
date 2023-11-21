using DoctorAppointmentsAPI.DataTransferObjects;

namespace DoctorAppointmentsAPI.Services.Contracts
{
    public interface IPatientService
    {
        Task<IEnumerable<GetAppointmentsForPatientDto>> GetAppointmentsForPatientAsync(int patientId, bool trackChanges);
        Task <string>CreateAppointmentAsync(CreateAppointmentDto appointmentDto, bool trackChanges);
        Task<IEnumerable<GetMedicationsForAppointmentDto>> GetMedicationsForAppointmentAsync(int patientId, string appointmentCode, bool trackChanges);
        Task<GetFamilyDoctorDto> GetFamilyDoctorAsync(int patientId, bool trackChanges);
    }
}

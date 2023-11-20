using DoctorAppointmentsAPI.DataTransferObjects;

namespace DoctorAppointmentsAPI.Services.Contracts
{
    public interface IPatientService
    {
        Task<GetAppointmentsForPatientDto> GetAppointmentsForPatientAsync(int patientId, bool trackChanges);
        Task <string>CreateAppointmentAsync(int patientId, CreateAppointmentDto appointmentDto, bool trackChanges);
        Task<GetMedicationsForAppointmentDto> GetMedicationsForAppointmentAsync(int patientId, string appointmentCode, bool trackChanges);
        Task<GetFamilyDoctorDto> GetFamilyDoctorAsync(int patientId, bool trackChanges);
    }
}

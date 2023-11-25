using Entities.DataTransferObjects;

namespace Services.Contracts
{
    public interface IPatientService
    {
        Task<IEnumerable<GetAppointmentsForPatientDto>> GetAppointmentsForPatientAsync(ulong patientTCId, bool trackChanges);
        Task <string>CreateAppointmentAsync(CreateAppointmentDto appointmentDto, bool trackChanges);
        Task<IEnumerable<GetMedicationsForAppointmentDto>> GetMedicationsForAppointmentAsync(ulong patientTCId, string appointmentCode, bool trackChanges);
        Task<GetFamilyDoctorDto> GetFamilyDoctorAsync(ulong patientTCId, bool trackChanges);
    }
}

using DoctorAppointmentsAPI.DataTransferObjects;

namespace DoctorAppointmentsAPI.Services.Contracts
{
    public interface IDoctorService
    {
        Task<IEnumerable<GetPatientsForFamilyDoctorDto>> GetPatientsAsync(int doctorId, bool trackChanges); // get all patients for the doctor
        Task<IEnumerable<GetAppointmentsForDoctorDto>> GetTodaysAppointmentsAsync(int doctorId, bool trackChanges);
        Task<IEnumerable<GetAppointmentsForDoctorDto>> GetUpcomingAppointmentsAsync(int doctorId, bool trackChanges);
        Task<int> CreateMedicationAsync(int doctorId, CreateMedicationDto medicationDto, bool trackChanges);
        Task ChangeAppointmentStatus(int doctorId, string appointmentCode, bool isApproved, bool trackChanges);
    }
}

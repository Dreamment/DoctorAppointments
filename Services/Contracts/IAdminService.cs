using Entities.DataTransferObjects.Auth;
using Entities.DataTransferObjects.Create;
using Entities.DataTransferObjects.Update;
using Entities.Models;

namespace Services.Contracts
{
    public interface IAdminService
    {
        // AppointmentMedications
        Task<IEnumerable<AppointmentMedications>> GetAllAppointmentMedicationsAsync(bool trackChanges);
        Task<IEnumerable<AppointmentMedications>> GetAppointmentMedicationsByAppointmentCodeAsync(string appointmentCode, bool trackChanges);
        Task<string> CreateAppointmentMedicationAsync(CreateMedicationDto medicationDto, bool trackChanges);
        Task DeleteAppointmentMedicationAsync(string medicationCode, bool trackChanges);
        Task UpdateAppointmentMedicationAsync(string medicationCode, UpdateAppointmentMedicationDto updateAppointmentMedicationDto, bool trackChanges);

        // Appointments
        Task<IEnumerable<Appointments>> GetAllAppointmentsAsync(bool trackChanges);
        Task<IEnumerable<Appointments>> GetAppointmentsByPatientTCIdAsync(ulong patientTCId, bool trackChanges);
        Task<IEnumerable<Appointments>> GetAppointmentsByDoctorCodeAsync(string doctorCode, bool trackChanges);
        Task<string> CreateAppointmentAsync(CreateAppointmentDto appointmentDto, bool trackChanges);
        Task DeleteAppointmentAsync(string appointmentCode, bool trackChanges);
        Task UpdateAppointmentAsync(string appointmentCode, UpdateAppointmentDto updateAppointmentDto, bool trackChanges);

        // Doctors
        Task<IEnumerable<Doctors>> GetAllDoctorsAsync(bool trackChanges);
        Task<Doctors> GetDoctorByDoctorCodeAsync(string doctorCode, bool trackChanges);
        Task<string> CreateDoctorAsync(CreateDoctorDto doctorDto, UserForDoctorRegistrationDto userForRegistrationDto, bool trackChanges);
        Task DeleteDoctorAsync(string doctorCode, bool trackChanges);
        Task UpdateDoctorAsync(string doctorCode, UpdateDoctorDto updateDoctorDto, bool trackChanges);

        // DoctorSpecialties
        Task<IEnumerable<DoctorSpecialties>> GetAllDoctorSpecialtiesAsync(bool trackChanges);
        Task<DoctorSpecialties> GetDoctorSpecialtyByDoctorSpecialtyIdAsync(int doctorSpecialtyId, bool trackChanges);
        Task CreateDoctorSpecialtyAsync(CreateDoctorSpecialtyDto doctorSpecialtyDto, bool trackChanges);
        Task DeleteDoctorSpecialtyAsync(int doctorSpecialtyId, bool trackChanges);
        Task UpdateDoctorSpecialtyAsync(int doctorSpecialtyId, UpdateDoctorSpecialtyDto updateDoctorSpecialtyDto, bool trackChanges);

        // FamilyDoctorChanges
        Task<IEnumerable<FamilyDoctorChanges>> GetAllFamilyDoctorChangesAsync(bool trackChanges);
        Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByPatientTCIdAsync(ulong patientTCId, bool trackChanges);
        Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByDoctorCodeAsync(string doctorCode, bool trackChanges);

        // Patients

        Task<IEnumerable<Patients>> GetAllPatientsAsync(bool trackChanges);
        Task<Patients> GetPatientByPatientTCIdAsync(ulong patientTCId, bool trackChanges);
        Task CreatePatientAsync(CreatePatientDto patientDto, UserForPatientRegistrationDto userForPatientRegistrationDto, bool trackChanges);
        Task DeletePatientAsync(ulong patientTCId, bool trackChanges);
        Task UpdatePatientAsync(ulong patientTCId, UpdatePatientDto updatePatientDto, bool trackChanges);
    }
}

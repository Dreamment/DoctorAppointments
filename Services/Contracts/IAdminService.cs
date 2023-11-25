using Entities.DataTransferObjects.Create;
using Entities.DataTransferObjects.Update;
using Entities.Models;

namespace Services.Contracts
{
    public interface IAdminService
    {
        // AppointmentMedications
        Task<IEnumerable<AppointmentMedications>> GetAllAppointmentMedications(bool trackChanges);
        Task<IEnumerable<AppointmentMedications>> GetAppointmentMedicationsByAppointmentCode(string appointmentCode, bool trackChanges);
        Task<string> CreateAppointmentMedication(CreateMedicationDto medicationDto, bool trackChanges);
        Task DeleteAppointmentMedication(string medicationCode, bool trackChanges);
        Task UpdateAppointmentMedication(string medicationCode, UpdateAppointmentMedicationDto updateAppointmentMedicationDto, bool trackChanges);

        // Appointments
        Task<IEnumerable<Appointments>> GetAllAppointments(bool trackChanges);
        Task<IEnumerable<Appointments>> GetAppointmentsByPatientTCId(int patientTCId, bool trackChanges);
        Task<IEnumerable<Appointments>> GetAppointmentsByDoctorCode(string doctorCode, bool trackChanges);
        Task<string> CreateAppointment(CreateAppointmentDto appointmentDto, bool trackChanges);
        Task DeleteAppointment(string appointmentCode, bool trackChanges);
        Task UpdateAppointment(string appointmentCode, UpdateAppointmentDto updateAppointmentDto, bool trackChanges);

        // Doctors
        Task<IEnumerable<Doctors>> GetAllDoctors(bool trackChanges);
        Task<Doctors> GetDoctorByDoctorCode(string doctorCode, bool trackChanges);
        Task<string> CreateDoctor(CreateDoctorDto doctorDto, bool trackChanges);
        Task DeleteDoctor(string doctorCode, bool trackChanges);
        Task UpdateDoctor(string doctorCode, UpdateDoctorDto updateDoctorDto, bool trackChanges);

        // DoctorSpecialties
        Task<IEnumerable<DoctorSpecialties>> GetAllDoctorSpecialties(bool trackChanges);
        Task<DoctorSpecialties> GetDoctorSpecialtyByDoctorSpecialtyId(int doctorSpecialtyId, bool trackChanges);
        Task CreateDoctorSpecialty(CreateDoctorSpecialtyDto doctorSpecialtyDto, bool trackChanges);
        Task DeleteDoctorSpecialty(int doctorSpecialtyId, bool trackChanges);
        Task UpdateDoctorSpecialty(int doctorSpecialtyId, UpdateDoctorSpecialtyDto updateDoctorSpecialtyDto, bool trackChanges);

        // FamilyDoctorChanges

        Task<IEnumerable<FamilyDoctorChanges>> GetAllFamilyDoctorChanges(bool trackChanges);
        Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByPatientTCId(int patientTCId, bool trackChanges);
        Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByDoctorCode(string doctorCode, bool trackChanges);
        Task<string> CreateFamilyDoctorChange(CreateFamilyDoctorChangeDto familyDoctorChangeDto, bool trackChanges);
        Task DeleteFamilyDoctorChange(string familyDoctorChangeCode, bool trackChanges);

        // Patients

        Task<IEnumerable<Patients>> GetAllPatients(bool trackChanges);
        Task<Patients> GetPatientByPatientTCId(int patientTCId, bool trackChanges);
        Task<string> CreatePatient(CreatePatientDto patientDto, bool trackChanges);
        Task DeletePatient(int patientTCId, bool trackChanges);
        Task UpdatePatient(int patientTCId, UpdatePatientDto updatePatientDto, bool trackChanges);
    }
}

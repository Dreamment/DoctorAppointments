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
        Task<IEnumerable<Appointments>> GetAppointmentsByPatientTCId(ulong patientTCId, bool trackChanges);
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
        Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByPatientTCId(ulong patientTCId, bool trackChanges);
        Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByDoctorCode(string doctorCode, bool trackChanges);
        Task CreateFamilyDoctorChange(CreateFamilyDoctorChangeDto familyDoctorChangeDto, bool trackChanges);
        Task DeleteFamilyDoctorChange(int changeId, bool trackChanges);

        // Patients

        Task<IEnumerable<Patients>> GetAllPatients(bool trackChanges);
        Task<Patients> GetPatientByPatientTCId(ulong patientTCId, bool trackChanges);
        Task CreatePatient(CreatePatientDto patientDto, bool trackChanges);
        Task DeletePatient(ulong patientTCId, bool trackChanges);
        Task UpdatePatient(ulong patientTCId, UpdatePatientDto updatePatientDto, bool trackChanges);
    }
}

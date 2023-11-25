using Entities.DataTransferObjects.Create;
using Entities.DataTransferObjects.Update;
using Entities.Models;
using Services.Admin;
using Services.Contracts;

namespace Services
{
    public class AdminManager : IAdminService
    {

        // implement methods from IAdminService in following files:
        private readonly AdminAppointmentMedications _appointmentMedications;
        private readonly AdminAppointment _appointment;
        private readonly AdminDoctor _doctor;
        private readonly AdminDoctorSpeciality _doctorSpeciality;
        private readonly AdminFamilyDoctorChange _familyDoctorChange;
        private readonly AdminPatient _patient;

        public AdminManager(AdminAppointmentMedications appointmentMedications,
            AdminAppointment appointment,
            AdminDoctor doctor,
            AdminDoctorSpeciality doctorSpeciality,
            AdminFamilyDoctorChange familyDoctorChange,
            AdminPatient patient)
        {
            _appointmentMedications = appointmentMedications;
            _appointment = appointment;
            _doctor = doctor;
            _doctorSpeciality = doctorSpeciality;
            _familyDoctorChange = familyDoctorChange;
            _patient = patient;
        }

        // AppointmentMedications
        public async Task<IEnumerable<AppointmentMedications>> GetAllAppointmentMedicationsAsync(bool trackChanges)
            => await _appointmentMedications.GetAllAppointmentMedicationsAsync(trackChanges);

        public async Task<IEnumerable<AppointmentMedications>> GetAppointmentMedicationsByAppointmentCodeAsync(string appointmentCode, bool trackChanges)
            => await _appointmentMedications.GetAppointmentMedicationsByAppointmentCodeAsync(appointmentCode, trackChanges);

        public async Task<string> CreateAppointmentMedicationAsync(CreateMedicationDto medicationDto, bool trackChanges)
            => await _appointmentMedications.CreateAppointmentMedicationAsync(medicationDto, trackChanges);

        public async Task DeleteAppointmentMedicationAsync(string medicationCode, bool trackChanges)
            => await _appointmentMedications.DeleteAppointmentMedicationAsync(medicationCode, trackChanges);

        public async Task UpdateAppointmentMedicationAsync(string medicationCode, UpdateAppointmentMedicationDto updateAppointmentMedicationDto, bool trackChanges)
            => await _appointmentMedications.UpdateAppointmentMedicationAsync(medicationCode, updateAppointmentMedicationDto, trackChanges);

        // Appointments
        public async Task<IEnumerable<Appointments>> GetAllAppointmentsAsync(bool trackChanges)
            => await _appointment.GetAllAppointmentsAsync(trackChanges);

        public async Task<IEnumerable<Appointments>> GetAppointmentsByPatientTCIdAsync(ulong patientTCId, bool trackChanges)
            => await _appointment.GetAppointmentsByPatientTCIdAsync(patientTCId, trackChanges);

        public async Task<IEnumerable<Appointments>> GetAppointmentsByDoctorCodeAsync(string doctorCode, bool trackChanges)
            => await _appointment.GetAppointmentsByDoctorCodeAsync(doctorCode, trackChanges);

        public async Task<string> CreateAppointmentAsync(CreateAppointmentDto appointmentDto, bool trackChanges)
            => await _appointment.CreateAppointmentAsync(appointmentDto, trackChanges);

        public async Task DeleteAppointmentAsync(string appointmentCode, bool trackChanges)
            => await _appointment.DeleteAppointmentAsync(appointmentCode, trackChanges);

        public async Task UpdateAppointmentAsync(string appointmentCode, UpdateAppointmentDto updateAppointmentDto, bool trackChanges)
            => await _appointment.UpdateAppointmentAsync(appointmentCode, updateAppointmentDto, trackChanges);

        // Doctors
        public async Task<IEnumerable<Doctors>> GetAllDoctorsAsync(bool trackChanges)
            => await _doctor.GetAllDoctorsAsync(trackChanges);

        public async Task<Doctors> GetDoctorByDoctorCodeAsync(string doctorCode, bool trackChanges)
            => await _doctor.GetDoctorByDoctorCodeAsync(doctorCode, trackChanges);

        public async Task<string> CreateDoctorAsync(CreateDoctorDto doctorDto, bool trackChanges)
            => await _doctor.CreateDoctorAsync(doctorDto, trackChanges);

        public async Task DeleteDoctorAsync(string doctorCode, bool trackChanges)
            => await _doctor.DeleteDoctorAsync(doctorCode, trackChanges);

        public async Task UpdateDoctorAsync(string doctorCode, UpdateDoctorDto updateDoctorDto, bool trackChanges)
            => await _doctor.UpdateDoctorAsync(doctorCode, updateDoctorDto, trackChanges);

        // DoctorSpecialties
        public async Task<IEnumerable<DoctorSpecialties>> GetAllDoctorSpecialtiesAsync(bool trackChanges)
            => await _doctorSpeciality.GetAllDoctorSpecialtiesAsync(trackChanges);

        public async Task<DoctorSpecialties> GetDoctorSpecialtyByDoctorSpecialtyIdAsync(int doctorSpecialtyId, bool trackChanges)
            => await _doctorSpeciality.GetDoctorSpecialtyByDoctorSpecialtyIdAsync(doctorSpecialtyId, trackChanges);

        public async Task CreateDoctorSpecialtyAsync(CreateDoctorSpecialtyDto doctorSpecialtyDto, bool trackChanges)
            => await _doctorSpeciality.CreateDoctorSpecialtyAsync(doctorSpecialtyDto, trackChanges);

        public async Task DeleteDoctorSpecialtyAsync(int doctorSpecialtyId, bool trackChanges)
            => await _doctorSpeciality.DeleteDoctorSpecialtyAsync(doctorSpecialtyId, trackChanges);

        public async Task UpdateDoctorSpecialtyAsync(int doctorSpecialtyId, UpdateDoctorSpecialtyDto updateDoctorSpecialtyDto, bool trackChanges)
            => await _doctorSpeciality.UpdateDoctorSpecialtyAsync(doctorSpecialtyId, updateDoctorSpecialtyDto, trackChanges);

        // FamilyDoctorChanges
        public async Task<IEnumerable<FamilyDoctorChanges>> GetAllFamilyDoctorChangesAsync(bool trackChanges)
            => await _familyDoctorChange.GetAllFamilyDoctorChangesAsync(trackChanges);

        public async Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByPatientTCIdAsync(ulong patientTCId, bool trackChanges)
            => await _familyDoctorChange.GetFamilyDoctorChangesByPatientTCIdAsync(patientTCId, trackChanges);

        public async Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByDoctorCodeAsync(string doctorCode, bool trackChanges)
            => await _familyDoctorChange.GetFamilyDoctorChangesByDoctorCodeAsync(doctorCode, trackChanges);

        public async Task CreateFamilyDoctorChangeAsync(CreateFamilyDoctorChangeDto familyDoctorChangeDto, bool trackChanges)
            => await _familyDoctorChange.CreateFamilyDoctorChangeAsync(familyDoctorChangeDto, trackChanges);

        public async Task DeleteFamilyDoctorChangeAsync(int changeId, bool trackChanges)
            => await _familyDoctorChange.DeleteFamilyDoctorChangeAsync(changeId, trackChanges);

        // Patients
        public async Task<IEnumerable<Patients>> GetAllPatientsAsync(bool trackChanges)
            => await _patient.GetAllPatientsAsync(trackChanges);

        public async Task<Patients> GetPatientByPatientTCIdAsync(ulong patientTCId, bool trackChanges)
            => await _patient.GetPatientByPatientTCIdAsync(patientTCId, trackChanges);

        public async Task CreatePatientAsync(CreatePatientDto patientDto, bool trackChanges)
            => await _patient.CreatePatientAsync(patientDto, trackChanges);

        public async Task DeletePatientAsync(ulong patientTCId, bool trackChanges)
            => await _patient.DeletePatientAsync(patientTCId, trackChanges);

        public async Task UpdatePatientAsync(ulong patientTCId, UpdatePatientDto updatePatientDto, bool trackChanges)
            => await _patient.UpdatePatientAsync(patientTCId, updatePatientDto, trackChanges);
    }
}

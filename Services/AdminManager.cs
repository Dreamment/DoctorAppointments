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
        public async Task<IEnumerable<AppointmentMedications>> GetAllAppointmentMedications(bool trackChanges)
            => await _appointmentMedications.GetAllAppointmentMedications(trackChanges);

        public async Task<IEnumerable<AppointmentMedications>> GetAppointmentMedicationsByAppointmentCode(string appointmentCode, bool trackChanges)
            => await _appointmentMedications.GetAppointmentMedicationsByAppointmentCode(appointmentCode, trackChanges);

        public async Task<string> CreateAppointmentMedication(CreateMedicationDto medicationDto, bool trackChanges)
            => await _appointmentMedications.CreateAppointmentMedication(medicationDto, trackChanges);

        public async Task DeleteAppointmentMedication(string medicationCode, bool trackChanges)
            => await _appointmentMedications.DeleteAppointmentMedication(medicationCode, trackChanges);

        public async Task UpdateAppointmentMedication(string medicationCode, UpdateAppointmentMedicationDto updateAppointmentMedicationDto, bool trackChanges)
            => await _appointmentMedications.UpdateAppointmentMedication(medicationCode, updateAppointmentMedicationDto, trackChanges);

        // Appointments
        public async Task<IEnumerable<Appointments>> GetAllAppointments(bool trackChanges)
            => await _appointment.GetAllAppointments(trackChanges);

        public async Task<IEnumerable<Appointments>> GetAppointmentsByPatientTCId(ulong patientTCId, bool trackChanges)
            => await _appointment.GetAppointmentsByPatientTCId(patientTCId, trackChanges);

        public async Task<IEnumerable<Appointments>> GetAppointmentsByDoctorCode(string doctorCode, bool trackChanges)
            => await _appointment.GetAppointmentsByDoctorCode(doctorCode, trackChanges);

        public async Task<string> CreateAppointment(CreateAppointmentDto appointmentDto, bool trackChanges)
            => await _appointment.CreateAppointment(appointmentDto, trackChanges);

        public async Task DeleteAppointment(string appointmentCode, bool trackChanges)
            => await _appointment.DeleteAppointment(appointmentCode, trackChanges);

        public async Task UpdateAppointment(string appointmentCode, UpdateAppointmentDto updateAppointmentDto, bool trackChanges)
            => await _appointment.UpdateAppointment(appointmentCode, updateAppointmentDto, trackChanges);

        // Doctors
        public async Task<IEnumerable<Doctors>> GetAllDoctors(bool trackChanges)
            => await _doctor.GetAllDoctors(trackChanges);

        public async Task<Doctors> GetDoctorByDoctorCode(string doctorCode, bool trackChanges)
            => await _doctor.GetDoctorByDoctorCode(doctorCode, trackChanges);

        public async Task<string> CreateDoctor(CreateDoctorDto doctorDto, bool trackChanges)
            => await _doctor.CreateDoctor(doctorDto, trackChanges);

        public async Task DeleteDoctor(string doctorCode, bool trackChanges)
            => await _doctor.DeleteDoctor(doctorCode, trackChanges);

        public async Task UpdateDoctor(string doctorCode, UpdateDoctorDto updateDoctorDto, bool trackChanges)
            => await _doctor.UpdateDoctor(doctorCode, updateDoctorDto, trackChanges);

        // DoctorSpecialties
        public async Task<IEnumerable<DoctorSpecialties>> GetAllDoctorSpecialties(bool trackChanges)
            => await _doctorSpeciality.GetAllDoctorSpecialties(trackChanges);

        public async Task<DoctorSpecialties> GetDoctorSpecialtyByDoctorSpecialtyId(int doctorSpecialtyId, bool trackChanges)
            => await _doctorSpeciality.GetDoctorSpecialtyByDoctorSpecialtyId(doctorSpecialtyId, trackChanges);

        public async Task CreateDoctorSpecialty(CreateDoctorSpecialtyDto doctorSpecialtyDto, bool trackChanges)
            => await _doctorSpeciality.CreateDoctorSpecialty(doctorSpecialtyDto, trackChanges);

        public async Task DeleteDoctorSpecialty(int doctorSpecialtyId, bool trackChanges)
            => await _doctorSpeciality.DeleteDoctorSpecialty(doctorSpecialtyId, trackChanges);

        public async Task UpdateDoctorSpecialty(int doctorSpecialtyId, UpdateDoctorSpecialtyDto updateDoctorSpecialtyDto, bool trackChanges)
            => await _doctorSpeciality.UpdateDoctorSpecialty(doctorSpecialtyId, updateDoctorSpecialtyDto, trackChanges);

        // FamilyDoctorChanges
        public async Task<IEnumerable<FamilyDoctorChanges>> GetAllFamilyDoctorChanges(bool trackChanges)
            => await _familyDoctorChange.GetAllFamilyDoctorChanges(trackChanges);

        public async Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByPatientTCId(ulong patientTCId, bool trackChanges)
            => await _familyDoctorChange.GetFamilyDoctorChangesByPatientTCId(patientTCId, trackChanges);

        public async Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByDoctorCode(string doctorCode, bool trackChanges)
            => await _familyDoctorChange.GetFamilyDoctorChangesByDoctorCode(doctorCode, trackChanges);

        public async Task CreateFamilyDoctorChange(CreateFamilyDoctorChangeDto familyDoctorChangeDto, bool trackChanges)
            => await _familyDoctorChange.CreateFamilyDoctorChange(familyDoctorChangeDto, trackChanges);

        public async Task DeleteFamilyDoctorChange(int changeId, bool trackChanges)
            => await _familyDoctorChange.DeleteFamilyDoctorChange(changeId, trackChanges);

        // Patients
        public async Task<IEnumerable<Patients>> GetAllPatients(bool trackChanges)
            => await _patient.GetAllPatients(trackChanges);

        public async Task<Patients> GetPatientByPatientTCId(ulong patientTCId, bool trackChanges)
            => await _patient.GetPatientByPatientTCId(patientTCId, trackChanges);

        public async Task CreatePatient(CreatePatientDto patientDto, bool trackChanges)
            => await _patient.CreatePatient(patientDto, trackChanges);

        public async Task DeletePatient(ulong patientTCId, bool trackChanges)
            => await _patient.DeletePatient(patientTCId, trackChanges);

        public async Task UpdatePatient(ulong patientTCId, UpdatePatientDto updatePatientDto, bool trackChanges)
            => await _patient.UpdatePatient(patientTCId, updatePatientDto, trackChanges);
    }
}

using AutoMapper;
using Entities.DataTransferObjects.Create;
using Entities.DataTransferObjects.Update;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class AdminManager : IAdminService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AdminManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        // AppointmentMedications
        public async Task<IEnumerable<AppointmentMedications>> GetAllAppointmentMedicationsAsync(bool trackChanges)
            => await _repositoryManager.AppointmentMedication.GetAllAppointmentMedicationsAsync(trackChanges);

        public async Task<IEnumerable<AppointmentMedications>> GetAppointmentMedicationsByAppointmentCodeAsync(string appointmentCode, bool trackChanges)
        => await _repositoryManager.AppointmentMedication.GetAppointmentMedicationsByConditionAsync(
            m => m.AppointmentCode == appointmentCode, trackChanges);

        public async Task<string> CreateAppointmentMedicationAsync(CreateMedicationDto medicationDto, bool trackChanges)
        {
            var appointment = await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.AppointmentCode == medicationDto.AppointmentCode, trackChanges) ?? 
                throw new Exception("Appointment not found");
            var medication = _mapper.Map<AppointmentMedications>(medicationDto);
            var existingMedications = await _repositoryManager.AppointmentMedication.GetAppointmentMedicationsByConditionAsync(
                m => m.AppointmentCode == medicationDto.AppointmentCode, trackChanges);
            medication.MedicationCode = medicationDto.AppointmentCode + (existingMedications.Count() + 1).ToString();
            await _repositoryManager.AppointmentMedication.CreateAppointmentMedicationAsync(medication);
            await _repositoryManager.SaveAsync();
            return medication.MedicationCode;
        }

        public async Task DeleteAppointmentMedicationAsync(string medicationCode, bool trackChanges)
        {
            var medications = await _repositoryManager.AppointmentMedication.GetAppointmentMedicationsByConditionAsync(
                m => m.MedicationCode == medicationCode, trackChanges);
            var medication = medications.FirstOrDefault() ?? throw new Exception("Medication not found");
            await _repositoryManager.AppointmentMedication.DeleteAppointmentMedicationAsync(medication);
            await _repositoryManager.SaveAsync();
        }

        public async Task UpdateAppointmentMedicationAsync(
            string medicationCode, 
            UpdateAppointmentMedicationDto updateAppointmentMedicationDto, 
            bool trackChanges)
        {
            var medications = await _repositoryManager.AppointmentMedication.GetAppointmentMedicationsByConditionAsync(
                m => m.MedicationCode == medicationCode, trackChanges);
            var medication = medications.FirstOrDefault() ?? throw new Exception("Medication not found");
            _mapper.Map(updateAppointmentMedicationDto, medication);
            await _repositoryManager.AppointmentMedication.UpdateAppointmentMedicationAsync(medication);
            await _repositoryManager.SaveAsync();
        }

        // Appointments
        public async Task<IEnumerable<Appointments>> GetAllAppointmentsAsync(bool trackChanges)
            => await _repositoryManager.Appointment.GetAllAppointmentsAsync(trackChanges);

        public async Task<IEnumerable<Appointments>> GetAppointmentsByPatientTCIdAsync(ulong patientTCId, bool trackChanges)
            => await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.PatientTCId == patientTCId, trackChanges);

        public async Task<IEnumerable<Appointments>> GetAppointmentsByDoctorCodeAsync(string doctorCode, bool trackChanges)
            => await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.DoctorCode == doctorCode, trackChanges);

        public async Task<string> CreateAppointmentAsync(CreateAppointmentDto appointmentDto, bool trackChanges)
        {
            var appointment = _mapper.Map<Appointments>(appointmentDto);
            appointment.AppointmentCode = GenerateAppointmentCode();
            while (await IsAppointmentCodeExistAsync(appointment.AppointmentCode, trackChanges))
            {
                appointment.AppointmentCode = GenerateAppointmentCode();
            }
            await _repositoryManager.Appointment.CreateAppointmentAsync(appointment);
            await _repositoryManager.SaveAsync();
            return appointment.AppointmentCode;
        }

        public async Task DeleteAppointmentAsync(string appointmentCode, bool trackChanges)
        {
            var appointments = await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.AppointmentCode == appointmentCode, trackChanges);
            var appointment = appointments.FirstOrDefault() ?? throw new Exception("Appointment not found");
            await _repositoryManager.Appointment.DeleteAppointmentAsync(appointment);
            await _repositoryManager.SaveAsync();
        }

        public async Task UpdateAppointmentAsync(string appointmentCode, UpdateAppointmentDto updateAppointmentDto, bool trackChanges)
        {
            var appointments = await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.AppointmentCode == appointmentCode, trackChanges);
            var appointment = appointments.FirstOrDefault() ?? throw new Exception("Appointment not found");
            _mapper.Map(updateAppointmentDto, appointment);
            await _repositoryManager.Appointment.UpdateAppointmentAsync(appointment);
            await _repositoryManager.SaveAsync();
        }

        private static string GenerateAppointmentCode()
        {
            Random random = new();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 15).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<bool> IsAppointmentCodeExistAsync(string appointmentCode, bool trackChanges)
        {
            var appointment = await _repositoryManager.Appointment
                .GetAppointmentsByConditionAsync(a => a.AppointmentCode == appointmentCode, trackChanges);
            if (appointment == null)
                return false;
            return true;
        }

        // Doctors
        // eager loading
        public async Task<IEnumerable<Doctors>> GetAllDoctorsAsync(bool trackChanges)
            => await _repositoryManager.Doctor.GetAllDoctorsWithDetailsAsync(trackChanges, d => d.DoctorSpeciality, d => d.Appointments);

        public async Task<Doctors> GetDoctorByDoctorCodeAsync(string doctorCode, bool trackChanges)
        {
            var doctors = await _repositoryManager.Doctor.GetDoctorsByConditionAsync(
                d => d.DoctorCode == doctorCode, trackChanges);
            return doctors.FirstOrDefault();
        }

        // lazy loading
        public async Task<string> CreateDoctorAsync(CreateDoctorDto doctorDto, bool trackChanges)
        {
            var doctorSpecialties = await _repositoryManager.DoctorSpeciality.GetDoctorSpecialtiesByConditionAsync(
                ds => ds.DoctorSpecialityId == doctorDto.DoctorSpecialityId, trackChanges) ?? 
                throw new Exception("DoctorSpecialty not found");
            var doctor = _mapper.Map<Doctors>(doctorDto);
            doctor.DoctorCode = GenerateDoctorCode();
            while (await IsDoctorCodeExistAsync(doctor.DoctorCode, trackChanges))
            {
                doctor.DoctorCode = GenerateDoctorCode();
            }
            await _repositoryManager.Doctor.CreateDoctorAsync(doctor);
            await _repositoryManager.SaveAsync();
            return doctor.DoctorCode;
        }

        public async Task DeleteDoctorAsync(string doctorCode, bool trackChanges)
        {
            var doctors = await _repositoryManager.Doctor.GetDoctorsByConditionAsync(
                d => d.DoctorCode == doctorCode, trackChanges);
            var doctor = doctors.FirstOrDefault() ?? throw new Exception("Doctor not found");
            await _repositoryManager.Doctor.DeleteDoctorAsync(doctor);
            await _repositoryManager.SaveAsync();
        }

        public async Task UpdateDoctorAsync(string doctorCode, UpdateDoctorDto updateDoctorDto, bool trackChanges)
        {
            var doctorSpecialties = await _repositoryManager.DoctorSpeciality.GetDoctorSpecialtiesByConditionAsync(
                ds => ds.DoctorSpecialityId == updateDoctorDto.DoctorSpecialityId, trackChanges) ?? 
                throw new Exception("DoctorSpecialty not found");
            var doctors = await _repositoryManager.Doctor.GetDoctorsByConditionAsync(
                d => d.DoctorCode == doctorCode, trackChanges);
            var doctor = doctors.FirstOrDefault() ?? throw new Exception("Doctor not found");
            _mapper.Map(updateDoctorDto, doctor);
            await _repositoryManager.Doctor.UpdateDoctorAsync(doctor);
            await _repositoryManager.SaveAsync();
        }

        private static string GenerateDoctorCode()
        {
            Random random = new();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<bool> IsDoctorCodeExistAsync(string doctorCode, bool trackChanges)
        {
            var doctor = await _repositoryManager.Doctor
                .GetDoctorsByConditionAsync(d => d.DoctorCode == doctorCode, trackChanges);
            if (!doctor.Any())
                return false;
            return true;

        }

        // DoctorSpecialties
        public async Task<IEnumerable<DoctorSpecialties>> GetAllDoctorSpecialtiesAsync(bool trackChanges)
            => await _repositoryManager.DoctorSpeciality.GetAllDoctorSpecialtiesAsync(trackChanges);

        public async Task<DoctorSpecialties> GetDoctorSpecialtyByDoctorSpecialtyIdAsync(int doctorSpecialtyId, bool trackChanges)
        {
            var doctorSpecialties = await _repositoryManager.DoctorSpeciality.GetDoctorSpecialtiesByConditionAsync(
                ds => ds.DoctorSpecialityId == doctorSpecialtyId, trackChanges);
            return doctorSpecialties.FirstOrDefault();
        }

        public async Task CreateDoctorSpecialtyAsync(CreateDoctorSpecialtyDto doctorSpecialtyDto, bool trackChanges)
        {
            var doctorSpecialty = _mapper.Map<DoctorSpecialties>(doctorSpecialtyDto);
            await _repositoryManager.DoctorSpeciality.CreateDoctorSpecialtyAsync(doctorSpecialty);
            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteDoctorSpecialtyAsync(int doctorSpecialtyId, bool trackChanges)
        {
            if (doctorSpecialtyId == 1)
                throw new Exception("Family Doctor Speacilty cannot be deleted");
            if (doctorSpecialtyId == 2)
                throw new Exception("Other Specialty cannot be deleted");

            var doctorSpecialties = await _repositoryManager.DoctorSpeciality.GetDoctorSpecialtiesByConditionAsync(
                ds => ds.DoctorSpecialityId == doctorSpecialtyId, trackChanges);
            var doctorSpecialty = doctorSpecialties.FirstOrDefault() ?? throw new Exception("DoctorSpecialty not found");
            await _repositoryManager.DoctorSpeciality.DeleteDoctorSpecialtyAsync(doctorSpecialty);
            await _repositoryManager.SaveAsync();
        }

        public async Task UpdateDoctorSpecialtyAsync(int doctorSpecialtyId, UpdateDoctorSpecialtyDto updateDoctorSpecialtyDto, bool trackChanges)
        {
            if (doctorSpecialtyId == 1)
                throw new Exception("Family Doctor Speacilty cannot be updated");
            if (doctorSpecialtyId == 2)
                throw new Exception("Other Specialty cannot be updated");
            var doctorSpecialties = await _repositoryManager.DoctorSpeciality.GetDoctorSpecialtiesByConditionAsync(
                ds => ds.DoctorSpecialityId == doctorSpecialtyId, trackChanges);
            var doctorSpecialty = doctorSpecialties.FirstOrDefault() ?? throw new Exception("DoctorSpecialty not found");
            _mapper.Map(updateDoctorSpecialtyDto, doctorSpecialty);
            await _repositoryManager.DoctorSpeciality.UpdateDoctorSpecialtyAsync(doctorSpecialty);
            await _repositoryManager.SaveAsync();
        }

        // FamilyDoctorChanges
        public async Task<IEnumerable<FamilyDoctorChanges>> GetAllFamilyDoctorChangesAsync(bool trackChanges)
            => await _repositoryManager.FamilyDoctorChanges.GetAllFamilyDoctorChangesAsync(trackChanges);

        public async Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByPatientTCIdAsync(ulong patientTCId, bool trackChanges)
            => await _repositoryManager.FamilyDoctorChanges.GetFamilyDoctorChangesByConditionAsync(
                fdc => fdc.PatientTCId == patientTCId, trackChanges);

        public async Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByDoctorCodeAsync(string doctorCode, bool trackChanges)
            => await _repositoryManager.FamilyDoctorChanges.GetFamilyDoctorChangesByConditionAsync(
                fdc => fdc.NewFamilyDoctorCode == doctorCode || fdc.PreviousFamilyDoctorCode == doctorCode, trackChanges);

        // Patients
        public async Task<IEnumerable<Patients>> GetAllPatientsAsync(bool trackChanges)
            => await _repositoryManager.Patient.GetAllPatientsAsync(trackChanges);

        public async Task<Patients> GetPatientByPatientTCIdAsync(ulong patientTCId, bool trackChanges)
        {
            var patients = await _repositoryManager.Patient.GetPatientsByConditionAsync(
                p => p.PatientTCId == patientTCId, trackChanges);
            return patients.FirstOrDefault() == null ? throw new Exception("Patient not found") : patients.FirstOrDefault();
        }

        public async Task CreatePatientAsync(CreatePatientDto patientDto, bool trackChanges)
        {
            var patient = _mapper.Map<Patients>(patientDto);
            await _repositoryManager.Patient.CreatePatientAsync(patient);
            await _repositoryManager.SaveAsync();
        }

        public async Task DeletePatientAsync(ulong patientTCId, bool trackChanges)
        {
            var patients = await _repositoryManager.Patient.GetPatientsByConditionAsync(
                p => p.PatientTCId == patientTCId, trackChanges);
            var patient = patients.FirstOrDefault() ?? throw new Exception("Patient not found");
            await _repositoryManager.Patient.DeletePatientAsync(patient);
            await _repositoryManager.SaveAsync();
        }

        public async Task UpdatePatientAsync(ulong patientTCId, UpdatePatientDto updatePatientDto, bool trackChanges)
        {

            var patients = await _repositoryManager.Patient.GetPatientsByConditionAsync(
                p => p.PatientTCId == patientTCId, trackChanges);
            var patient = patients.FirstOrDefault() ?? throw new Exception("Patient not found");
            DateTime dateNow = DateTime.Now;
            updatePatientDto.PatientFamilyDoctorAppointDate = dateNow;

            // if old family doctor is null
            if (patient.PatientFamilyDoctorCode == null)
            {
                // if new family doctor is not null
                if (updatePatientDto.PatientFamilyDoctorCode != null)
                {
                    await _repositoryManager.FamilyDoctorChanges.CreateFamilyDoctorChangeAsync(
                        new FamilyDoctorChanges
                        {
                            PatientTCId = patientTCId,
                            PreviousFamilyDoctorCode = patient.PatientFamilyDoctorCode,
                            NewFamilyDoctorCode = null,
                            ChangeDate = dateNow,
                        });
                }
            }
            // if old family doctor is not null
            else
            {
                // if new family doctor is not null
                if (updatePatientDto.PatientFamilyDoctorCode != null)
                {
                    // if old family doctor is not equal to new family doctor
                    if (patient.PatientFamilyDoctorCode != updatePatientDto.PatientFamilyDoctorCode)
                    {
                        await _repositoryManager.FamilyDoctorChanges.CreateFamilyDoctorChangeAsync(
                            new FamilyDoctorChanges
                            {
                                PatientTCId = patientTCId,
                                PreviousFamilyDoctorCode = patient.PatientFamilyDoctorCode,
                                NewFamilyDoctorCode = updatePatientDto.PatientFamilyDoctorCode,
                                ChangeDate = dateNow
                            });
                    }
                }
            }
            _mapper.Map(updatePatientDto, patient);
            await _repositoryManager.Patient.UpdatePatientAsync(patient);
            await _repositoryManager.SaveAsync();
        }
    }
}

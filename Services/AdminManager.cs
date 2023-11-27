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
            => await _repositoryManager.AppointmentMedication.GetAllAppointmentMedicationsWithDetailsAsync(
                trackChanges, m => m.Appointment);

        public async Task<IEnumerable<AppointmentMedications>> GetAppointmentMedicationsByAppointmentCodeAsync(
            string appointmentCode, bool trackChanges)
        => await _repositoryManager.AppointmentMedication.GetAppointmentMedicationsByConditionWithDetailsAsync(
            m => m.AppointmentCode == appointmentCode, trackChanges, m => m.Appointment);

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
            var otherMedications = await _repositoryManager.AppointmentMedication.GetAppointmentMedicationsByConditionAsync(
                m => m.AppointmentCode == medication.AppointmentCode, trackChanges);
            int j = 1;
            foreach (var otherMedication in otherMedications)
            {
                if (otherMedication.MedicationCode == medicationCode)
                    continue;
                otherMedication.MedicationCode = medication.AppointmentCode + j.ToString();
                j++;
            }
            await _repositoryManager.AppointmentMedication.DeleteAppointmentMedicationAsync(medication);
            foreach (var otherMedication in otherMedications)
            {
                await _repositoryManager.AppointmentMedication.UpdateAppointmentMedicationAsync(otherMedication);
            }
            try
            {
                await _repositoryManager.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task UpdateAppointmentMedicationAsync(
            string medicationCode,
            UpdateAppointmentMedicationDto updateAppointmentMedicationDto,
            bool trackChanges)
        {
            var medications = await _repositoryManager.AppointmentMedication.GetAppointmentMedicationsByConditionAsync(
                m => m.MedicationCode == medicationCode, trackChanges);
            var medication = medications.FirstOrDefault() ?? throw new Exception("Medication not found");
            if (updateAppointmentMedicationDto.MedicationName != null)
                medication.MedicationName = updateAppointmentMedicationDto.MedicationName;
            if (updateAppointmentMedicationDto.Dosage != null)
                medication.Dosage = updateAppointmentMedicationDto.Dosage;
            if (updateAppointmentMedicationDto.UsageInstructions != null)
                medication.UsageInstructions = updateAppointmentMedicationDto.UsageInstructions;
            await _repositoryManager.AppointmentMedication.UpdateAppointmentMedicationAsync(medication);
            try
            {
                await _repositoryManager.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Appointments
        public async Task<IEnumerable<Appointments>> GetAllAppointmentsAsync(bool trackChanges)
            => await _repositoryManager.Appointment.GetAllAppointmentsWithDetailsAsync(trackChanges,
                a => a.Patient, a => a.Doctor, a => a.Medications);

        public async Task<IEnumerable<Appointments>> GetAppointmentsByPatientTCIdAsync(
            ulong patientTCId, bool trackChanges)
            => await _repositoryManager.Appointment.GetAppointmentsByConditionWithDetailsAsync(
                a => a.PatientTCId == patientTCId, trackChanges,
                a => a.Patient, a => a.Doctor, a => a.Medications);

        public async Task<IEnumerable<Appointments>> GetAppointmentsByDoctorCodeAsync(
            string doctorCode, bool trackChanges)
            => await _repositoryManager.Appointment.GetAppointmentsByConditionWithDetailsAsync(
                a => a.DoctorCode == doctorCode, trackChanges,
                a => a.Patient, a => a.Doctor, a => a.Medications);

        public async Task<string> CreateAppointmentAsync(
            CreateAppointmentDto appointmentDto, bool trackChanges)
        {
            if (appointmentDto.AppointmentDateTime < DateTime.Now)
                throw new Exception("Appointment date cannot be earlier than now");
            var patients = await _repositoryManager.Patient.GetPatientsByConditionAsync(p
                => p.PatientTCId == appointmentDto.PatientTCId, trackChanges);
            if (!patients.Any())
                throw new Exception("Patient not found");
            var doctors = await _repositoryManager.Doctor.GetDoctorsByConditionAsync(d =>
            d.DoctorCode == appointmentDto.DoctorCode, trackChanges);
            if (!doctors.Any())
                throw new Exception("Doctor not found");
            var appointment = _mapper.Map<Appointments>(appointmentDto);
            appointment.AppointmentCode = GenerateAppointmentCode();
            while (await IsAppointmentCodeExistAsync(appointment.AppointmentCode, trackChanges))
            {
                appointment.AppointmentCode = GenerateAppointmentCode();
            }
            await _repositoryManager.Appointment.CreateAppointmentAsync(appointment);
            try
            {
                await _repositoryManager.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return appointment.AppointmentCode;
        }

        public async Task DeleteAppointmentAsync(string appointmentCode, bool trackChanges)
        {
            var appointments = await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.AppointmentCode == appointmentCode, trackChanges);
            var appointment = appointments.FirstOrDefault() ?? throw new Exception("Appointment not found");

            var medications = await _repositoryManager.AppointmentMedication.GetAppointmentMedicationsByConditionAsync(
                m => m.AppointmentCode == appointmentCode, trackChanges);
            foreach (var medication in medications)
            {
                await _repositoryManager.AppointmentMedication.DeleteAppointmentMedicationAsync(medication);
            }
            await _repositoryManager.Appointment.DeleteAppointmentAsync(appointment);
            try
            {
                await _repositoryManager.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task UpdateAppointmentAsync(string appointmentCode,
            UpdateAppointmentDto updateAppointmentDto, bool trackChanges)
        {
            if (updateAppointmentDto.AppointmentDateTime < DateTime.Now)
                throw new Exception("Appointment date cannot be earlier than now");
            var appointments = await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.AppointmentCode == appointmentCode, trackChanges);
            var appointment = appointments.FirstOrDefault() ?? throw new Exception("Appointment not found");
            updateAppointmentDto.Status ??= appointment.Status;
            updateAppointmentDto.AppointmentDateTime ??= appointment.AppointmentDateTime;
            _mapper.Map(updateAppointmentDto, appointment);
            await _repositoryManager.Appointment.UpdateAppointmentAsync(appointment);
            try
            {
                await _repositoryManager.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
            if (!appointment.Any())
                return false;
            return true;
        }

        // Doctors
        // eager loading
        public async Task<IEnumerable<Doctors>> GetAllDoctorsAsync(bool trackChanges)
            => await _repositoryManager.Doctor.GetAllDoctorsWithDetailsAsync(
                trackChanges, d => d.DoctorSpeciality, d => d.Appointments);

        public async Task<Doctors> GetDoctorByDoctorCodeAsync(string doctorCode, bool trackChanges)
        {
            var doctors = await _repositoryManager.Doctor.GetDoctorsByConditionWithDetailsAsync(
                d => d.DoctorCode == doctorCode, trackChanges, d => d.DoctorSpeciality, d => d.Appointments);
            return doctors.FirstOrDefault();
        }

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
            try
            {
                await _repositoryManager.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return doctor.DoctorCode;
        }

        public async Task DeleteDoctorAsync(string doctorCode, bool trackChanges)
        {
            var doctors = await _repositoryManager.Doctor.GetDoctorsByConditionAsync(
                d => d.DoctorCode == doctorCode, trackChanges);
            var doctor = doctors.FirstOrDefault() ?? throw new Exception("Doctor not found");

            if (doctor.DoctorSpecialityId == 1)
            {
                var patients = await _repositoryManager.Patient.GetPatientsByConditionAsync(
                    p => p.PatientFamilyDoctorCode == doctorCode, trackChanges);
                if (patients.Any())
                {
                    foreach (var patient in patients)
                    {
                        await _repositoryManager.FamilyDoctorChanges.CreateFamilyDoctorChangeAsync(
                            new FamilyDoctorChanges
                            {
                                PatientTCId = patient.PatientTCId,
                                PreviousFamilyDoctorCode = patient.PatientFamilyDoctorCode,
                                NewFamilyDoctorCode = null,
                                ChangeDate = DateTime.Now
                            });
                        patient.PatientFamilyDoctorCode = null;
                        patient.PatientFamilyDoctorAppointDate = DateTime.Now;
                        await _repositoryManager.Patient.UpdatePatientAsync(patient);
                    }
                }
            }

            var FamilyDoctorChanges = await _repositoryManager.FamilyDoctorChanges.GetFamilyDoctorChangesByConditionAsync(
                fdc => fdc.NewFamilyDoctorCode == doctorCode || fdc.PreviousFamilyDoctorCode == doctorCode, trackChanges);

            foreach (var fdc in FamilyDoctorChanges)
            {
                await _repositoryManager.FamilyDoctorChanges.DeleteFamilyDoctorChangeAsync(fdc);
            }
            await _repositoryManager.Doctor.DeleteDoctorAsync(doctor);
            try
            {
                await _repositoryManager.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task UpdateDoctorAsync(string doctorCode, UpdateDoctorDto updateDoctorDto, bool trackChanges)
        {
            IEnumerable<DoctorSpecialties>? doctorSpecialties;
            if (updateDoctorDto.DoctorSpecialityId != null)
            {
                doctorSpecialties = await _repositoryManager.DoctorSpeciality.GetDoctorSpecialtiesByConditionAsync(
                    ds => ds.DoctorSpecialityId == updateDoctorDto.DoctorSpecialityId, trackChanges);
                if (!doctorSpecialties.Any())
                    throw new Exception("DoctorSpecialty not found");

            }
            var doctors = await _repositoryManager.Doctor.GetDoctorsByConditionAsync(
                d => d.DoctorCode == doctorCode, trackChanges);
            var doctor = doctors.FirstOrDefault() ?? throw new Exception("Doctor not found");

            DateTime dateNow = DateTime.Now;
            if (updateDoctorDto.DoctorSpecialityId != null && updateDoctorDto.DoctorSpecialityId != 1)
            {
                var patients = await _repositoryManager.Patient.GetPatientsByConditionAsync(
                    p => p.PatientFamilyDoctorCode == doctorCode, trackChanges);
                if (patients.Any())
                {
                    foreach (var patient in patients)
                    {
                        await _repositoryManager.FamilyDoctorChanges.CreateFamilyDoctorChangeAsync(
                            new FamilyDoctorChanges
                            {
                                PatientTCId = patient.PatientTCId,
                                PreviousFamilyDoctorCode = patient.PatientFamilyDoctorCode,
                                NewFamilyDoctorCode = null,
                                ChangeDate = dateNow
                            });
                        patient.PatientFamilyDoctorCode = null;
                        patient.PatientFamilyDoctorAppointDate = dateNow;
                        await _repositoryManager.Patient.UpdatePatientAsync(patient);
                    }
                }
            }

            if (updateDoctorDto.DoctorName != null)
                doctor.DoctorName = updateDoctorDto.DoctorName;
            if (updateDoctorDto.DoctorSurname != null)
                doctor.DoctorSurname = updateDoctorDto.DoctorSurname;
            if (updateDoctorDto.DoctorSpecialityId != null)
                doctor.DoctorSpecialityId = updateDoctorDto.DoctorSpecialityId.Value;
            // _mapper.Map(updateDoctorDto, doctor);
            await _repositoryManager.Doctor.UpdateDoctorAsync(doctor);
            try
            {
                await _repositoryManager.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
            try
            {
                await _repositoryManager.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
            try
            {
                await _repositoryManager.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task UpdateDoctorSpecialtyAsync(int doctorSpecialtyId,
            UpdateDoctorSpecialtyDto updateDoctorSpecialtyDto, bool trackChanges)
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
            try
            {
                await _repositoryManager.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // FamilyDoctorChanges
        public async Task<IEnumerable<FamilyDoctorChanges>> GetAllFamilyDoctorChangesAsync(bool trackChanges)
        {
            var FamilyDoctorChanges = await _repositoryManager.FamilyDoctorChanges.GetAllFamilyDoctorChangesAsync(trackChanges);

            foreach (var fdc in FamilyDoctorChanges)
            {
                if (fdc.PreviousFamilyDoctorCode != null)
                {
                    var doctors = await _repositoryManager.Doctor.GetDoctorsByConditionAsync(
                        d => d.DoctorCode == fdc.PreviousFamilyDoctorCode, trackChanges);
                    fdc.PreviousFamilyDoctor = doctors.FirstOrDefault();
                }
                if (fdc.NewFamilyDoctorCode != null)
                {
                    var doctors = await _repositoryManager.Doctor.GetDoctorsByConditionAsync(
                        d => d.DoctorCode == fdc.NewFamilyDoctorCode, trackChanges);
                    fdc.NewFamilyDoctor = doctors.FirstOrDefault();
                }
            }
            return FamilyDoctorChanges;
        }

        public async Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByPatientTCIdAsync(
            ulong patientTCId, bool trackChanges)
        {
            var FamilyDoctorChanges = await _repositoryManager.FamilyDoctorChanges.GetFamilyDoctorChangesByConditionAsync(
                        fdc => fdc.PatientTCId == patientTCId, trackChanges);

            foreach (var fdc in FamilyDoctorChanges)
            {
                if (fdc.PreviousFamilyDoctorCode != null)
                {
                    var doctors = await _repositoryManager.Doctor.GetDoctorsByConditionAsync(
                        d => d.DoctorCode == fdc.PreviousFamilyDoctorCode, trackChanges);
                    fdc.PreviousFamilyDoctor = doctors.FirstOrDefault();
                }
                if (fdc.NewFamilyDoctorCode != null)
                {
                    var doctors = await _repositoryManager.Doctor.GetDoctorsByConditionAsync(
                        d => d.DoctorCode == fdc.NewFamilyDoctorCode, trackChanges);
                    fdc.NewFamilyDoctor = doctors.FirstOrDefault();
                }
            }

            return FamilyDoctorChanges;
        }

        public async Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByDoctorCodeAsync(
            string doctorCode, bool trackChanges)
        {
            var FamilyDoctorChanges = await _repositoryManager.FamilyDoctorChanges.GetFamilyDoctorChangesByConditionAsync(
                fdc => fdc.NewFamilyDoctorCode == doctorCode || fdc.PreviousFamilyDoctorCode == doctorCode, trackChanges);

            foreach (var fdc in FamilyDoctorChanges)
            {
                if (fdc.PreviousFamilyDoctorCode != null)
                {
                    var doctors = await _repositoryManager.Doctor.GetDoctorsByConditionAsync(
                        d => d.DoctorCode == fdc.PreviousFamilyDoctorCode, trackChanges);
                    fdc.PreviousFamilyDoctor = doctors.FirstOrDefault();
                }
                if (fdc.NewFamilyDoctorCode != null)
                {
                    var doctors = await _repositoryManager.Doctor.GetDoctorsByConditionAsync(
                        d => d.DoctorCode == fdc.NewFamilyDoctorCode, trackChanges);
                    fdc.NewFamilyDoctor = doctors.FirstOrDefault();
                }
            }
            return FamilyDoctorChanges;
        }

        // Patients
        public async Task<IEnumerable<Patients>> GetAllPatientsAsync(bool trackChanges)
            => await _repositoryManager.Patient.GetAllPatientsWithDetailsAsync(trackChanges,
                p => p.FamilyDoctor, p => p.Appointments, p => p.FamilyDoctorChanges);

        public async Task<Patients> GetPatientByPatientTCIdAsync(ulong patientTCId, bool trackChanges)
        {
            var patients = await _repositoryManager.Patient.GettPatientsByConditionWithDetailsAsync(
                p => p.PatientTCId == patientTCId, trackChanges,
                p => p.FamilyDoctor, p => p.Appointments, p => p.FamilyDoctorChanges);
            return patients.FirstOrDefault() == null ? throw new Exception("Patient not found") : patients.FirstOrDefault();
        }

        public async Task CreatePatientAsync(CreatePatientDto patientDto, bool trackChanges)
        {
            DateTime dateNow = DateTime.Now;
            var patient = _mapper.Map<Patients>(patientDto);
            if (patient.PatientFamilyDoctorCode != null)
            {
                var doctors = await _repositoryManager.Doctor.GetDoctorsByConditionAsync(
                    d => d.DoctorCode == patient.PatientFamilyDoctorCode, trackChanges);
                if (!doctors.Any())
                    throw new Exception("Family Doctor not found");
                if (doctors.FirstOrDefault().DoctorSpecialityId != 1)
                    throw new Exception("Family Doctor must be Family Doctor Specialty");
                patient.PatientFamilyDoctorAppointDate = dateNow;
                await _repositoryManager.FamilyDoctorChanges.CreateFamilyDoctorChangeAsync(
                    new FamilyDoctorChanges
                    {
                        PatientTCId = patient.PatientTCId,
                        PreviousFamilyDoctorCode = null,
                        NewFamilyDoctorCode = patient.PatientFamilyDoctorCode,
                        ChangeDate = dateNow
                    });
            }
            await _repositoryManager.Patient.CreatePatientAsync(patient);
            try
            {
                await _repositoryManager.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeletePatientAsync(ulong patientTCId, bool trackChanges)
        {
            var patients = await _repositoryManager.Patient.GetPatientsByConditionAsync(
                p => p.PatientTCId == patientTCId, trackChanges);
            var patient = patients.FirstOrDefault() ?? throw new Exception("Patient not found");
            await _repositoryManager.Patient.DeletePatientAsync(patient);
            var familyDoctorChanges = await _repositoryManager.FamilyDoctorChanges.GetFamilyDoctorChangesByConditionAsync(
                fdc => fdc.PatientTCId == patientTCId, trackChanges);
            foreach (var fdc in familyDoctorChanges)
            {
                await _repositoryManager.FamilyDoctorChanges.DeleteFamilyDoctorChangeAsync(fdc);
            }
            try
            {
                await _repositoryManager.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task UpdatePatientAsync(ulong patientTCId, UpdatePatientDto updatePatientDto, bool trackChanges)
        {
            var patients = await _repositoryManager.Patient.GetPatientsByConditionAsync(
                p => p.PatientTCId == patientTCId, trackChanges);
            var patient = patients.FirstOrDefault() ?? throw new Exception("Patient not found");
            DateTime dateNow = DateTime.Now;

            if (updatePatientDto.PatientFamilyDoctorCode != "null")
            {
                // if old family doctor is null and new family doctor is not null
                if (patient.PatientFamilyDoctorCode == null && updatePatientDto.PatientFamilyDoctorCode != null)
                {
                    await _repositoryManager.FamilyDoctorChanges.CreateFamilyDoctorChangeAsync(
                        new FamilyDoctorChanges
                        {
                            PatientTCId = patientTCId,
                            PreviousFamilyDoctorCode = patient.PatientFamilyDoctorCode,
                            NewFamilyDoctorCode = updatePatientDto.PatientFamilyDoctorCode,
                            ChangeDate = dateNow,
                        });
                }
                // if old family doctor is not null and old family doctor is not equal to new family doctor
                else if (patient.PatientFamilyDoctorCode != null &&
                    patient.PatientFamilyDoctorCode != updatePatientDto.PatientFamilyDoctorCode)
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
            else
            {
                if (patient.PatientFamilyDoctorCode != null)
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
            if (updatePatientDto.PatientName != null)
                patient.PatientName = updatePatientDto.PatientName;
            if (updatePatientDto.PatientSurname != null)
                patient.PatientSurname = updatePatientDto.PatientSurname;
            if (updatePatientDto.PatientGender != null)
                patient.PatientGender = updatePatientDto.PatientGender;
            if (updatePatientDto.PatientBirthDate != null)
                patient.PatientBirthDate = updatePatientDto.PatientBirthDate.Value;
            if (updatePatientDto.PatientFamilyDoctorCode != null)
                patient.PatientFamilyDoctorCode = updatePatientDto.PatientFamilyDoctorCode;
            if (updatePatientDto.PatientFamilyDoctorCode == "null")
                patient.PatientFamilyDoctorCode = null;

            patient.PatientFamilyDoctorAppointDate = dateNow;
            await _repositoryManager.Patient.UpdatePatientAsync(patient);
            try
            {
                await _repositoryManager.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

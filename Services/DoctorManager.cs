using AutoMapper;
using Entities.DataTransferObjects;
using Repositories.Contracts;
using Services.Contracts;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Services
{
    public class DoctorManager : IDoctorService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public DoctorManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task ChangeAppointmentStatus(int doctorId, string appointmentCode, JsonPatchDocument<PartiallyUpdateAppointmentForDoctorDto> jsonPatch, bool trackChanges)
        {
            var appointment = await _repositoryManager.Appointment.GetAppointmentByCodeAsync(appointmentCode, trackChanges);
            if (appointment == null)
                throw new Exception("Appointment not found");
            var appointmentDto = _mapper.Map<PartiallyUpdateAppointmentForDoctorDto>(appointment);
            jsonPatch.ApplyTo(appointmentDto);
            var appointmentEntity = _mapper.Map<Appointments>(appointmentDto);
            await _repositoryManager.Appointment.UpdateAppointmentAsync(appointmentEntity);
            await _repositoryManager.SaveAsync();
        }

        public async Task<int> CreateMedicationAsync(int doctorId, CreateMedicationDto medicationDto, bool trackChanges)
        {
            var appointment = await _repositoryManager.Appointment.GetAppointmentByCodeAsync(medicationDto.AppointmentCode, trackChanges);
            if (appointment == null)
                throw new Exception("Appointment not found");
            var medication = _mapper.Map<AppointmentMedications>(medicationDto);
            medication.AppointmentId = appointment.AppointmentId;
            await _repositoryManager.AppointmentMedication.CreateAppointmentMedicationAsync(medication);
            await _repositoryManager.SaveAsync();
            return medication.MedicationId;
        }

        public async Task<IEnumerable<GetPatientsForFamilyDoctorDto>> GetPatientsAsync(int doctorId, bool trackChanges)
        {
            var patients = await _repositoryManager.Patient.GetPatientsByConditionAsync(
                p => p.PatientFamilyDoctorId == doctorId, trackChanges);
            if (patients == null)
                throw new Exception("You don't have any patient");
            IEnumerable<GetPatientsForFamilyDoctorDto> patientsDto = new List<GetPatientsForFamilyDoctorDto>();
            foreach (var patient in patients)
            {
                var patientDto =new GetPatientsForFamilyDoctorDto 
                { 
                    Id = patient.PatientId,
                    Name = patient.PatientName,
                    Surname = patient.PatientSurname,
                    Gender = patient.PatientGender,
                    BirthDate = DateOnly.FromDateTime(patient.PatientBirthDate).ToString("dd-MM-yyyy")
                };
                patientsDto = patientsDto.Append(patientDto);
            }
            return patientsDto;
        }

        public async Task<IEnumerable<GetAppointmentsForDoctorDto>> GetTodaysAppointmentsAsync(int doctorId, bool trackChanges)
        {
            var appointments = await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.AppointmentDateTime.Date == DateTime.Now.Date && a.DoctorId == doctorId, trackChanges);
            if (appointments == null)
                throw new Exception("You don't have any appointments today");
            var appointmentsDto = _mapper.Map<IEnumerable<GetAppointmentsForDoctorDto>>(appointments);

            for (int i = 0; i < appointmentsDto.Count(); i++)
            {
                int patientId = appointments.ElementAt(i).PatientId;
                appointmentsDto.ElementAt(i).Patient = 
                    _mapper.Map<GetPatientsForFamilyDoctorDto>(
                        await _repositoryManager.Patient.GetPatientByIdAsync(patientId, trackChanges));
                if (appointmentsDto.ElementAt(i).Patient == null)
                    throw new Exception("Patient not found");
                appointmentsDto.ElementAt(i).Patient.Id = i + 1;
                appointmentsDto.ElementAt(i).Medications = _mapper.Map<IEnumerable<GetMedicationsForAppointmentDto>>(
                    await _repositoryManager.AppointmentMedication.GetAppointmentMedicationsByConditionAsync(
                    m => m.AppointmentId == appointments.ElementAt(i).AppointmentId, trackChanges));
            }

            return appointmentsDto;
        }

        public async Task<IEnumerable<GetAppointmentsForDoctorDto>> GetUpcomingAppointmentsAsync(int doctorId, bool trackChanges)
        {
            var appointments = await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.AppointmentDateTime > DateTime.Now && a.DoctorId == doctorId, trackChanges);
            if (appointments == null)
                throw new Exception("You don't have any upcoming appointments");
            var appointmentsDto = _mapper.Map<IEnumerable<GetAppointmentsForDoctorDto>>(appointments);

            for(int i = 0; i < appointmentsDto.Count(); i++)
            {
                int patientId = appointments.ElementAt(i).PatientId;
                appointmentsDto.ElementAt(i).Patient =
                    _mapper.Map<GetPatientsForFamilyDoctorDto>(
                        await _repositoryManager.Patient.GetPatientByIdAsync(patientId, trackChanges));
                if (appointmentsDto.ElementAt(i).Patient == null)
                    throw new Exception("Patient not found");
                appointmentsDto.ElementAt(i).Patient.Id = i + 1;
                appointmentsDto.ElementAt(i).Medications = _mapper.Map<IEnumerable<GetMedicationsForAppointmentDto>>(
                    await _repositoryManager.AppointmentMedication.GetAppointmentMedicationsByConditionAsync(
                    m => m.AppointmentId == appointments.ElementAt(i).AppointmentId, trackChanges));
            }

            return appointmentsDto;
        }
    }
}

using AutoMapper;
using Repositories.Contracts;
using Services.Contracts;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Entities.DataTransferObjects.Create;
using Entities.DataTransferObjects.Get;
using Entities.DataTransferObjects.Update;

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

        public async Task ChangeAppointmentStatus(string doctorCode, 
            string appointmentCode, 
            JsonPatchDocument<PartiallyUpdateAppointmentForDoctorDto> jsonPatch, 
            bool trackChanges)
        {
            var appointment = await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.AppointmentCode == appointmentCode, trackChanges);
            if (appointment == null)
                throw new Exception("Appointment not found");
            var appointmentDto = _mapper.Map<PartiallyUpdateAppointmentForDoctorDto>(appointment);
            jsonPatch.ApplyTo(appointmentDto);
            var appointmentEntity = _mapper.Map<Appointments>(appointmentDto);
            await _repositoryManager.Appointment.UpdateAppointmentAsync(appointmentEntity);
            await _repositoryManager.SaveAsync();
        }

        public async Task<string> CreateMedicationAsync(string doctorCode, CreateMedicationDto medicationDto, bool trackChanges)
        {
            var appointment = await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.AppointmentCode ==  medicationDto.AppointmentCode, trackChanges);
            if (appointment == null)
                throw new Exception("Appointment not found");
            var medication = _mapper.Map<AppointmentMedications>(medicationDto);
            var existingMedication = await _repositoryManager.AppointmentMedication.GetAppointmentMedicationsByConditionAsync(
                               m => m.AppointmentCode == medicationDto.AppointmentCode, trackChanges);
            medication.MedicationCode = medicationDto.AppointmentCode + (existingMedication.Count() + 1);
            await _repositoryManager.AppointmentMedication.CreateAppointmentMedicationAsync(medication);
            await _repositoryManager.SaveAsync();
            return medication.MedicationCode;
        }

        public async Task<IEnumerable<GetAppointmentsForDoctorDto>> GetPastAppointmentsAsync(string doctorCode, bool trackChanges)
        {
            var appointments = await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                               a => a.AppointmentDateTime.Date < DateTime.Now.Date && a.DoctorCode == doctorCode, trackChanges);
            if (appointments == null)
                throw new Exception("You don't have any past appointments");
            IEnumerable<GetAppointmentsForDoctorDto> appointmentsDto = await GetAppointmentDetails(trackChanges, appointments);
            return appointmentsDto;
        }

        public async Task<IEnumerable<GetPatientsForFamilyDoctorDto>> GetPatientsAsync(string doctorCode, bool trackChanges)
        {
            var patients = await _repositoryManager.Patient.GetPatientsByConditionAsync(
                p => p.PatientFamilyDoctorCode == doctorCode, trackChanges);
            if (patients == null)
                throw new Exception("You don't have any patient");
            var patientsDto = _mapper.Map<IEnumerable<GetPatientsForFamilyDoctorDto>>(patients);
            for (int i = 0; i < patientsDto.Count(); i++)
            {
                patientsDto.ElementAt(i).BirthDate = DateOnly.FromDateTime(patients.ElementAt(i).PatientBirthDate);
            }
            return patientsDto;
        }

        public async Task<IEnumerable<GetAppointmentsForDoctorDto>> GetTodaysAppointmentsAsync(string doctorCode, bool trackChanges)
        {
            var appointments = await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.AppointmentDateTime.Date == DateTime.Now.Date && a.DoctorCode == doctorCode, trackChanges);
            if (appointments == null)
                throw new Exception("You don't have any past appointments");
            IEnumerable<GetAppointmentsForDoctorDto> appointmentsDto = await GetAppointmentDetails(trackChanges, appointments);
            return appointmentsDto;
        }

        public async Task<IEnumerable<GetAppointmentsForDoctorDto>> GetUpcomingAppointmentsAsync(string doctorCode, bool trackChanges)
        {
            var appointments = await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.AppointmentDateTime > DateTime.Now && a.DoctorCode == doctorCode, trackChanges);
            if (appointments == null)
                throw new Exception("You don't have any past appointments");
            IEnumerable<GetAppointmentsForDoctorDto> appointmentsDto = await GetAppointmentDetails(trackChanges, appointments);
            return appointmentsDto;
        }

        private async Task<IEnumerable<GetAppointmentsForDoctorDto>> GetAppointmentDetails(bool trackChanges, IEnumerable<Appointments> appointments)
        {
            var appointmentsDto = _mapper.Map<IEnumerable<GetAppointmentsForDoctorDto>>(appointments);
            for (int i = 0; i < appointmentsDto.Count(); i++)
            {
                appointmentsDto.ElementAt(i).Patient = _mapper.Map<GetPatientsForFamilyDoctorDto>(
                    await _repositoryManager.Patient.GetPatientsByConditionAsync(
                        p => p.PatientTCId == appointments.ElementAt(i).PatientTCId, trackChanges));

                if (appointmentsDto.ElementAt(i).Patient == null)
                    throw new Exception("Patient not found");

                appointmentsDto.ElementAt(i).Patient.Id = i + 1;

                appointmentsDto.ElementAt(i).Medications = _mapper.Map<IEnumerable<GetMedicationsForAppointmentDto>>(
                    await _repositoryManager.AppointmentMedication.GetAppointmentMedicationsByConditionAsync(
                        m => m.AppointmentCode == appointments.ElementAt(i).AppointmentCode, trackChanges));
            }

            return appointmentsDto;
        }
    }
}

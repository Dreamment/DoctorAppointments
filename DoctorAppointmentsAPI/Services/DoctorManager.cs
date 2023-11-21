﻿using AutoMapper;
using DoctorAppointmentsAPI.DataTransferObjects;
using DoctorAppointmentsAPI.Repositories.Contracts;
using DoctorAppointmentsAPI.Services.Contracts;
using DoctorAppointmentsDomain.Entities;

namespace DoctorAppointmentsAPI.Services
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

        public async Task ChangeAppointmentStatus(int doctorId, string appointmentCode, bool isApproved, bool trackChanges)
        {
            var appointment = await _repositoryManager.Appointment.GetAppointmentByCodeAsync(appointmentCode, trackChanges);
            if (appointment == null)
                throw new Exception("Appointment not found");
            appointment.Status = isApproved;
            await _repositoryManager.Appointment.UpdateAppointmentAsync(appointment);
            await _repositoryManager.SaveAsync();
            return;
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
            var patientsDto = _mapper.Map<IEnumerable<GetPatientsForFamilyDoctorDto>>(patients);
            int i = 1;
            foreach (var patient in patientsDto)
            {
                patient.Id = i;
                i++;
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
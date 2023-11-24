using AutoMapper;
using Entities.DataTransferObjects;
using Repositories.Contracts;
using Services.Contracts;
using Entities.Models;

namespace Services
{
    public class PatientManager : IPatientService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public PatientManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<string> CreateAppointmentAsync(CreateAppointmentDto appointmentDto, bool trackChanges)
        {
            var appointment = _mapper.Map<Appointments>(appointmentDto);
            appointment.AppointmentCode = GenerateAppointmentCode();
            while (await IsAppointmentCodeExist(appointment.AppointmentCode, trackChanges))
            {
                appointment.AppointmentCode = GenerateAppointmentCode();
            }
            await _repositoryManager.Appointment.CreateAppointmentAsync(appointment);
            await _repositoryManager.SaveAsync();
            return appointment.AppointmentCode;
        }

        public async Task<IEnumerable<GetAppointmentsForPatientDto>> GetAppointmentsForPatientAsync(int patientId, bool trackChanges)
        {
            var appointments = await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                               a => a.PatientId == patientId, trackChanges);
            if (appointments == null)
                throw new Exception("You don't have any appointment");
            var appointmentsDto = _mapper.Map<IEnumerable<GetAppointmentsForPatientDto>>(appointments);
            for (int i = 0; i < appointmentsDto.Count(); i++)
            {
                var doctor = await _repositoryManager.Doctor.GetDoctorByIdAsync(
                    appointments.ElementAt(i).DoctorId, trackChanges);
                appointmentsDto.ElementAt(i).id = i + 1;
                appointmentsDto.ElementAt(i).DoctorName = doctor.DoctorName;
                appointmentsDto.ElementAt(i).DoctorSurname = doctor.DoctorSurname;
                var doctorSpecialtiy = await _repositoryManager.DoctorSpeciality.GetDoctorSpecialtyByIdAsync(
                    doctor.DoctorSpecialityId, trackChanges);
                appointmentsDto.ElementAt(i).AppointmentType = doctorSpecialtiy.DoctorSpecialtyName + " Appointment";
                appointmentsDto.ElementAt(i).AppointmentDate = DateOnly.FromDateTime(appointments.ElementAt(i).AppointmentDateTime);
                appointmentsDto.ElementAt(i).AppointmentTime = TimeOnly.FromDateTime(appointments.ElementAt(i).AppointmentDateTime);
                if (appointments.ElementAt(i).AppointmentDateTime < DateTime.Now)
                    appointmentsDto.ElementAt(i).AppointmentStatus = "Past";
                else
                    appointmentsDto.ElementAt(i).AppointmentStatus = "Upcoming";
            }
            return appointmentsDto;
        }

        public async Task<GetFamilyDoctorDto> GetFamilyDoctorAsync(int patientId, bool trackChanges)
        {
            var patient = await _repositoryManager.Patient.GetPatientByIdAsync(patientId, trackChanges);
            if (patient == null)
                throw new Exception("Patient not found");
            var familyDoctor = await _repositoryManager.Doctor.GetDoctorByIdAsync((int)patient.PatientFamilyDoctorId, trackChanges);
            if (familyDoctor == null)
                throw new Exception("Family doctor not found");
            var familyDoctorDto = _mapper.Map<GetFamilyDoctorDto>(familyDoctor);
            return familyDoctorDto;
        }

        public async Task<IEnumerable<GetMedicationsForAppointmentDto>> GetMedicationsForAppointmentAsync(
            int patientId, string appointmentCode, bool trackChanges)
        {
            var appointment = await _repositoryManager.Appointment.GetAppointmentByCodeAsync(appointmentCode, trackChanges);
            if (appointment == null)
                throw new Exception("Appointment not found");
            if (appointment.PatientId != patientId)
                throw new Exception("You don't have permission to see this appointment");
            var medications = await _repositoryManager.AppointmentMedication.GetAppointmentMedicationsByConditionAsync(
                               m => m.AppointmentId == appointment.AppointmentId, trackChanges);
            if (medications == null)
                throw new Exception("No medications found");
            var medicationsDto = _mapper.Map<IEnumerable<GetMedicationsForAppointmentDto>>(medications);
            for (int i = 0; i < medicationsDto.Count(); i++)
            {
                medicationsDto.ElementAt(i).Id = i + 1;
            }
            return medicationsDto;
        }

        private static string GenerateAppointmentCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<bool> IsAppointmentCodeExist(string appointmentCode, bool trackChanges)
        {
            var appointment = await _repositoryManager.Appointment
                .GetAppointmentByCodeAsync(appointmentCode, trackChanges);
            if (appointment == null)
                return false;
            return true;

        }
    }
}

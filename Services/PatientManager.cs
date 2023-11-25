using AutoMapper;
using Repositories.Contracts;
using Services.Contracts;
using Entities.Models;
using Entities.DataTransferObjects.Create;
using Entities.DataTransferObjects.Get;

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

        public async Task<IEnumerable<GetAppointmentsForPatientDto>> GetAppointmentsForPatientAsync(ulong patientTCId, bool trackChanges)
        {
            var appointments = await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                               a => a.PatientTCId == patientTCId, trackChanges);
            if (appointments == null)
                throw new Exception("You don't have any appointment");
            var appointmentsDto = _mapper.Map<IEnumerable<GetAppointmentsForPatientDto>>(appointments);
            for (int i = 0; i < appointmentsDto.Count(); i++)
            {
                var doctors = await _repositoryManager.Doctor.GetDoctorsByConditionAsync( d => d.DoctorCode ==
                    appointments.ElementAt(i).DoctorCode, trackChanges);
                var doctor = doctors.FirstOrDefault();
                if (doctor == null)
                    throw new Exception("Doctor not found");
                appointmentsDto.ElementAt(i).id = i + 1;
                appointmentsDto.ElementAt(i).DoctorName = doctor.DoctorName;
                appointmentsDto.ElementAt(i).DoctorSurname = doctor.DoctorSurname;
                var doctorSpecialties = await _repositoryManager.DoctorSpeciality.GetDoctorSpecialtiesByConditionAsync(
                    ds => ds.DoctorSpecialityId == doctor.DoctorSpecialityId, trackChanges);
                var doctorSpecialtiy = doctorSpecialties.FirstOrDefault();
                if (doctorSpecialtiy == null)
                    throw new Exception("Doctor specialty not found");
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

        public async Task<GetFamilyDoctorDto> GetFamilyDoctorAsync(ulong patientTCId, bool trackChanges)
        {
            var patients = await _repositoryManager.Patient.GetPatientsByConditionAsync(
                p => p.PatientTCId == patientTCId, trackChanges);
            var patient = patients.FirstOrDefault();
            if (patient == null)
                throw new Exception("Patient not found");
            var familyDoctors = await _repositoryManager.Doctor.GetDoctorsByConditionAsync(
                d => d.DoctorCode ==  patient.PatientFamilyDoctorCode, trackChanges);
            var familyDoctor = familyDoctors.FirstOrDefault();
            if (familyDoctor == null)
                throw new Exception("Family doctor not found");
            var familyDoctorDto = _mapper.Map<GetFamilyDoctorDto>(familyDoctor);
            return familyDoctorDto;
        }

        public async Task<IEnumerable<GetMedicationsForAppointmentDto>> GetMedicationsForAppointmentAsync(
            ulong patientTCId, string appointmentCode, bool trackChanges)
        {
            var appointments = await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.AppointmentCode == appointmentCode, trackChanges);
            var appointment = appointments.FirstOrDefault();
            if (appointment == null)
                throw new Exception("Appointment not found");
            if (appointment.PatientTCId != patientTCId)
                throw new Exception("You don't have permission to see this appointment");
            var medications = await _repositoryManager.AppointmentMedication.GetAppointmentMedicationsByConditionAsync(
                               m => m.AppointmentCode == appointment.AppointmentCode, trackChanges);
            if (medications == null)
                throw new Exception("No medications found");
            var medicationsDto = _mapper.Map<IEnumerable<GetMedicationsForAppointmentDto>>(medications);
            return medicationsDto;
        }

        private static string GenerateAppointmentCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 15).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<bool> IsAppointmentCodeExist(string appointmentCode, bool trackChanges)
        {
            var appointment = await _repositoryManager.Appointment
                .GetAppointmentsByConditionAsync(a => a.AppointmentCode == appointmentCode, trackChanges);
            if (appointment == null)
                return false;
            return true;

        }
    }
}

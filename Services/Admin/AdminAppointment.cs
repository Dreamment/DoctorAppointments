using AutoMapper;
using Entities.DataTransferObjects.Create;
using Entities.DataTransferObjects.Update;
using Entities.Models;
using Repositories.Contracts;

namespace Services.Admin
{
    public class AdminAppointment
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AdminAppointment(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        internal async Task<string> CreateAppointmentAsync(CreateAppointmentDto appointmentDto, bool trackChanges)
        {
            var appointment = _mapper.Map<Appointments>(appointmentDto);
            appointment.AppointmentDateTime = DateTime.Now;
            appointment.AppointmentCode = GenerateAppointmentCode();
            while (await IsAppointmentCodeExistAsync(appointment.AppointmentCode, trackChanges))
            {
                appointment.AppointmentCode = GenerateAppointmentCode();
            }
            await _repositoryManager.Appointment.CreateAppointmentAsync(appointment);
            await _repositoryManager.SaveAsync();
            return appointment.AppointmentCode;
        }

        internal async Task DeleteAppointmentAsync(string appointmentCode, bool trackChanges)
        {
            var appointments = await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.AppointmentCode == appointmentCode, trackChanges);
            var appointment = appointments.FirstOrDefault();
            if (appointment == null)
                throw new Exception("Appointment not found");
            await _repositoryManager.Appointment.DeleteAppointmentAsync(appointment);
            await _repositoryManager.SaveAsync();
        }

        internal async Task<IEnumerable<Appointments>> GetAllAppointmentsAsync(bool trackChanges) 
            => await _repositoryManager.Appointment.GetAllAppointmentsAsync(trackChanges);

        internal async Task<IEnumerable<Appointments>> GetAppointmentsByDoctorCodeAsync(string doctorCode, bool trackChanges)
            => await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.DoctorCode == doctorCode, trackChanges);

        internal async Task<IEnumerable<Appointments>> GetAppointmentsByPatientTCIdAsync(ulong patientTCId, bool trackChanges)
            => await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.PatientTCId == patientTCId, trackChanges);

        internal async Task UpdateAppointmentAsync(string appointmentCode, UpdateAppointmentDto updateAppointmentDto, bool trackChanges)
        {
            var appointments = await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.AppointmentCode == appointmentCode, trackChanges);
            var appointment = appointments.FirstOrDefault();
            if (appointment == null)
                throw new Exception("Appointment not found");
            _mapper.Map(updateAppointmentDto, appointment);
            await _repositoryManager.SaveAsync();
        }

        private static string GenerateAppointmentCode()
        {
            Random random = new Random();
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
    }
}

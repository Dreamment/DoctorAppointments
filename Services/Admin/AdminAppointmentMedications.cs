using AutoMapper;
using Entities.DataTransferObjects.Create;
using Entities.DataTransferObjects.Update;
using Entities.Models;
using Repositories.Contracts;

namespace Services.Admin
{
    public class AdminAppointmentMedications
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AdminAppointmentMedications(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        internal async Task<string> CreateAppointmentMedication(CreateMedicationDto medicationDto, bool trackChanges)
        {
            var appointment = await _repositoryManager.Appointment.GetAppointmentsByConditionAsync(
                a => a.AppointmentCode == medicationDto.AppointmentCode, trackChanges);
            if (appointment == null)
                throw new Exception("Appointment not found");
            var medication = _mapper.Map<AppointmentMedications>(medicationDto);
            var existingMedication = await _repositoryManager.AppointmentMedication.GetAppointmentMedicationsByConditionAsync(
                m => m.AppointmentCode == medicationDto.AppointmentCode, trackChanges);
            medication.MedicationCode = medicationDto.AppointmentCode + (existingMedication.Count() + 1).ToString();
            await _repositoryManager.AppointmentMedication.CreateAppointmentMedicationAsync(medication);
            await _repositoryManager.SaveAsync();
            return medication.MedicationCode;
        }
        internal async Task DeleteAppointmentMedication(string medicationCode, bool trackChanges)
        {
            var medications = await _repositoryManager.AppointmentMedication.GetAppointmentMedicationsByConditionAsync(
                m => m.MedicationCode == medicationCode, trackChanges);
            var medication = medications.FirstOrDefault();
            if (medication == null)
                throw new Exception("Medication not found");
            await _repositoryManager.AppointmentMedication.DeleteAppointmentMedicationAsync(medication);
            await _repositoryManager.SaveAsync();
        }

        internal async Task<IEnumerable<AppointmentMedications>> GetAllAppointmentMedications(bool trackChanges)
            => await _repositoryManager.AppointmentMedication.GetAllAppointmentMedicationsAsync(trackChanges);

        internal async Task<IEnumerable<AppointmentMedications>> GetAppointmentMedicationsByAppointmentCode(string appointmentCode, bool trackChanges)
        => await _repositoryManager.AppointmentMedication.GetAppointmentMedicationsByConditionAsync(
            m => m.AppointmentCode == appointmentCode, trackChanges);
        internal async Task UpdateAppointmentMedication(string medicationCode, UpdateAppointmentMedicationDto updateAppointmentMedicationDto, bool trackChanges)
        {
            var medications = await _repositoryManager.AppointmentMedication.GetAppointmentMedicationsByConditionAsync(
                m => m.MedicationCode == medicationCode, trackChanges);
            var medication = medications.FirstOrDefault();
            if (medication == null)
                throw new Exception("Medication not found");
            _mapper.Map(updateAppointmentMedicationDto, medication);
            await _repositoryManager.SaveAsync();
        }
    }
}

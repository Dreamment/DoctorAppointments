using Entities.DataTransferObjects.Create;
using Entities.DataTransferObjects.Get;
using Entities.DataTransferObjects.Update;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Contracts
{
    public interface IDoctorService
    {
        Task<IEnumerable<GetPatientsForFamilyDoctorDto>> GetPatientsAsync(string doctorCode, bool trackChanges);
        Task<IEnumerable<GetAppointmentsForDoctorDto>> GetPastAppointmentsAsync(string doctorCode, bool trackChanges);
        Task<IEnumerable<GetAppointmentsForDoctorDto>> GetTodaysAppointmentsAsync(string doctorCode, bool trackChanges);
        Task<IEnumerable<GetAppointmentsForDoctorDto>> GetUpcomingAppointmentsAsync(string doctorCode, bool trackChanges);
        Task<string> CreateMedicationAsync(string doctorCode, CreateMedicationDto medicationDto, bool trackChanges);
        Task ChangeAppointmentStatus(string doctorCode, string appointmentCode, JsonPatchDocument<PartiallyUpdateAppointmentForDoctorDto> jsonPatch, bool trackChanges);
    }
}

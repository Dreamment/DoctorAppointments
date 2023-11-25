using Entities.DataTransferObjects;
using Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _manager;

        public PatientController(IPatientService manager)
        {
            _manager = manager;
        }

        [HttpGet("Appointments/{patientId:int}", Name = "GetAppointments")]
        public async Task<IActionResult> GetAppointmentsAsync([FromRoute(Name = "patientId")] ulong patientId)
        {
            var appointments = await _manager.GetAppointmentsForPatientAsync(patientId, false);
            if (appointments == null)
                return NotFound("You don't have any appointment.");
            return Ok(appointments);
        }

        [HttpGet("FamilyDoctor/{patientId:int}", Name = "GetFamilyDoctor")]
        public async Task<IActionResult> GetFamilyDoctorAsync([FromRoute(Name = "patientId")] ulong patientId)
        {
            var familyDoctor = await _manager.GetFamilyDoctorAsync(patientId, false);
            if (familyDoctor == null)
                return NotFound("You don't have a family doctor.");
            return Ok(familyDoctor);
        }

        [HttpGet("Medications/{patientId:int}/{appointmentCode}", Name = "GetMedications")]
        public async Task<IActionResult> GetMedicationsAsync([FromRoute(Name = "patientId")] ulong patientId, [FromRoute(Name = "appointmentCode")] string appointmentCode)
        {
            var medications = await _manager.GetMedicationsForAppointmentAsync(patientId, appointmentCode, false);
            if (medications == null)
                return NotFound($"You don't have any medication for {appointmentCode}");
            return Ok(medications);
        }

        [HttpPost("CreateAppointment", Name = "CreateAppointment")]
        public async Task<IActionResult> CreateAppointmentAsync([FromBody] CreateAppointmentDto appointmentDto)
        {
            var appointmentCode = await _manager.CreateAppointmentAsync(appointmentDto, false);
            return CreatedAtRoute("GetAppointment", new { appointmentCode }, appointmentDto);
        }

    }
}

using Services.Contracts;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects.Create;
using Entities.DataTransferObjects.Update;

namespace DoctorAppointmentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _manager;

        public DoctorController(IDoctorService manager)
        {
            _manager = manager;
        }

        [HttpGet("Patients/{doctorCode}", Name = "GetPatientsAsync")]
        public async Task<IActionResult> GetPatientsAsync([FromRoute(Name = "doctorCode")] string doctorCode)
        {
            var patients = await _manager.GetPatientsAsync(doctorCode, false);
            if (patients.Count() == 0)
                return NotFound("You don't have any patient.");
            return Ok(patients);
        }

        [HttpGet("PastAppointments/{doctorCode}", Name = "GetPastAppointmentsAsync")]
        public async Task<IActionResult> GetPastAppointmentsAsync([FromRoute(Name = "doctorCode")] string doctorCode)
        {
            var appointments = await _manager.GetPastAppointmentsAsync(doctorCode, false);
            if (appointments.Count() == 0)
                return NotFound("You don't have any past appointment.");
            return Ok(appointments);
        }

        [HttpGet("TodaysAppointments/{doctorCode}", Name = "GetTodaysAppointmentsAsync")]
        public async Task<IActionResult> GetTodaysAppointmentsAsync([FromRoute(Name = "doctorCode")] string doctorCode)
        {
            var appointments = await _manager.GetTodaysAppointmentsAsync(doctorCode, false);
            if (appointments.Count() == 0)
                return NotFound("You don't have any appointment today.");
            return Ok(appointments);
        }

        [HttpGet("UpcomingAppointments/{doctorCode}", Name = "GetUpcomingAppointmentsAsync")]
        public async Task<IActionResult> GetUpcomingAppointmentsAsync([FromRoute(Name = "doctorCode")] string doctorCode)
        {
            var appointments = await _manager.GetUpcomingAppointmentsAsync(doctorCode, false);
            if (appointments.Count() == 0)
                return NotFound("You don't have any upcoming appointment.");
            return Ok(appointments);
        }

        [HttpPost("WriteMedication/{doctorCode}", Name = "WriteMedicationAsync")]
        public async Task<IActionResult> WriteMedicationAsync([FromRoute(Name = "doctorCode")] string doctorCode, [FromBody] CreateMedicationDto medicationDto)
        {
            var medicationId = await _manager.CreateMedicationAsync(doctorCode, medicationDto, false);
            return CreatedAtRoute("GetMedicationAsync", new { medicationId }, medicationDto);
        }

        [HttpPatch("ChangeAppointmentStatus/{doctorCode}/{appointmentCode}", Name = "ChangeAppointmentStatusAsync")]
        public async Task<IActionResult> ChangeAppointmentStatusAsync([FromRoute(Name = "doctorCode")] string doctorCode,[FromRoute(Name = "appointmentCode")] string appointmentCode, [FromBody] JsonPatchDocument<PartiallyUpdateAppointmentForDoctorDto> jsonPatch)
        {
            var status = jsonPatch.Operations.FirstOrDefault().value.ToString();
            if (string.Equals(status, "true", StringComparison.OrdinalIgnoreCase) || string.Equals(status, "false", StringComparison.OrdinalIgnoreCase))
            {
                await _manager.ChangeAppointmentStatus(doctorCode, appointmentCode, jsonPatch, false);
                return NoContent();
            }
            return BadRequest("The status can only be true or false.");
        }

    }
}

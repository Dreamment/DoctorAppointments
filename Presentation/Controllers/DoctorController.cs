using Entities.DataTransferObjects;
using Services.Contracts;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("Patients/{doctorID:int}", Name = "GetPatientsAsync")]
        public async Task<IActionResult> GetPatientsAsync([FromRoute(Name = "doctorId")] int doctorId)
        {
            var patients = await _manager.GetPatientsAsync(doctorId, false);
            if (patients == null)
                return NotFound("You don't have any patient.");
            return Ok(patients);
        }

        [HttpGet("TodaysAppointments/{doctorID:int}", Name = "GetTodaysAppointmentsAsync")]
        public async Task<IActionResult> GetTodaysAppointmentsAsync([FromRoute(Name = "doctorId")] int doctorId)
        {
            var appointments = await _manager.GetTodaysAppointmentsAsync(doctorId, false);
            if (appointments == null)
                return NotFound("You don't have any appointment today.");
            return Ok(appointments);
        }

        [HttpGet("UpcomingAppointments/{doctorID:int}", Name = "GetUpcomingAppointmentsAsync")]
        public async Task<IActionResult> GetUpcomingAppointmentsAsync([FromRoute(Name = "doctorId")] int doctorId)
        {
            var appointments = await _manager.GetUpcomingAppointmentsAsync(doctorId, false);
            if (appointments == null)
                return NotFound("You don't have any upcoming appointment.");
            return Ok(appointments);
        }

        [HttpPost("WriteMedication/{doctorID:int}", Name = "WriteMedicationAsync")]
        public async Task<IActionResult> WriteMedicationAsync([FromRoute(Name = "doctorId")] int doctorId, [FromBody] CreateMedicationDto medicationDto)
        {
            var medicationId = await _manager.CreateMedicationAsync(doctorId, medicationDto, false);
            return CreatedAtRoute("GetMedicationAsync", new { medicationId }, medicationDto);
        }

        [HttpPatch("ChangeAppointmentStatus/{doctorID:int}/{appointmentCode}", Name = "ChangeAppointmentStatusAsync")]
        public async Task<IActionResult> ChangeAppointmentStatusAsync([FromRoute(Name = "doctorId")] int doctorId,[FromRoute(Name = "appointmentCode")] string appointmentCode, [FromBody] JsonPatchDocument<PartiallyUpdateAppointmentForDoctorDto> jsonPatch)
        {
            if(jsonPatch.Operations.Any(op => op.path.Equals("status", StringComparison.OrdinalIgnoreCase)))
            {
                if (jsonPatch.Operations.Count() > 1)
                    return BadRequest("You can only update the status of the appointment.");
                var status = jsonPatch.Operations.FirstOrDefault().value.ToString();
                if (string.Equals(status, "true", StringComparison.OrdinalIgnoreCase) || string.Equals(status, "false", StringComparison.OrdinalIgnoreCase))
                {
                    await _manager.ChangeAppointmentStatus(doctorId, appointmentCode, jsonPatch, false);
                    return NoContent();
                }
                return BadRequest("The status can only be true or false.");
            }
            return BadRequest("You can only update the status of the appointment.");
        }

    }
}

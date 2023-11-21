using DoctorAppointmentsAPI.DataTransferObjects;
using DoctorAppointmentsAPI.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public DoctorController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("Patients/{doctorID:int}", Name = "GetPatientsAsync")]
        public async Task<IActionResult> GetPatientsAsync([FromRoute(Name = "doctorId")] int doctorId)
        {
            var patients = await _serviceManager.Doctor.GetPatientsAsync(doctorId, false);
            if (patients == null)
                return NotFound("You don't have any patient.");
            return Ok(patients);
        }

        [HttpGet("TodaysAppointments/{doctorID:int}", Name = "GetTodaysAppointmentsAsync")]
        public async Task<IActionResult> GetTodaysAppointmentsAsync([FromRoute(Name = "doctorId")] int doctorId)
        {
            var appointments = await _serviceManager.Doctor.GetTodaysAppointmentsAsync(doctorId, false);
            if (appointments == null)
                return NotFound("You don't have any appointment today.");
            return Ok(appointments);
        }

        [HttpGet("UpcomingAppointments/{doctorID:int}", Name = "GetUpcomingAppointmentsAsync")]
        public async Task<IActionResult> GetUpcomingAppointmentsAsync([FromRoute(Name = "doctorId")] int doctorId)
        {
            var appointments = await _serviceManager.Doctor.GetUpcomingAppointmentsAsync(doctorId, false);
            if (appointments == null)
                return NotFound("You don't have any upcoming appointment.");
            return Ok(appointments);
        }

        [HttpPost("WriteMedication/{doctorID:int}", Name = "WriteMedicationAsync")]
        public async Task<IActionResult> WriteMedicationAsync([FromRoute(Name = "doctorId")] int doctorId, [FromBody] CreateMedicationDto medicationDto)
        {
            var medicationId = await _serviceManager.Doctor.CreateMedicationAsync(doctorId, medicationDto, false);
            return CreatedAtRoute("GetMedicationAsync", new { medicationId }, medicationDto);
        }

    }
}

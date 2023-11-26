using Entities.DataTransferObjects.Create;
using Entities.DataTransferObjects.Update;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAppointmentController : Controller
    {
        private readonly IAdminService _manager;

        public AdminAppointmentController(IAdminService manager)
        {
            _manager = manager;
        }

        [HttpGet(Name = "GetAllAppointmentsAsync")]
        public async Task<IActionResult> GetAllAppointmentsAsyn()
        {
            var appointments = await _manager.GetAllAppointmentsAsync(false);
            if (appointments == null)
                return NotFound("There is no appointment.");
            return Ok(appointments);
        }

        [HttpGet("DoctorCode/{doctorCode}", Name = "GetAppointmentsByDoctorCodeAsync")]
        public async Task<IActionResult> GetAppointmentsByDoctorCodeAsync(string doctorCode)
        {
            var appointments = await _manager.GetAppointmentsByDoctorCodeAsync(doctorCode, false);
            if (appointments == null)
                return NotFound("There is no appointment.");
            return Ok(appointments);
        }

        [HttpGet("PatientCode/{patientTCId:int}", Name = "GetAppointmentsByPatientTCIdAsync")]
        public async Task<IActionResult> GetAppointmentsByPatientTCIdAsync(ulong patientTCId)
        {
            var appointments = await _manager.GetAppointmentsByPatientTCIdAsync(patientTCId, false);
            if (appointments == null)
                return NotFound("There is no appointment.");
            return Ok(appointments);
        }

        [HttpPost(Name = "CreateAppointmentAsync")]
        public async Task<IActionResult> CreateAppointmentAsync([FromBody] CreateAppointmentDto appointmentDto)
        {
            if (appointmentDto == null)
                return BadRequest("Appointment is null.");
            var appointmentCode = await _manager.CreateAppointmentAsync(appointmentDto, false);
            return CreatedAtRoute("GetAllAppointmentsAsync", new { appointmentCode }, appointmentCode);
        }

        [HttpPut("{appointmentCode}", Name = "UpdateAppointmentAsync")]
        public async Task<IActionResult> UpdateAppointmentAsync(string appointmentCode, [FromBody] UpdateAppointmentDto updateAppointmentDto)
        {
            if (updateAppointmentDto == null)
                return BadRequest("Appointment is null.");
            await _manager.UpdateAppointmentAsync(appointmentCode, updateAppointmentDto, false);
            return NoContent();
        }

        [HttpDelete("{appointmentCode}", Name = "DeleteAppointmentAsync")]
        public async Task<IActionResult> DeleteAppointmentAsync(string appointmentCode)
        {
            await _manager.DeleteAppointmentAsync(appointmentCode, false);
            return NoContent();
        }
    }
}

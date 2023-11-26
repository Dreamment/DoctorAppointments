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

    [Route("api/[controller]")]
    [ApiController]
    public class AdminMedicationController : Controller
    {
        private readonly IAdminService _manager;

        public AdminMedicationController(IAdminService manager)
        {
            _manager = manager;
        }

        [HttpGet(Name = "GetAllMedicationsAsync")]
        public async Task<IActionResult> GetAllMedicationsAsync()
        {
            var medications = await _manager.GetAllAppointmentMedicationsAsync(false);
            if (medications == null)
                return NotFound("There is no medication.");
            return Ok(medications);
        }

        [HttpGet("{appointmentCode}", Name = "GetMedicationByAppointmentCodeAsync")]
        public async Task<IActionResult> GetMedicationByAppointmentCodeAsync(string appointmentCode)
        {
            var medications = await _manager.GetAppointmentMedicationsByAppointmentCodeAsync(appointmentCode, false);
            if (medications == null)
                return NotFound("There is no medication.");
            return Ok(medications);
        }

        [HttpPost(Name = "CreateMedicationAsync")]
        public async Task<IActionResult> CreateMedicationAsync([FromBody] CreateMedicationDto medicationDto)
        {
            if (medicationDto == null)
                return BadRequest("Medication is null.");
            var medicationCode = await _manager.CreateAppointmentMedicationAsync(medicationDto, false);
            return CreatedAtRoute("GetAllMedicationsAsync", new { medicationCode }, medicationCode);
        }

        [HttpPut("{medicationCode}", Name = "UpdateMedicationAsync")]
        public async Task<IActionResult> UpdateMedicationAsync(string medicationCode, [FromBody] UpdateAppointmentMedicationDto updateAppointmentMedicationDto)
        {
            if (updateAppointmentMedicationDto == null)
                return BadRequest("Medication is null.");
            await _manager.UpdateAppointmentMedicationAsync(medicationCode, updateAppointmentMedicationDto, false);
            return NoContent();
        }

        [HttpDelete("{medicationCode}", Name = "DeleteMedicationAsync")]
        public async Task<IActionResult> DeleteMedicationAsync(string medicationCode)
        {
            await _manager.DeleteAppointmentMedicationAsync(medicationCode, false);
            return NoContent();
        }
    }
}

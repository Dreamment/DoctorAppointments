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

    [Route("api/[controller]")]
    [ApiController]
    public class AdminDoctorController : Controller
    {
        private readonly IAdminService _manager;

        public AdminDoctorController(IAdminService manager)
        {
            _manager = manager;
        }

        [HttpGet(Name = "GetAllDoctorsAsync")]
        public async Task<IActionResult> GetAllDoctorsAsync()
        {
            var doctors = await _manager.GetAllDoctorsAsync(false);
            if (doctors == null)
                return NotFound("There is no doctor.");
            return Ok(doctors);
        }

        [HttpGet("{doctorCode}", Name = "GetDoctorByDoctorCodeAsync")]
        public async Task<IActionResult> GetDoctorByDoctorCodeAsync(string doctorCode)
        {
            var doctor = await _manager.GetDoctorByDoctorCodeAsync(doctorCode, false);
            if (doctor == null)
                return NotFound("There is no doctor.");
            return Ok(doctor);
        }

        [HttpPost(Name = "CreateDoctorAsync")]
        public async Task<IActionResult> CreateDoctorAsync([FromBody] CreateDoctorDto doctorDto)
        {
            if (doctorDto == null)
                return BadRequest("Doctor is null.");
            var doctorCode = await _manager.CreateDoctorAsync(doctorDto, false);
            return CreatedAtRoute("GetAllDoctorsAsync", new { doctorCode }, doctorCode);
        }

        [HttpPut("{doctorCode}", Name = "UpdateDoctorAsync")]
        public async Task<IActionResult> UpdateDoctorAsync(string doctorCode, [FromBody] UpdateDoctorDto updateDoctorDto)
        {
            if (updateDoctorDto == null)
                return BadRequest("Doctor is null.");
            await _manager.UpdateDoctorAsync(doctorCode, updateDoctorDto, false);
            return NoContent();
        }

        [HttpDelete("{doctorCode}", Name = "DeleteDoctorAsync")]
        public async Task<IActionResult> DeleteDoctorAsync(string doctorCode)
        {
            await _manager.DeleteDoctorAsync(doctorCode, false);
            return NoContent();
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AdminDoctorSpecialtyController : Controller
    {
        private readonly IAdminService _manager;

        public AdminDoctorSpecialtyController(IAdminService manager)
        {
            _manager = manager;
        }

        [HttpGet(Name = "GetAllDoctorSpecialtiesAsync")]
        public async Task<IActionResult> GetAllDoctorSpecialtiesAsync()
        {
            var doctorSpecialties = await _manager.GetAllDoctorSpecialtiesAsync(false);
            if (doctorSpecialties == null)
                return NotFound("There is no doctor specialty.");
            return Ok(doctorSpecialties);
        }

        [HttpGet("{doctorSpecialtyId:int}", Name = "GetDoctorSpecialtyByDoctorSpecialtyIdAsync")]
        public async Task<IActionResult> GetDoctorSpecialtyByDoctorSpecialtyIdAsync(int doctorSpecialtyId)
        {
            var doctorSpecialty = await _manager.GetDoctorSpecialtyByDoctorSpecialtyIdAsync(doctorSpecialtyId, false);
            if (doctorSpecialty == null)
                return NotFound("There is no doctor specialty.");
            return Ok(doctorSpecialty);
        }

        [HttpPost(Name = "CreateDoctorSpecialtyAsync")]
        public async Task<IActionResult> CreateDoctorSpecialtyAsync([FromBody] CreateDoctorSpecialtyDto doctorSpecialtyDto)
        {
            if (doctorSpecialtyDto == null)
                return BadRequest("Doctor specialty is null.");
            await _manager.CreateDoctorSpecialtyAsync(doctorSpecialtyDto, false);
            return NoContent();
        }

        [HttpPut("{doctorSpecialtyId:int}", Name = "UpdateDoctorSpecialtyAsync")]
        public async Task<IActionResult> UpdateDoctorSpecialtyAsync(int doctorSpecialtyId, [FromBody] UpdateDoctorSpecialtyDto updateDoctorSpecialtyDto)
        {
            if (updateDoctorSpecialtyDto == null)
                return BadRequest("Doctor specialty is null.");
            await _manager.UpdateDoctorSpecialtyAsync(doctorSpecialtyId, updateDoctorSpecialtyDto, false);
            return NoContent();
        }

        [HttpDelete("{doctorSpecialtyId:int}", Name = "DeleteDoctorSpecialtyAsync")]
        public async Task<IActionResult> DeleteDoctorSpecialtyAsync(int doctorSpecialtyId)
        {
            await _manager.DeleteDoctorSpecialtyAsync(doctorSpecialtyId, false);
            return NoContent();
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AdminFamilyDoctorChangesController : Controller
    {
        private readonly IAdminService _manager;

        public AdminFamilyDoctorChangesController(IAdminService manager)
        {
            _manager = manager;
        }

        [HttpGet(Name = "GetAllFamilyDoctorChangesAsync")]
        public async Task<IActionResult> GetAllFamilyDoctorChangesAsync()
        {
            var familyDoctorChanges = await _manager.GetAllFamilyDoctorChangesAsync(false);
            if (familyDoctorChanges == null)
                return NotFound("There is no family doctor change.");
            return Ok(familyDoctorChanges);
        }

        [HttpGet("{PatientTCId:int}", Name = "GetFamilyDoctorChangesPatientTCIdAsync")]
        public async Task<IActionResult> GetFamilyDoctorChangesPatientTCIdAsync(ulong patientTCId)
        {
            var familyDoctorChanges = await _manager.GetFamilyDoctorChangesByPatientTCIdAsync(patientTCId, false);
            if (familyDoctorChanges == null)
                return NotFound("There is no family doctor change.");
            return Ok(familyDoctorChanges);
        }

        [HttpGet("{DoctorCode}", Name = "GetFamilyDoctorChangesDoctorCodeAsync")]
        public async Task<IActionResult> GetFamilyDoctorChangesDoctorCodeAsync(string doctorCode)
        {
            var familyDoctorChanges = await _manager.GetFamilyDoctorChangesByDoctorCodeAsync(doctorCode, false);
            if (familyDoctorChanges == null)
                return NotFound("There is no family doctor change.");
            return Ok(familyDoctorChanges);
        }
    }
}

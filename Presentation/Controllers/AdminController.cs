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
            try
            {
                var appointments = await _manager.GetAllAppointmentsAsync(false);
                if (appointments.Count() == 0)
                    return NotFound("There is no appointment.");
                return Ok(appointments);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("DoctorCode/{doctorCode}", Name = "GetAppointmentsByDoctorCodeAsync")]
        public async Task<IActionResult> GetAppointmentsByDoctorCodeAsync(string doctorCode)
        {
            try
            {
                var appointments = await _manager.GetAppointmentsByDoctorCodeAsync(doctorCode, false);
                if (appointments.Count() == 0)
                    return NotFound("There is no appointment.");
                return Ok(appointments);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("PatientTCId/{patientTCId}", Name = "GetAppointmentsByPatientTCIdAsync")]
        public async Task<IActionResult> GetAppointmentsByPatientTCIdAsync(ulong patientTCId)
        {
            try
            {
                var appointments = await _manager.GetAppointmentsByPatientTCIdAsync(patientTCId, false);
                if (appointments.Count() == 0)
                    return NotFound("There is no appointment.");
                return Ok(appointments);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost(Name = "CreateAppointmentAsync")]
        public async Task<IActionResult> CreateAppointmentAsync([FromBody] CreateAppointmentDto appointmentDto)
        {
            try
            {
                if (appointmentDto == null)
                    return BadRequest("Appointment is null.");
                var appointmentCode = await _manager.CreateAppointmentAsync(appointmentDto, false);
                return CreatedAtRoute("GetAllAppointmentsAsync", new { appointmentCode }, appointmentCode);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{appointmentCode}", Name = "UpdateAppointmentAsync")]
        public async Task<IActionResult> UpdateAppointmentAsync(string appointmentCode, [FromBody] UpdateAppointmentDto updateAppointmentDto)
        {
            try
            {
                if (updateAppointmentDto == null)
                    return BadRequest("Appointment is null.");
                await _manager.UpdateAppointmentAsync(appointmentCode, updateAppointmentDto, false);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{appointmentCode}", Name = "DeleteAppointmentAsync")]
        public async Task<IActionResult> DeleteAppointmentAsync(string appointmentCode)
        {
            try
            {
                await _manager.DeleteAppointmentAsync(appointmentCode, false);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
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
            try
            {
                var medications = await _manager.GetAllAppointmentMedicationsAsync(false);
                if (medications.Count() == 0)
                    return NotFound("There is no medication.");
                return Ok(medications);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{appointmentCode}", Name = "GetMedicationByAppointmentCodeAsync")]
        public async Task<IActionResult> GetMedicationByAppointmentCodeAsync(string appointmentCode)
        {
            try
            {
                var medications = await _manager.GetAppointmentMedicationsByAppointmentCodeAsync(appointmentCode, false);
                if (medications.Count() == 0)
                    return NotFound("There is no medication.");
                return Ok(medications);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost(Name = "CreateMedicationAsync")]
        public async Task<IActionResult> CreateMedicationAsync([FromBody] CreateMedicationDto medicationDto)
        {
            try
            {
                if (medicationDto == null)
                    return BadRequest("Medication is null.");
                var medicationCode = await _manager.CreateAppointmentMedicationAsync(medicationDto, false);
                return CreatedAtRoute("GetAllMedicationsAsync", new { medicationCode }, medicationCode);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{medicationCode}", Name = "UpdateMedicationAsync")]
        public async Task<IActionResult> UpdateMedicationAsync(string medicationCode, [FromBody] UpdateAppointmentMedicationDto updateAppointmentMedicationDto)
        {
            try
            {
                if (updateAppointmentMedicationDto == null)
                    return BadRequest("Medication is null.");
                await _manager.UpdateAppointmentMedicationAsync(medicationCode, updateAppointmentMedicationDto, false);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{medicationCode}", Name = "DeleteMedicationAsync")]
        public async Task<IActionResult> DeleteMedicationAsync(string medicationCode)
        {
            try
            {
                await _manager.DeleteAppointmentMedicationAsync(medicationCode, false);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
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
            try
            {
                var doctors = await _manager.GetAllDoctorsAsync(false);
                if (doctors.Count() == 0)
                    return NotFound("There is no doctor.");
                return Ok(doctors);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{doctorCode}", Name = "GetDoctorByDoctorCodeAsync")]
        public async Task<IActionResult> GetDoctorByDoctorCodeAsync(string doctorCode)
        {
            try
            {
                var doctor = await _manager.GetDoctorByDoctorCodeAsync(doctorCode, false);
                if (doctor == null)
                    return NotFound("There is no doctor.");
                return Ok(doctor);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost(Name = "CreateDoctorAsync")]
        public async Task<IActionResult> CreateDoctorAsync([FromBody] CreateDoctorDto doctorDto)
        {
            try
            {
                if (doctorDto == null)
                    return BadRequest("Doctor is null.");
                var doctorCode = await _manager.CreateDoctorAsync(doctorDto, false);
                return CreatedAtRoute("GetAllDoctorsAsync", new { doctorCode }, doctorCode);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{doctorCode}", Name = "UpdateDoctorAsync")]
        public async Task<IActionResult> UpdateDoctorAsync(string doctorCode, [FromBody] UpdateDoctorDto updateDoctorDto)
        {
            try
            {
                if (updateDoctorDto == null)
                    return BadRequest("Doctor is null.");
                await _manager.UpdateDoctorAsync(doctorCode, updateDoctorDto, false);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{doctorCode}", Name = "DeleteDoctorAsync")]
        public async Task<IActionResult> DeleteDoctorAsync(string doctorCode)
        {
            try
            {
                await _manager.DeleteDoctorAsync(doctorCode, false);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
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
            try
            {
                var doctorSpecialties = await _manager.GetAllDoctorSpecialtiesAsync(false);
                if (doctorSpecialties.Count() == 0)
                    return NotFound("There is no doctor specialty.");
                return Ok(doctorSpecialties);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{doctorSpecialtyId:int}", Name = "GetDoctorSpecialtyByDoctorSpecialtyIdAsync")]
        public async Task<IActionResult> GetDoctorSpecialtyByDoctorSpecialtyIdAsync(int doctorSpecialtyId)
        {
            try
            {
                var doctorSpecialty = await _manager.GetDoctorSpecialtyByDoctorSpecialtyIdAsync(doctorSpecialtyId, false);
                if (doctorSpecialty == null)
                    return NotFound("There is no doctor specialty.");
                return Ok(doctorSpecialty);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost(Name = "CreateDoctorSpecialtyAsync")]
        public async Task<IActionResult> CreateDoctorSpecialtyAsync([FromBody] CreateDoctorSpecialtyDto doctorSpecialtyDto)
        {
            try
            {
                if (doctorSpecialtyDto == null)
                    return BadRequest("Doctor specialty is null.");
                await _manager.CreateDoctorSpecialtyAsync(doctorSpecialtyDto, false);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{doctorSpecialtyId:int}", Name = "UpdateDoctorSpecialtyAsync")]
        public async Task<IActionResult> UpdateDoctorSpecialtyAsync(int doctorSpecialtyId, [FromBody] UpdateDoctorSpecialtyDto updateDoctorSpecialtyDto)
        {
            try
            {
                if (updateDoctorSpecialtyDto == null)
                    return BadRequest("Doctor specialty is null.");
                await _manager.UpdateDoctorSpecialtyAsync(doctorSpecialtyId, updateDoctorSpecialtyDto, false);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{doctorSpecialtyId:int}", Name = "DeleteDoctorSpecialtyAsync")]
        public async Task<IActionResult> DeleteDoctorSpecialtyAsync(int doctorSpecialtyId)
        {
            try
            {
                await _manager.DeleteDoctorSpecialtyAsync(doctorSpecialtyId, false);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
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
            try
            {
                var familyDoctorChanges = await _manager.GetAllFamilyDoctorChangesAsync(false);
                if (!familyDoctorChanges.Any())
                    return NotFound("There is no family doctor change.");
                return Ok(familyDoctorChanges);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("PatientTCId/{PatientTCId}", Name = "GetFamilyDoctorChangesPatientTCIdAsync")]
        public async Task<IActionResult> GetFamilyDoctorChangesPatientTCIdAsync(ulong patientTCId)
        {
            try
            {
                var familyDoctorChanges = await _manager.GetFamilyDoctorChangesByPatientTCIdAsync(patientTCId, false);
                if (familyDoctorChanges.Count() == 0)
                    return NotFound("There is no family doctor change.");
                return Ok(familyDoctorChanges);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("DoctorCode/{DoctorCode}", Name = "GetFamilyDoctorChangesDoctorCodeAsync")]
        public async Task<IActionResult> GetFamilyDoctorChangesDoctorCodeAsync(string doctorCode)
        {
            try
            {
                var familyDoctorChanges = await _manager.GetFamilyDoctorChangesByDoctorCodeAsync(doctorCode, false);
                if (familyDoctorChanges.Count() == 0)
                    return NotFound("There is no family doctor change.");
                return Ok(familyDoctorChanges);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AdminPatientController : Controller
    {
        private readonly IAdminService _manager;

        public AdminPatientController(IAdminService manager)
        {
            _manager = manager;
        }

        [HttpGet(Name = "GetAllPatientsAsync")]
        public async Task<IActionResult> GetAllPatientsAsync()
        {
            try
            {
                var patients = await _manager.GetAllPatientsAsync(false);
                if (patients.Count() == 0)
                    return NotFound("There is no patient.");
                return Ok(patients);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{patientTCId}", Name = "GetPatientByPatientTCIdAsync")]
        public async Task<IActionResult> GetPatientByPatientTCIdAsync(ulong patientTCId)
        {
            try
            {
                var patient = await _manager.GetPatientByPatientTCIdAsync(patientTCId, false);
                if (patient == null)
                    return NotFound("There is no patient.");
                return Ok(patient);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost(Name = "CreatePatientAsync")]
        public async Task<IActionResult> CreatePatientAsync([FromBody] CreatePatientDto patientDto)
        {
            try
            {
                if (patientDto == null)
                    return BadRequest("Patient is null.");
                await _manager.CreatePatientAsync(patientDto, false);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{patientTCId}", Name = "UpdatePatientAsync")]
        public async Task<IActionResult> UpdatePatientAsync(ulong patientTCId, [FromBody] UpdatePatientDto updatePatientDto)
        {
            try
            {
                if (updatePatientDto == null)
                    return BadRequest("Patient is null.");
                await _manager.UpdatePatientAsync(patientTCId, updatePatientDto, false);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{patientTCId}", Name = "DeletePatientAsync")]
        public async Task<IActionResult> DeletePatientAsync([FromRoute(Name ="patientTCId")]ulong patientTCId)
        {
            try
            {
                await _manager.DeletePatientAsync(patientTCId, false);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}

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
            return Ok(patients);
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.Create
{
    public class CreateAppointmentDto
    {
        [Required]
        public ulong PatientTCId { get; set; }
        [Required]
        public string DoctorCode { get; set; }
        [Required]
        public DateTime AppointmentDateTime { get; set; }
    }
}

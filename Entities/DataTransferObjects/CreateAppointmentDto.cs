using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
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

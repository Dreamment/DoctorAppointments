using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.Create
{
    public class CreateAppointmentDto
    {
        [Required]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "PatientTCId must be 11 digits")]
        public ulong PatientTCId { get; set; }
        [Required]
        [MaxLength(10), MinLength(10)]
        public string DoctorCode { get; set; }
        [Required]
        public DateTime AppointmentDateTime { get; set; }
    }
}

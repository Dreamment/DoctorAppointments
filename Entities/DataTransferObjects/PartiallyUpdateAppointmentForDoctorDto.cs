using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Entities.DataTransferObjects
{
    public class PartiallyUpdateAppointmentForDoctorDto
    {
        [AllowNull]
        public string AppointmentCode { get; set; }

        [AllowNull]
        public ulong PatientTCId { get; set; }

        [AllowNull]
        public string DoctorCode { get; set; }

        [AllowNull]
        public DateTime AppointmentDateTime { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}

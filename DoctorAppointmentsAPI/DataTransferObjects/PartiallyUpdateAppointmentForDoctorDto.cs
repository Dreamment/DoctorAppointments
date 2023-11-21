using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DoctorAppointmentsAPI.DataTransferObjects
{
    public class PartiallyUpdateAppointmentForDoctorDto
    {
        [AllowNull]
        public int AppointmentId { get; set; }

        [AllowNull]
        public int PatientId { get; set; }

        [AllowNull]
        public int DoctorId { get; set; }

        [AllowNull]
        public DateTime AppointmentDateTime { get; set; }

        [Required]
        public bool Status { get; set; }

        [AllowNull]
        public string AppointmentCode { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Entities.DataTransferObjects.Update
{
    public class UpdateAppointmentDto
    {
        [AllowNull]
        [MaxLength(15), MinLength(15)]
        public string? AppointmentCode { get; set; }

        [AllowNull]
        public ulong? PatientTCId { get; set; }

        [AllowNull]
        [MaxLength(10), MinLength(10)]
        public string? DoctorCode { get; set; }

        [AllowNull]
        public DateTime? AppointmentDateTime { get; set; }

        [AllowNull]
        public bool? Status { get; set; }
    }
}

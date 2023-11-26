using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Entities.DataTransferObjects.Update
{
    public class PartiallyUpdateAppointmentForDoctorDto
    {
        [Required]
        public bool Status { get; set; }
    }
}

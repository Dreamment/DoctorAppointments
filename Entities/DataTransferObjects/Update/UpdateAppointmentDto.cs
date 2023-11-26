using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Entities.DataTransferObjects.Update
{
    public class UpdateAppointmentDto
    {
        [AllowNull]
        public DateTime? AppointmentDateTime { get; set; }

        [AllowNull]
        public bool? Status { get; set; }
    }
}

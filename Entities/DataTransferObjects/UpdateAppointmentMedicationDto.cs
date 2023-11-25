using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Entities.DataTransferObjects
{
    public class UpdateAppointmentMedicationDto
    {
        [AllowNull]
        [MaxLength(20)]
        public string? MedicationName { get; set; }

        [AllowNull]
        [MaxLength(20)]
        public string? Dosage { get; set; }

        [AllowNull]
        [MaxLength(100)]
        public string? UsageInstructions { get; set; }
    }
}

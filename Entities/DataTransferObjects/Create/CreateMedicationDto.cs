using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.Create
{
    public class CreateMedicationDto
    {
        [Required]
        [MaxLength(15), MinLength(15)]
        public string AppointmentCode { get; set; }

        [Required]
        public string MedicationName { get; set; }

        [Required]
        public string Dosage { get; set; }

        public string UsageInstructions { get; set; }
    }
}

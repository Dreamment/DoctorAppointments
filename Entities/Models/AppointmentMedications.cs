using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class AppointmentMedications
    {
        [Key]
        [MaxLength(17), MinLength(16)]
        public string MedicationCode { get; set; } //AppointmentCode + MedicationID (AppointmentCode1, AppointmentCode2, etc..)

        [Required]
        [ForeignKey("Appointment")]
        [MaxLength(15), MinLength(15)]
        public string AppointmentCode { get; set; }

        [Required]
        [MaxLength(20)]
        public string MedicationName { get; set; }

        [Required]
        [MaxLength(20)]
        public string Dosage { get; set; }

        [Required]
        [MaxLength(100)]
        public string UsageInstructions { get; set; }


        public virtual Appointments Appointment { get; set; }
    }
}

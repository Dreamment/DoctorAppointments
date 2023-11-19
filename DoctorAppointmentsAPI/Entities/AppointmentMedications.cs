using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorAppointmentsDomain.Entities
{
    public class AppointmentMedications
    {
        [Key]
        public int MedicationId { get; set; }

        [Required]
        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }

        [Required]
        public string MedicationName { get; set; }

        [Required]
        public string Dosage { get; set; }

        [Required]
        public string UsageInstructions { get; set; }


        public virtual Appointments Appointment { get; set; }
    }
}

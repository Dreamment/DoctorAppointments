using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models
{
    public class Appointments
    {
        [Key]
        [MaxLength(15), MinLength(15)]
        public string AppointmentCode { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public ulong PatientTCId { get; set; }

        [Required]
        [ForeignKey("Doctor")]
        [MaxLength(10), MinLength(10)]
        public string DoctorCode { get; set; }

        [Required]
        public DateTime AppointmentDateTime { get; set; }

        [AllowNull]
        public bool? Status { get; set; }


        [Required]
        public virtual Patients Patient { get; set; }

        [Required]
        public virtual Doctors Doctor { get; set; }

        public virtual ICollection<AppointmentMedications>? Medications { get; set; }
    }
}

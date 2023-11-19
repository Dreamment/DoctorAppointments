using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorAppointmentsDomain.Entities
{
    public class Appointments
    {
        [Key]
        public int AppointmentId { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        [Required]
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        [Required]
        public DateTime AppointmentDateTime { get; set; }

        public bool? Status { get; set; }

        [Required]        
        public string AppointmentCode { get; set; }

        [Required]
        public virtual Patients Patient { get; set; }

        [Required]
        public virtual Doctors Doctor { get; set; }

        public virtual IEnumerable<AppointmentMedications>? Medications { get; set; }
    }
}

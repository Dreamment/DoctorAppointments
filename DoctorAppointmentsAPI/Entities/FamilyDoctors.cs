using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorAppointmentsDomain.Entities
{
    public class FamilyDoctors
    {
        [Key]
        public int FamilyDoctorId { get; set; }

        [Required]
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }


        public virtual Doctors Doctor { get; set; }

        public virtual Patients Patient { get; set; }
    }
}

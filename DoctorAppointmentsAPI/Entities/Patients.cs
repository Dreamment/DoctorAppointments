using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorAppointmentsDomain.Entities
{
    public class Patients
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        public string PatientName { get; set; }

        [Required]
        public string PatientSurname { get; set; }

        [Required]
        public string PatientGender { get; set; }

        [Required]
        public DateTime PatientBirthDate { get; set; }

        [Required]
        [ForeignKey("FamilyDoctor")]
        public int PatientFamilyDoctorId { get; set; }


        public virtual FamilyDoctors FamilyDoctor { get; set; }

        public virtual IEnumerable<Appointments> Appointments { get; set; }

        public virtual IEnumerable<FamilyDoctorChanges> FamilyDoctorChanges { get; set; }
    }
}

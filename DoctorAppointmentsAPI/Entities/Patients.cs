using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DoctorAppointmentsAPI.Entities
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

        [AllowNull]
        [ForeignKey("FamilyDoctor")]
        public int? PatientFamilyDoctorId { get; set; }


        public virtual FamilyDoctors? FamilyDoctor { get; set; }

        public virtual IEnumerable<Appointments> Appointments { get; set; }

        public virtual IEnumerable<FamilyDoctorChanges> FamilyDoctorChanges { get; set; }
    }
}

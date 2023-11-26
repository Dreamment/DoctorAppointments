using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models
{
    public class Patients
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ulong PatientTCId { get; set; }

        [Required]
        [MaxLength(20)]
        public string PatientName { get; set; }

        [Required]
        [MaxLength(20)]
        public string PatientSurname { get; set; }

        [Required]
        [MaxLength(6),MinLength(4)]
        public string PatientGender { get; set; }

        [Required]
        public DateTime PatientBirthDate { get; set; }

        [AllowNull]
        [ForeignKey("FamilyDoctor")]
        [MaxLength(10),MinLength(10)]
        public string? PatientFamilyDoctorCode { get; set; }

        [AllowNull]
        public DateTime? PatientFamilyDoctorAppointDate { get; set; }


        public virtual Doctors? FamilyDoctor { get; set; }

        public virtual ICollection<Appointments>? Appointments { get; set; }

        public virtual ICollection<FamilyDoctorChanges>? FamilyDoctorChanges { get; set; }
    }
}

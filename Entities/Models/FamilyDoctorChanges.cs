using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models
{
    public class FamilyDoctorChanges
    {
        [Key]
        public int ChangeId { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public ulong PatientTCId { get; set; }

        [AllowNull]
        [ForeignKey("PreviousFamilyDoctor")]
        [MaxLength(10), MinLength(10)]
        public string? PreviousFamilyDoctorCode { get; set; }

        [AllowNull]
        [ForeignKey("NewFamilyDoctor")]
        [MaxLength(10), MinLength(10)]
        public string? NewFamilyDoctorCode { get; set; }

        [Required]
        public DateTime ChangeDate { get; set; }


        public Patients Patient { get; set; }
        public Doctors PreviousFamilyDoctor { get; set; }
        public Doctors NewFamilyDoctor { get; set; }
    }
}

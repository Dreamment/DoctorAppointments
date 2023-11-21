using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DoctorAppointmentsAPI.Entities
{
    public class FamilyDoctorChanges
    {
        [Key]
        public int ChangeId { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        [AllowNull]
        [ForeignKey("PreviousFamilyDoctor")]
        public int? PreviousFamilyDoctorId { get; set; }

        [Required]
        [ForeignKey("NewFamilyDoctor")]
        public int NewFamilyDoctorId { get; set; }

        [Required]
        public DateTime ChangeDate { get; set; }


        public Patients Patient { get; set; }
        public Doctors PreviousFamilyDoctor { get; set; }
        public Doctors NewFamilyDoctor { get; set; }
    }
}

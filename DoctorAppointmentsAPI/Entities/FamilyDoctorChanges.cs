using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentsDomain.Entities
{
    public class FamilyDoctorChanges
    {
        [Key]
        public int ChangeId { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        [ForeignKey("PreviousFamilyDoctor")]
        public int PreviousFamilyDoctorId { get; set; }

        [ForeignKey("NewFamilyDoctor")]
        public int NewFamilyDoctorId { get; set; }

        public DateTime ChangeDate { get; set; }


        public Patients Patient { get; set; }
        public Doctors PreviousFamilyDoctor { get; set; }
        public Doctors NewFamilyDoctor { get; set; }
    }
}

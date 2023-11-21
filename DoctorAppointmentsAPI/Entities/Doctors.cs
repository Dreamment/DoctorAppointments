using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorAppointmentsAPI.Entities
{
    public class Doctors
    {
        [Key]
        public int DoctorId { get; set; }

        [Required]
        public string DoctorName { get; set; }

        [Required]
        public string DoctorSurname { get; set; }

        [Required]
        [ForeignKey("DoctorSpeciality")]
        public int DoctorSpecialityId { get; set; }


        public virtual DoctorSpecialties DoctorSpeciality { get; set; }
        
        public virtual IEnumerable<Appointments> Appointments { get; set; }
    }
}

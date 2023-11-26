using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Doctors
    {
        [Key]
        [MaxLength(10), MinLength(10)]
        public string DoctorCode { get; set; }

        [Required]
        [MaxLength(20)]
        public string DoctorName { get; set; }

        [Required]
        [MaxLength(20)]
        public string DoctorSurname { get; set; }

        [Required]
        [ForeignKey("DoctorSpeciality")]
        public int DoctorSpecialityId { get; set; }


        [Required]
        public virtual DoctorSpecialties DoctorSpeciality { get; set; }
        
        public virtual IEnumerable<Appointments>? Appointments { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Entities.Models 
{
    public class DoctorSpecialties
    {
        [Key]
        public int DoctorSpecialityId { get; set; }

        [Required]
        public string DoctorSpecialtyName { get; set; }
    }
}

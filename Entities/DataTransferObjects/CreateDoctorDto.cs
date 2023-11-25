using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class CreateDoctorDto
    {
        [Required]
        [MaxLength(20)]
        public string DoctorName { get; set; }

        [Required]
        [MaxLength(20)]
        public string DoctorSurname { get; set; }

        [Required]
        public int DoctorSpecialityId { get; set; }
    }
}

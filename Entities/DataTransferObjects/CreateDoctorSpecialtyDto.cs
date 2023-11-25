using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class CreateDoctorSpecialtyDto
    {
        [Required]
        public string DoctorSpecialtyName { get; set; }
    }
}

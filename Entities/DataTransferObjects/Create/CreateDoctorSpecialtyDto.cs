using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.Create
{
    public class CreateDoctorSpecialtyDto
    {
        [Required]
        public string DoctorSpecialtyName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class CreateFamilyDoctorChangeDto
    {
        [Required]
        public ulong PatientTcId { get; set; }

        [Required]
        [MaxLength(10), MinLength(10)]
        public string DoctorCode { get; set; }
    }
}

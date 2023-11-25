using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Entities.DataTransferObjects.Update
{
    public class UpdateDoctorDto
    {
        [AllowNull]
        [MaxLength(20)]
        public string DoctorName { get; set; }

        [AllowNull]
        [MaxLength(20)]
        public string DoctorSurname { get; set; }

        [AllowNull]
        public int DoctorSpecialityId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Entities.DataTransferObjects.Create
{
    public class CreatePatientDto
    {
        [Required]
        public ulong PatientTCId { get; set; }

        [Required]
        [MaxLength(20)]
        public string PatientName { get; set; }

        [Required]
        [MaxLength(20)]
        public string PatientSurname { get; set; }

        [Required]
        [MaxLength(6), MinLength(4)]
        public string PatientGender { get; set; }

        [Required]
        public DateTime PatientBirthDate { get; set; }

        [AllowNull]
        [MaxLength(10), MinLength(10)]
        public string? PatientFamilyDoctorCode { get; set; }

        [AllowNull]
        public DateTime? PatientFamilyDoctorAppointDate { get; set; }
    }
}

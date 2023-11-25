using System.Diagnostics.CodeAnalysis;

namespace Entities.DataTransferObjects
{
    public class UpdatePatientDto
    {
        [AllowNull]
        public string? PatientName { get; set; }
        [AllowNull]
        public string? PatientSurname { get; set; }
        [AllowNull]
        public string? PatientGender { get; set; }
        [AllowNull]
        public DateTime? PatientBirthDate { get; set; }
        [AllowNull]
        public string? PatientFamilyDoctorCode { get; set; }
        [AllowNull]
        public DateTime? PatientFamilyDoctorAppointDate { get; set; }
    }
}

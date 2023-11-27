using System.Diagnostics.CodeAnalysis;

namespace Entities.DataTransferObjects.Update
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
    }
}

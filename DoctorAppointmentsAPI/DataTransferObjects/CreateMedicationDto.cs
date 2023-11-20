using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentsAPI.DataTransferObjects
{
    public class CreateMedicationDto
    {
        [Required]
        public string AppointmentCode { get; set; }

        [Required]
        public string MedicationName { get; set; }

        [Required]
        public string Dosage { get; set; }

        public string UsageInstructions { get; set; }
    }
}

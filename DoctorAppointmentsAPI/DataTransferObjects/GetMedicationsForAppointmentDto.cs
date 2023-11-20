namespace DoctorAppointmentsAPI.DataTransferObjects
{
    public class GetMedicationsForAppointmentDto
    {
        public string Id { get; set; }
        public string AppointmentCode { get; set; }
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public string UsageInstructions { get; set; }
    }
}

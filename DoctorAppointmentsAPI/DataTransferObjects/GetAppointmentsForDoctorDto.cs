namespace DoctorAppointmentsAPI.DataTransferObjects
{
    public class GetAppointmentsForDoctorDto
    {
        public GetPatientsForFamilyDoctorDto Patient { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public bool? Status { get; set; }
        public IEnumerable<GetMedicationsForAppointmentDto>? Medications { get; set; }
    }
}

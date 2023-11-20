namespace DoctorAppointmentsAPI.DataTransferObjects
{
    public class CreateAppointmentDto
    {
        public int PatientId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
    }
}

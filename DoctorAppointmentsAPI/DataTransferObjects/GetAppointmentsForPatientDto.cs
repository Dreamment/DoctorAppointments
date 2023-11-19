namespace DoctorAppointmentsAPI.DataTransferObjects
{
    public class GetAppointmentsForPatientDto
    {
        public string AppointmentCode { get; set; }

        public string DoctorName { get; set; }

        public string DoctorSurname { get; set; }

        public DateOnly AppointmentDate { get; set; }

        public TimeOnly AppointmentTime { get; set; }

        public bool? Status { get; set; } // is approved by doctor

        public string ApoointmentType { get; set; } // past or upcoming
    }
}

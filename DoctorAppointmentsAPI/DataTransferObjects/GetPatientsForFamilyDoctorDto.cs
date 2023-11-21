namespace DoctorAppointmentsAPI.DataTransferObjects
{
    public class GetPatientsForFamilyDoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set;}
        public string Gender { get; set; }
        public string BirthDate { get; set; }
    }
}

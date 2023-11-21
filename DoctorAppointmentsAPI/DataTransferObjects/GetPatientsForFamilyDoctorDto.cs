﻿namespace DoctorAppointmentsAPI.DataTransferObjects
{
    public class GetPatientsForFamilyDoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set;}
        public string Gender { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
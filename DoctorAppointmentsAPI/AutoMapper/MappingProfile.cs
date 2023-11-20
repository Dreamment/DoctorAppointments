using AutoMapper;
using DoctorAppointmentsAPI.DataTransferObjects;
using DoctorAppointmentsDomain.Entities;

namespace DoctorAppointmentsAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateMedicationDto, AppointmentMedications>();
        }
    }
}

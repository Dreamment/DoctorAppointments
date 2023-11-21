using AutoMapper;
using DoctorAppointmentsAPI.DataTransferObjects;
using DoctorAppointmentsAPI.Entities;

namespace DoctorAppointmentsAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateMedicationDto, AppointmentMedications>();
            CreateMap<Patients, GetPatientsForFamilyDoctorDto>();
            CreateMap<Appointments, GetAppointmentsForDoctorDto>();
            CreateMap<AppointmentMedications, GetMedicationsForAppointmentDto>();
            CreateMap<CreateAppointmentDto, Appointments>();
            CreateMap<Appointments, GetAppointmentsForPatientDto>();
            CreateMap<Doctors, GetFamilyDoctorDto>();
            CreateMap<Appointments, PartiallyUpdateAppointmentForDoctorDto>().ReverseMap();
        }
    }
}

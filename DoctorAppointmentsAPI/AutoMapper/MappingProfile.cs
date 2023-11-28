using AutoMapper;
using Entities.DataTransferObjects.Auth;
using Entities.DataTransferObjects.Create;
using Entities.DataTransferObjects.Get;
using Entities.DataTransferObjects.Update;
using Entities.Models;

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
            CreateMap<CreateMedicationDto, AppointmentMedications>();
            CreateMap<UpdateAppointmentMedicationDto, AppointmentMedications>();
            CreateMap<CreateAppointmentDto, Appointments>();
            CreateMap<UpdateAppointmentDto, Appointments>();
            CreateMap<CreateDoctorDto, Doctors>();
            CreateMap<UpdateDoctorDto, Doctors>();
            CreateMap<CreateDoctorSpecialtyDto, DoctorSpecialties>();
            CreateMap<UpdateDoctorSpecialtyDto, DoctorSpecialties>();
            CreateMap<CreatePatientDto, Patients>();
            CreateMap<UpdatePatientDto, Patients>();
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<UserForAuthenticationDto, User>();
            CreateMap<UserForPatientRegistrationDto, UserForRegistrationDto>().ReverseMap();
            CreateMap<UserForDoctorRegistrationDto, UserForRegistrationDto>().ReverseMap();
        }
    }
}

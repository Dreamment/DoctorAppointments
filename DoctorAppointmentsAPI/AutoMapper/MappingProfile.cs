using AutoMapper;
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
        }
    }
}

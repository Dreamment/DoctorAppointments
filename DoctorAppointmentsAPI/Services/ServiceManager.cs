using AutoMapper;
using DoctorAppointmentsAPI.Repositories.Contracts;
using DoctorAppointmentsAPI.Services.Contracts;

namespace DoctorAppointmentsAPI.Services
{
    public class ServiceManager : IServiceManager
    {

        private readonly Lazy<IDoctorService> _doctorService;
        private readonly Lazy<IPatientService> _patientService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _doctorService = new Lazy<IDoctorService>(() => new DoctorManager(repositoryManager, mapper));
            _patientService = new Lazy<IPatientService>(() => new PatientManager(repositoryManager, mapper));
        }

        public IDoctorService Doctor => _doctorService.Value;
        public IPatientService Patient => _patientService.Value;
    }
}

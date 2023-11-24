using Repositories.Contracts;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;

        private Lazy<IAppointmentMedicationRepository> _appointmentMedicationRepository;
        private Lazy<IAppointmentRepository> _appointmentRepository;
        private Lazy<IDoctorRepository> _doctorRepository;
        private Lazy<IDoctorSpecialityRepository> _doctorSpecialityRepository;
        private Lazy<IFamilyDoctorChangesRepository> _familyDoctorChangesRepository;
        private Lazy<IFamilyDoctorRepository> _familyDoctorRepository;
        private Lazy<IPatientRepository> _patientRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _appointmentMedicationRepository = new Lazy<IAppointmentMedicationRepository>(() 
                => new AppointmentMedicationRepository(_repositoryContext));

            _appointmentRepository = new Lazy<IAppointmentRepository>(()
                => new AppointmentRepository(_repositoryContext));

            _doctorRepository = new Lazy<IDoctorRepository>(()
                => new DoctorRepository(_repositoryContext));

            _doctorSpecialityRepository = new Lazy<IDoctorSpecialityRepository>(()
                => new DoctorSpecialityRepository(_repositoryContext));

            _familyDoctorChangesRepository = new Lazy<IFamilyDoctorChangesRepository>(()
                => new FamilyDoctorChangesRepository(_repositoryContext));

            _familyDoctorRepository = new Lazy<IFamilyDoctorRepository>(()
                => new FamilyDoctorRepository(_repositoryContext));

            _patientRepository = new Lazy<IPatientRepository>(()
                => new PatientRepository(_repositoryContext));
        }

        public IAppointmentMedicationRepository AppointmentMedication
            => _appointmentMedicationRepository.Value;
        public IAppointmentRepository Appointment
            => _appointmentRepository.Value;
        public IDoctorRepository Doctor
            => _doctorRepository.Value;
        public IDoctorSpecialityRepository DoctorSpeciality
            => _doctorSpecialityRepository.Value;
        public IFamilyDoctorChangesRepository FamilyDoctorChanges
            => _familyDoctorChangesRepository.Value;
        public IFamilyDoctorRepository FamilyDoctor
            => _familyDoctorRepository.Value;
        public IPatientRepository Patient
            => _patientRepository.Value;

        public async Task SaveAsync()
            => await _repositoryContext.SaveChangesAsync();
    }
}

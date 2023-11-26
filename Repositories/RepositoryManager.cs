using Repositories.Contracts;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

            AppointmentMedication = new AppointmentMedicationRepository(_repositoryContext);
            Appointment = new AppointmentRepository(_repositoryContext);
            Doctor = new DoctorRepository(_repositoryContext);
            DoctorSpeciality = new DoctorSpecialityRepository(_repositoryContext);
            FamilyDoctorChanges = new FamilyDoctorChangesRepository(_repositoryContext);
            Patient = new PatientRepository(_repositoryContext);
        }

        public IAppointmentMedicationRepository AppointmentMedication { get; }
        public IAppointmentRepository Appointment { get; }
        public IDoctorRepository Doctor { get; }
        public IDoctorSpecialityRepository DoctorSpeciality { get; }
        public IFamilyDoctorChangesRepository FamilyDoctorChanges { get; }
        public IPatientRepository Patient { get; }

        public async Task SaveAsync()
            => await _repositoryContext.SaveChangesAsync();
    }
}

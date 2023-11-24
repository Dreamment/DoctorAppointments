namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IAppointmentMedicationRepository AppointmentMedication { get; }
        IAppointmentRepository Appointment { get; }
        IDoctorRepository Doctor { get; }
        IDoctorSpecialityRepository DoctorSpeciality { get; }
        IFamilyDoctorChangesRepository FamilyDoctorChanges { get; }
        IFamilyDoctorRepository FamilyDoctor { get; }
        IPatientRepository Patient { get; }
        Task SaveAsync();
    }
}

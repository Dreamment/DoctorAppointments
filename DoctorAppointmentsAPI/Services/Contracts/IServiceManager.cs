namespace DoctorAppointmentsAPI.Services.Contracts
{
    public interface IServiceManager
    {
        IDoctorService Doctor { get; }
        IPatientService Patient { get; }
    }
}

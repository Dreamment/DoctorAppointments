using DoctorAppointmentsDomain.Entities;
using System.Linq.Expressions;

namespace DoctorAppointmentsAPI.Repositories.Contracts
{
    public interface IPatientRepository : IRepositoryBase<Patients>
    {
        Task<IEnumerable<Patients>> GetAllPatientsAsync(bool trackChanges);
        Task<Patients> GetPatientByIdAsync(int patientId, bool trackChanges);
        Task<IEnumerable<Patients>> GetPatientsByConditionAsync(Expression<Func<Patients, bool>> expression, bool trackChanges);
        Task CreatePatient(Patients patient);
        Task DeletePatient(Patients patient);
        Task UpdatePatient(Patients patient);
    }
}

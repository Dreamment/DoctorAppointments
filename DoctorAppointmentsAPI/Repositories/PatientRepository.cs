using DoctorAppointmentsAPI.Repositories.Contracts;
using DoctorAppointmentsAPI.Entities;
using System.Linq.Expressions;

namespace DoctorAppointmentsAPI.Repositories
{
    public class PatientRepository : RepositoryBase<Patients>, IPatientRepository
    {
        public PatientRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task CreatePatientAsync(Patients patient) 
            => await CreateAsync(patient);

        public async Task DeletePatientAsync(Patients patient)
            => await DeleteAsync(patient);

        public async Task<IEnumerable<Patients>> GetAllPatientsAsync(bool trackChanges)
            => await FindAllAsync(trackChanges);

        public async Task<Patients> GetPatientByIdAsync(int patientId, bool trackChanges)
        {
            var patients = await FindByConditionAsync(p => p.PatientId.Equals(patientId), trackChanges);
            return patients.FirstOrDefault();
        }

        public async Task<IEnumerable<Patients>> GetPatientsByConditionAsync(Expression<Func<Patients, bool>> expression, bool trackChanges) 
            => await FindByConditionAsync(expression, trackChanges);

        public Task UpdatePatientAsync(Patients patient)
            => UpdateAsync(patient);
    }
}

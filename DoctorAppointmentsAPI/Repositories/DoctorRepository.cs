using DoctorAppointmentsAPI.Repositories.Contracts;
using DoctorAppointmentsDomain.Entities;
using System.Linq.Expressions;

namespace DoctorAppointmentsAPI.Repositories
{
    public class DoctorRepository : RepositoryBase<Doctors>, IDoctorRepository
    {
        public DoctorRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task CreateDoctor(Doctors doctor)
            => await CreateAsync(doctor);

        public async Task DeleteDoctor(Doctors doctor)
            => await DeleteAsync(doctor);

        public async Task<IEnumerable<Doctors>> GetAllDoctorsAsync(bool trackChanges)
            => await FindAllAsync(trackChanges);

        public async Task<Doctors> GetDoctorByIdAsync(int doctorId, bool trackChanges)
        {
            var doctors = await FindByConditionAsync(d => d.DoctorId.Equals(doctorId), trackChanges);
            return doctors.FirstOrDefault();
        }

        public async Task<IEnumerable<Doctors>> GetDoctorsByConditionAsync(Expression<Func<Doctors, bool>> expression, bool trackChanges)
            => await FindByConditionAsync(expression, trackChanges);

        public Task UpdateDoctor(Doctors doctor)
            => UpdateAsync(doctor);
    }
}

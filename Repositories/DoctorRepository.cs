using Repositories.Contracts;
using Entities.Models;
using System.Linq.Expressions;

namespace Repositories
{
    public class DoctorRepository : RepositoryBase<Doctors>, IDoctorRepository
    {
        public DoctorRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task CreateDoctorAsync(Doctors doctor)
            => await CreateAsync(doctor);

        public async Task DeleteDoctorAsync(Doctors doctor)
            => await DeleteAsync(doctor);

        public async Task<IEnumerable<Doctors>> GetAllDoctorsAsync(bool trackChanges)
            => await FindAllAsync(trackChanges);

        public async Task<IEnumerable<Doctors>> GetDoctorsByConditionAsync(Expression<Func<Doctors, bool>> expression, bool trackChanges)
            => await FindByConditionAsync(expression, trackChanges);

        public Task UpdateDoctorAsync(Doctors doctor)
            => UpdateAsync(doctor);
    }
}

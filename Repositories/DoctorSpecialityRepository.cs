using Repositories.Contracts;
using Entities.Models;
using System.Linq.Expressions;

namespace Repositories
{
    public class DoctorSpecialityRepository : RepositoryBase<DoctorSpecialties>, IDoctorSpecialityRepository
    {
        public DoctorSpecialityRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task CreateDoctorSpecialtyAsync(DoctorSpecialties doctorSpecialty) 
            => await CreateAsync(doctorSpecialty);

        public async Task DeleteDoctorSpecialtyAsync(DoctorSpecialties doctorSpecialty)
            => await DeleteAsync(doctorSpecialty);

        public async Task<IEnumerable<DoctorSpecialties>> GetAllDoctorSpecialtiesAsync(bool trackChanges)
            => await FindAllAsync(trackChanges);

        public async Task<IEnumerable<DoctorSpecialties>> GetDoctorSpecialtiesByConditionAsync(Expression<Func<DoctorSpecialties, bool>> expression, bool trackChanges)
            => await FindByConditionAsync(expression, trackChanges);

        public async Task<DoctorSpecialties> GetDoctorSpecialtyByIdAsync(int doctorSpecialtyId, bool trackChanges)
        {
            var doctorSpecialties = await FindByConditionAsync(d => d.DoctorSpecialityId.Equals(doctorSpecialtyId), trackChanges);
            return doctorSpecialties.FirstOrDefault();
        }

        public async Task UpdateDoctorSpecialtyAsync(DoctorSpecialties doctorSpecialty)
            => await UpdateAsync(doctorSpecialty);
    }
}

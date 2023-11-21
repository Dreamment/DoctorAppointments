using DoctorAppointmentsAPI.Repositories.Contracts;
using DoctorAppointmentsAPI.Entities;
using System.Linq.Expressions;

namespace DoctorAppointmentsAPI.Repositories
{
    public class FamilyDoctorRepository : RepositoryBase<FamilyDoctors>, IFamilyDoctorRepository
    {
        public FamilyDoctorRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task CreateFamilyDoctorAsync(FamilyDoctors familyDoctor)
            => await CreateAsync(familyDoctor);

        public async Task DeleteFamilyDoctorAsync(FamilyDoctors familyDoctor)
            => await DeleteAsync(familyDoctor);

        public async Task<IEnumerable<FamilyDoctors>> GetAllFamilyDoctorsAsync(bool trackChanges)
            => await FindAllAsync(trackChanges);

        public async Task<FamilyDoctors> GetFamilyDoctorByIdAsync(int familyDoctorId, bool trackChanges)
        {
            var familyDoctors = await FindByConditionAsync(d => d.FamilyDoctorId.Equals(familyDoctorId), trackChanges);
            return familyDoctors.FirstOrDefault();
        }

        public async Task<IEnumerable<FamilyDoctors>> GetFamilyDoctorsByConditionAsync(Expression<Func<FamilyDoctors, bool>> expression, bool trackChanges)
            => await FindByConditionAsync(expression, trackChanges);

        public Task UpdateFamilyDoctorAsync(FamilyDoctors familyDoctor)
            => UpdateAsync(familyDoctor);
    }
}

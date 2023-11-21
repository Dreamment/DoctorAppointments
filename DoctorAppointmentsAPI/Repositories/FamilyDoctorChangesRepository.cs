using DoctorAppointmentsAPI.Repositories.Contracts;
using DoctorAppointmentsAPI.Entities;
using System.Linq.Expressions;

namespace DoctorAppointmentsAPI.Repositories
{
    public class FamilyDoctorChangesRepository : RepositoryBase<FamilyDoctorChanges>, IFamilyDoctorChangesRepository
    {
        public FamilyDoctorChangesRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task CreateFamilyDoctorChangeAsync(FamilyDoctorChanges familyDoctorChange)
            => await CreateAsync(familyDoctorChange);

        public async Task DeleteFamilyDoctorChangeAsync(FamilyDoctorChanges familyDoctorChange)
            => await DeleteAsync(familyDoctorChange);

        public async Task<IEnumerable<FamilyDoctorChanges>> GetAllFamilyDoctorChangesAsync(bool trackChanges)
            => await FindAllAsync(trackChanges);

        public async Task<FamilyDoctorChanges> GetFamilyDoctorChangeByIdAsync(int familyDoctorChangeId, bool trackChanges)
        {
            var familyDoctorChanges = await FindByConditionAsync(f => f.ChangeId.Equals(familyDoctorChangeId), trackChanges);
            return familyDoctorChanges.FirstOrDefault();
        }

        public async Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByConditionAsync(Expression<Func<FamilyDoctorChanges, bool>> expression, bool trackChanges)
            => await FindByConditionAsync(expression, trackChanges);

        public async Task UpdateFamilyDoctorChangeAsync(FamilyDoctorChanges familyDoctorChange)
            => await UpdateAsync(familyDoctorChange);
    }
}

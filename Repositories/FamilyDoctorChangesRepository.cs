using Repositories.Contracts;
using Entities.Models;
using System.Linq.Expressions;

namespace Repositories
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

        public async Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByConditionAsync(Expression<Func<FamilyDoctorChanges, bool>> expression, bool trackChanges)
            => await FindByConditionAsync(expression, trackChanges);

        public async Task UpdateFamilyDoctorChangeAsync(FamilyDoctorChanges familyDoctorChange)
            => await UpdateAsync(familyDoctorChange);
    }
}

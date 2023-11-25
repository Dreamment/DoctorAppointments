using Entities.Models;
using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IFamilyDoctorChangesRepository : IRepositoryBase<FamilyDoctorChanges>
    {
        Task<IEnumerable<FamilyDoctorChanges>> GetAllFamilyDoctorChangesAsync(bool trackChanges);
        Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByConditionAsync(Expression<Func<FamilyDoctorChanges, bool>> expression, bool trackChanges);
        Task CreateFamilyDoctorChangeAsync(FamilyDoctorChanges familyDoctorChange);
        Task DeleteFamilyDoctorChangeAsync(FamilyDoctorChanges familyDoctorChange);
        Task UpdateFamilyDoctorChangeAsync(FamilyDoctorChanges familyDoctorChange);
    }
}

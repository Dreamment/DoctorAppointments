using DoctorAppointmentsAPI.Entities;
using System.Linq.Expressions;

namespace DoctorAppointmentsAPI.Repositories.Contracts
{
    public interface IFamilyDoctorChangesRepository : IRepositoryBase<FamilyDoctorChanges>
    {
        Task CreateFamilyDoctorChangeAsync(FamilyDoctorChanges familyDoctorChange);
        Task DeleteFamilyDoctorChangeAsync(FamilyDoctorChanges familyDoctorChange);
        Task<IEnumerable<FamilyDoctorChanges>> GetAllFamilyDoctorChangesAsync(bool trackChanges);
        Task<FamilyDoctorChanges> GetFamilyDoctorChangeByIdAsync(int familyDoctorChangeId, bool trackChanges);
        Task<IEnumerable<FamilyDoctorChanges>> GetFamilyDoctorChangesByConditionAsync(Expression<Func<FamilyDoctorChanges, bool>> expression, bool trackChanges);
        Task UpdateFamilyDoctorChangeAsync(FamilyDoctorChanges familyDoctorChange);
    }
}

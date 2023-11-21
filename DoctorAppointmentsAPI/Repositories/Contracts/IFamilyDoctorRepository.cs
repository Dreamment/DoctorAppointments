using DoctorAppointmentsAPI.Entities;
using System.Linq.Expressions;

namespace DoctorAppointmentsAPI.Repositories.Contracts
{
    public interface IFamilyDoctorRepository : IRepositoryBase<FamilyDoctors>
    {
        Task CreateFamilyDoctorAsync(FamilyDoctors familyDoctor);
        Task DeleteFamilyDoctorAsync(FamilyDoctors familyDoctor);
        Task<IEnumerable<FamilyDoctors>> GetAllFamilyDoctorsAsync(bool trackChanges);
        Task<FamilyDoctors> GetFamilyDoctorByIdAsync(int familyDoctorId, bool trackChanges);
        Task<IEnumerable<FamilyDoctors>> GetFamilyDoctorsByConditionAsync(Expression<Func<FamilyDoctors, bool>> expression, bool trackChanges);
        Task UpdateFamilyDoctorAsync(FamilyDoctors familyDoctor);
    }
}

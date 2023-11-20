using DoctorAppointmentsDomain.Entities;
using System.Linq.Expressions;

namespace DoctorAppointmentsAPI.Repositories.Contracts
{
    public interface IFamilyDoctorRepository : IRepositoryBase<FamilyDoctors>
    {
        Task CreateFamilyDoctor(FamilyDoctors familyDoctor);
        Task DeleteFamilyDoctor(FamilyDoctors familyDoctor);
        Task<IEnumerable<FamilyDoctors>> GetAllFamilyDoctorsAsync(bool trackChanges);
        Task<FamilyDoctors> GetFamilyDoctorByIdAsync(int familyDoctorId, bool trackChanges);
        Task<IEnumerable<FamilyDoctors>> GetFamilyDoctorsByConditionAsync(Expression<Func<FamilyDoctors, bool>> expression, bool trackChanges);
        Task UpdateFamilyDoctor(FamilyDoctors familyDoctor);
    }
}

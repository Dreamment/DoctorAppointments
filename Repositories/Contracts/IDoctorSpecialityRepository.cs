using Entities.Models;
using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IDoctorSpecialityRepository : IRepositoryBase<DoctorSpecialties>
    {
        Task<IEnumerable<DoctorSpecialties>> GetAllDoctorSpecialtiesAsync(bool trackChanges);
        Task<DoctorSpecialties> GetDoctorSpecialtyByIdAsync(int doctorSpecialtyId, bool trackChanges);
        Task<IEnumerable<DoctorSpecialties>> GetDoctorSpecialtiesByConditionAsync(Expression<Func<DoctorSpecialties, bool>> expression, bool trackChanges);
        Task CreateDoctorSpecialtyAsync(DoctorSpecialties doctorSpecialty);
        Task DeleteDoctorSpecialtyAsync(DoctorSpecialties doctorSpecialty);
        Task UpdateDoctorSpecialtyAsync(DoctorSpecialties doctorSpecialty);
    }
}

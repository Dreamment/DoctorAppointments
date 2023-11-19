using DoctorAppointmentsDomain.Entities;
using System.Linq.Expressions;

namespace DoctorAppointmentsAPI.Repositories.Contracts
{
    public interface IDoctorRepository : IRepositoryBase<Doctors>
    {
        Task<IEnumerable<Doctors>> GetAllDoctorsAsync(bool trackChanges);
        Task<Doctors> GetDoctorByIdAsync(int doctorId, bool trackChanges);
        Task<IEnumerable<Doctors>> GetDoctorsByConditionAsync(Expression<Func<Doctors, bool>> expression, bool trackChanges);
        Task CreateDoctor(Doctors doctor);
        Task DeleteDoctor(Doctors doctor);
        Task UpdateDoctor(Doctors doctor);
    }
}

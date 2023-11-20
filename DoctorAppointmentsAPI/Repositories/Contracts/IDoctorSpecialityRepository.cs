﻿using DoctorAppointmentsDomain.Entities;
using System.Linq.Expressions;

namespace DoctorAppointmentsAPI.Repositories.Contracts
{
    public interface IDoctorSpecialityRepository : IRepositoryBase<DoctorSpecialties>
    {
        Task<IEnumerable<DoctorSpecialties>> GetAllDoctorSpecialtiesAsync(bool trackChanges);
        Task<DoctorSpecialties> GetDoctorSpecialtyByIdAsync(int doctorSpecialtyId, bool trackChanges);
        Task<IEnumerable<DoctorSpecialties>> GetDoctorSpecialtiesByConditionAsync(Expression<Func<DoctorSpecialties, bool>> expression, bool trackChanges);
        Task CreateDoctorSpecialty(DoctorSpecialties doctorSpecialty);
        Task DeleteDoctorSpecialty(DoctorSpecialties doctorSpecialty);
        Task UpdateDoctorSpecialty(DoctorSpecialties doctorSpecialty);
    }
}
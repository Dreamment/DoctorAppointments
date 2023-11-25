﻿using Entities.Models;
using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IDoctorRepository : IRepositoryBase<Doctors>
    {
        Task<IEnumerable<Doctors>> GetAllDoctorsAsync(bool trackChanges);
        Task<IEnumerable<Doctors>> GetDoctorsByConditionAsync(Expression<Func<Doctors, bool>> expression, bool trackChanges);
        Task CreateDoctorAsync(Doctors doctor);
        Task DeleteDoctorAsync(Doctors doctor);
        Task UpdateDoctorAsync(Doctors doctor);
    }
}

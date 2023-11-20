﻿using DoctorAppointmentsDomain.Entities;
using System.Linq.Expressions;

namespace DoctorAppointmentsAPI.Repositories.Contracts
{
    public interface IAppointmentRepository : IRepositoryBase<Appointments>
    {
        Task<IEnumerable<Appointments>> GetAllAppointmentsAsync(bool trackChanges);
        Task<Appointments> GetAppointmentByCodeAsync(int appointmentCode, bool trackChanges);
        Task<IEnumerable<Appointments>> GetAppointmentsByConditionAsync(Expression<Func<Appointments, bool>> expression, bool trackChanges);
        Task CreateAppointment(Appointments appointment);
        Task DeleteAppointment(Appointments appointment);
        Task UpdateAppointment(Appointments appointment);
    }
}
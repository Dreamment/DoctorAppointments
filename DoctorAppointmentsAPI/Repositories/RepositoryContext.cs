using DoctorAppointmentsDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentsAPI.Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions context) : base (context)
        {

        }

        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<FamilyDoctors> FamilyDoctors { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<DoctorSpecialties> DoctorSpecialties { get; set; }
        public DbSet<FamilyDoctorChanges> FamilyDoctorChanges { get; set; }
        public DbSet<AppointmentMedications> AppointmentsMedications { get; set; }
    }
}

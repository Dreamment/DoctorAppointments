using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<DoctorSpecialties> DoctorSpecialties { get; set; }
        public DbSet<FamilyDoctorChanges> FamilyDoctorChanges { get; set; }
        public DbSet<AppointmentMedications> AppointmentsMedications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointments>()
                .Property(a => a.PatientTCId)
                .HasColumnType("bigint");

            modelBuilder.Entity<FamilyDoctorChanges>()
                .Property(f => f.PatientTCId)
                .HasColumnType("bigint");

            modelBuilder.Entity<Patients>()
                .Property(p => p.PatientBirthDate)
                .HasColumnType("date");

            modelBuilder.Entity<Patients>()
                .Property(p => p.PatientTCId)
                .HasColumnType("bigint");

            modelBuilder.Entity<FamilyDoctorChanges>()
                .Property(f => f.PreviousFamilyDoctorCode)
                .IsRequired(false);

            modelBuilder.Entity<Patients>()
                .Property(p => p.PatientFamilyDoctorCode)
                .IsRequired(false);

            modelBuilder.Entity<Appointments>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorCode)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointments>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientTCId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AppointmentMedications>()
                .HasOne(am => am.Appointment)
                .WithMany(a => a.Medications)
                .HasForeignKey(am => am.AppointmentCode)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Doctors>()
                .HasOne(d => d.DoctorSpeciality)
                .WithMany()
                .HasForeignKey(d => d.DoctorSpecialityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FamilyDoctorChanges>()
                .HasOne(f => f.PreviousFamilyDoctor)
                .WithMany()
                .HasForeignKey(f => f.PreviousFamilyDoctorCode)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FamilyDoctorChanges>()
                .HasOne(f => f.NewFamilyDoctor)
                .WithMany()
                .HasForeignKey(f => f.NewFamilyDoctorCode)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FamilyDoctorChanges>()
                .HasOne(f => f.Patient)
                .WithMany(p => p.FamilyDoctorChanges)
                .HasForeignKey(f => f.PatientTCId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Patients>()
                .HasOne(p => p.FamilyDoctor)
                .WithMany()
                .HasForeignKey(p => p.PatientFamilyDoctorCode)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

using DoctorAppointmentsDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorAppointmentsAPI.Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions context) : base(context)
        {

        }

        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<FamilyDoctors> FamilyDoctors { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<DoctorSpecialties> DoctorSpecialties { get; set; }
        public DbSet<FamilyDoctorChanges> FamilyDoctorChanges { get; set; }
        public DbSet<AppointmentMedications> AppointmentsMedications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointments>()
                .HasIndex(a => a.AppointmentCode)
                .IsUnique();

            modelBuilder.Entity<Patients>()
                .Property(p => p.PatientBirthDate)
                .HasColumnType("date");

            modelBuilder.Entity<Appointments>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointments>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AppointmentMedications>()
                .HasOne(am => am.Appointment)
                .WithMany(a => a.Medications)
                .HasForeignKey(am => am.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Doctors>()
                .HasOne(d => d.DoctorSpeciality)
                .WithMany()
                .HasForeignKey(d => d.DoctorSpecialityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FamilyDoctorChanges>()
                .HasOne(f => f.PreviousFamilyDoctor)
                .WithMany()
                .HasForeignKey(f => f.PreviousFamilyDoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FamilyDoctorChanges>()
                .HasOne(f => f.NewFamilyDoctor)
                .WithMany()
                .HasForeignKey(f => f.NewFamilyDoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FamilyDoctorChanges>()
                .HasOne(f => f.Patient)
                .WithMany(p => p.FamilyDoctorChanges)
                .HasForeignKey(f => f.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FamilyDoctors>()
                .HasOne(fd => fd.Doctor)
                .WithMany()
                .HasForeignKey(fd => fd.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FamilyDoctors>()
                .HasOne(fd => fd.Patient)
                .WithMany()
                .HasForeignKey(fd => fd.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Patients>()
                .HasOne(p => p.FamilyDoctor)
                .WithMany()
                .HasForeignKey(p => p.PatientFamilyDoctorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

﻿// <auto-generated />
using System;
using DoctorAppointmentsAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DoctorAppointmentsAPI.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20231120122653_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DoctorAppointmentsDomain.Entities.AppointmentMedications", b =>
                {
                    b.Property<int>("MedicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedicationId"), 1L, 1);

                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<string>("Dosage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MedicationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsageInstructions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MedicationId");

                    b.HasIndex("AppointmentId");

                    b.ToTable("AppointmentsMedications");
                });

            modelBuilder.Entity("DoctorAppointmentsDomain.Entities.Appointments", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"), 1L, 1);

                    b.Property<string>("AppointmentCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("AppointmentDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.HasKey("AppointmentId");

                    b.HasIndex("AppointmentCode")
                        .IsUnique();

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("DoctorAppointmentsDomain.Entities.Doctors", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorId"), 1L, 1);

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoctorSpecialityId")
                        .HasColumnType("int");

                    b.Property<string>("DoctorSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DoctorId");

                    b.HasIndex("DoctorSpecialityId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("DoctorAppointmentsDomain.Entities.DoctorSpecialties", b =>
                {
                    b.Property<int>("DoctorSpecialityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorSpecialityId"), 1L, 1);

                    b.Property<string>("DoctorSpecialtyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DoctorSpecialityId");

                    b.ToTable("DoctorSpecialties");
                });

            modelBuilder.Entity("DoctorAppointmentsDomain.Entities.FamilyDoctorChanges", b =>
                {
                    b.Property<int>("ChangeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChangeId"), 1L, 1);

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("NewFamilyDoctorId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("PreviousFamilyDoctorId")
                        .HasColumnType("int");

                    b.HasKey("ChangeId");

                    b.HasIndex("NewFamilyDoctorId");

                    b.HasIndex("PatientId");

                    b.HasIndex("PreviousFamilyDoctorId");

                    b.ToTable("FamilyDoctorChanges");
                });

            modelBuilder.Entity("DoctorAppointmentsDomain.Entities.FamilyDoctors", b =>
                {
                    b.Property<int>("FamilyDoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FamilyDoctorId"), 1L, 1);

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("FamilyDoctorId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("FamilyDoctors");
                });

            modelBuilder.Entity("DoctorAppointmentsDomain.Entities.Patients", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PatientId"), 1L, 1);

                    b.Property<DateTime>("PatientBirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PatientFamilyDoctorId")
                        .HasColumnType("int");

                    b.Property<string>("PatientGender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PatientId");

                    b.HasIndex("PatientFamilyDoctorId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("DoctorAppointmentsDomain.Entities.AppointmentMedications", b =>
                {
                    b.HasOne("DoctorAppointmentsDomain.Entities.Appointments", "Appointment")
                        .WithMany("Medications")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("DoctorAppointmentsDomain.Entities.Appointments", b =>
                {
                    b.HasOne("DoctorAppointmentsDomain.Entities.Doctors", "Doctor")
                        .WithMany("Appointments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DoctorAppointmentsDomain.Entities.Patients", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("DoctorAppointmentsDomain.Entities.Doctors", b =>
                {
                    b.HasOne("DoctorAppointmentsDomain.Entities.DoctorSpecialties", "DoctorSpeciality")
                        .WithMany()
                        .HasForeignKey("DoctorSpecialityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("DoctorSpeciality");
                });

            modelBuilder.Entity("DoctorAppointmentsDomain.Entities.FamilyDoctorChanges", b =>
                {
                    b.HasOne("DoctorAppointmentsDomain.Entities.Doctors", "NewFamilyDoctor")
                        .WithMany()
                        .HasForeignKey("NewFamilyDoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DoctorAppointmentsDomain.Entities.Patients", "Patient")
                        .WithMany("FamilyDoctorChanges")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DoctorAppointmentsDomain.Entities.Doctors", "PreviousFamilyDoctor")
                        .WithMany()
                        .HasForeignKey("PreviousFamilyDoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("NewFamilyDoctor");

                    b.Navigation("Patient");

                    b.Navigation("PreviousFamilyDoctor");
                });

            modelBuilder.Entity("DoctorAppointmentsDomain.Entities.FamilyDoctors", b =>
                {
                    b.HasOne("DoctorAppointmentsDomain.Entities.Doctors", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DoctorAppointmentsDomain.Entities.Patients", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("DoctorAppointmentsDomain.Entities.Patients", b =>
                {
                    b.HasOne("DoctorAppointmentsDomain.Entities.FamilyDoctors", "FamilyDoctor")
                        .WithMany()
                        .HasForeignKey("PatientFamilyDoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("FamilyDoctor");
                });

            modelBuilder.Entity("DoctorAppointmentsDomain.Entities.Appointments", b =>
                {
                    b.Navigation("Medications");
                });

            modelBuilder.Entity("DoctorAppointmentsDomain.Entities.Doctors", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("DoctorAppointmentsDomain.Entities.Patients", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("FamilyDoctorChanges");
                });
#pragma warning restore 612, 618
        }
    }
}

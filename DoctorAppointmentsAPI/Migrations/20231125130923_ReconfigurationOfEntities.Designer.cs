﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repositories;

#nullable disable

namespace DoctorAppointmentsAPI.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20231125130923_ReconfigurationOfEntities")]
    partial class ReconfigurationOfEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Entities.Models.AppointmentMedications", b =>
                {
                    b.Property<string>("MedicationCode")
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)");

                    b.Property<string>("AppointmentCode")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Dosage")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("MedicationName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UsageInstructions")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MedicationCode");

                    b.HasIndex("AppointmentCode");

                    b.ToTable("AppointmentsMedications");
                });

            modelBuilder.Entity("Entities.Models.Appointments", b =>
                {
                    b.Property<string>("AppointmentCode")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime>("AppointmentDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DoctorCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<long>("PatientTCId")
                        .HasColumnType("bigint");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.HasKey("AppointmentCode");

                    b.HasIndex("DoctorCode");

                    b.HasIndex("PatientTCId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Entities.Models.Doctors", b =>
                {
                    b.Property<string>("DoctorCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("DoctorSpecialityId")
                        .HasColumnType("int");

                    b.Property<string>("DoctorSurname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("DoctorCode");

                    b.HasIndex("DoctorSpecialityId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Entities.Models.DoctorSpecialties", b =>
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

            modelBuilder.Entity("Entities.Models.FamilyDoctorChanges", b =>
                {
                    b.Property<int>("ChangeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChangeId"), 1L, 1);

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NewFamilyDoctorCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<long>("PatientTCId")
                        .HasColumnType("bigint");

                    b.Property<string>("PreviousFamilyDoctorCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("ChangeId");

                    b.HasIndex("NewFamilyDoctorCode");

                    b.HasIndex("PatientTCId");

                    b.HasIndex("PreviousFamilyDoctorCode");

                    b.ToTable("FamilyDoctorChanges");
                });

            modelBuilder.Entity("Entities.Models.Patients", b =>
                {
                    b.Property<long>("PatientTCId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PatientTCId"), 1L, 1);

                    b.Property<string>("DoctorsDoctorCode")
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("PatientBirthDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("PatientFamilyDoctorAppointDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PatientFamilyDoctorCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("PatientGender")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PatientSurname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("PatientTCId");

                    b.HasIndex("DoctorsDoctorCode");

                    b.HasIndex("PatientFamilyDoctorCode");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Entities.Models.AppointmentMedications", b =>
                {
                    b.HasOne("Entities.Models.Appointments", "Appointment")
                        .WithMany("Medications")
                        .HasForeignKey("AppointmentCode")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("Entities.Models.Appointments", b =>
                {
                    b.HasOne("Entities.Models.Doctors", "Doctor")
                        .WithMany("Appointments")
                        .HasForeignKey("DoctorCode")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Entities.Models.Patients", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientTCId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Entities.Models.Doctors", b =>
                {
                    b.HasOne("Entities.Models.DoctorSpecialties", "DoctorSpeciality")
                        .WithMany()
                        .HasForeignKey("DoctorSpecialityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("DoctorSpeciality");
                });

            modelBuilder.Entity("Entities.Models.FamilyDoctorChanges", b =>
                {
                    b.HasOne("Entities.Models.Doctors", "NewFamilyDoctor")
                        .WithMany()
                        .HasForeignKey("NewFamilyDoctorCode")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Entities.Models.Patients", "Patient")
                        .WithMany("FamilyDoctorChanges")
                        .HasForeignKey("PatientTCId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Entities.Models.Doctors", "PreviousFamilyDoctor")
                        .WithMany()
                        .HasForeignKey("PreviousFamilyDoctorCode")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("NewFamilyDoctor");

                    b.Navigation("Patient");

                    b.Navigation("PreviousFamilyDoctor");
                });

            modelBuilder.Entity("Entities.Models.Patients", b =>
                {
                    b.HasOne("Entities.Models.Doctors", null)
                        .WithMany("Patients")
                        .HasForeignKey("DoctorsDoctorCode");

                    b.HasOne("Entities.Models.Doctors", "FamilyDoctor")
                        .WithMany()
                        .HasForeignKey("PatientFamilyDoctorCode")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("FamilyDoctor");
                });

            modelBuilder.Entity("Entities.Models.Appointments", b =>
                {
                    b.Navigation("Medications");
                });

            modelBuilder.Entity("Entities.Models.Doctors", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Patients");
                });

            modelBuilder.Entity("Entities.Models.Patients", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("FamilyDoctorChanges");
                });
#pragma warning restore 612, 618
        }
    }
}

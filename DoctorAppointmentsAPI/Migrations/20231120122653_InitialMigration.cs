using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorAppointmentsAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorSpecialties",
                columns: table => new
                {
                    DoctorSpecialityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorSpecialtyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSpecialties", x => x.DoctorSpecialityId);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorSpecialityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK_Doctors_DoctorSpecialties_DoctorSpecialityId",
                        column: x => x.DoctorSpecialityId,
                        principalTable: "DoctorSpecialties",
                        principalColumn: "DoctorSpecialityId");
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    AppointmentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    AppointmentCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId");
                });

            migrationBuilder.CreateTable(
                name: "AppointmentsMedications",
                columns: table => new
                {
                    MedicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsageInstructions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentsMedications", x => x.MedicationId);
                    table.ForeignKey(
                        name: "FK_AppointmentsMedications_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentId");
                });

            migrationBuilder.CreateTable(
                name: "FamilyDoctorChanges",
                columns: table => new
                {
                    ChangeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    PreviousFamilyDoctorId = table.Column<int>(type: "int", nullable: false),
                    NewFamilyDoctorId = table.Column<int>(type: "int", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyDoctorChanges", x => x.ChangeId);
                    table.ForeignKey(
                        name: "FK_FamilyDoctorChanges_Doctors_NewFamilyDoctorId",
                        column: x => x.NewFamilyDoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId");
                    table.ForeignKey(
                        name: "FK_FamilyDoctorChanges_Doctors_PreviousFamilyDoctorId",
                        column: x => x.PreviousFamilyDoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId");
                });

            migrationBuilder.CreateTable(
                name: "FamilyDoctors",
                columns: table => new
                {
                    FamilyDoctorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyDoctors", x => x.FamilyDoctorId);
                    table.ForeignKey(
                        name: "FK_FamilyDoctors_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId");
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientBirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientFamilyDoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_FamilyDoctors_PatientFamilyDoctorId",
                        column: x => x.PatientFamilyDoctorId,
                        principalTable: "FamilyDoctors",
                        principalColumn: "FamilyDoctorId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentCode",
                table: "Appointments",
                column: "AppointmentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsMedications_AppointmentId",
                table: "AppointmentsMedications",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DoctorSpecialityId",
                table: "Doctors",
                column: "DoctorSpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyDoctorChanges_NewFamilyDoctorId",
                table: "FamilyDoctorChanges",
                column: "NewFamilyDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyDoctorChanges_PatientId",
                table: "FamilyDoctorChanges",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyDoctorChanges_PreviousFamilyDoctorId",
                table: "FamilyDoctorChanges",
                column: "PreviousFamilyDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyDoctors_DoctorId",
                table: "FamilyDoctors",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyDoctors_PatientId",
                table: "FamilyDoctors",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientFamilyDoctorId",
                table: "Patients",
                column: "PatientFamilyDoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyDoctorChanges_Patients_PatientId",
                table: "FamilyDoctorChanges",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyDoctors_Patients_PatientId",
                table: "FamilyDoctors",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyDoctors_Doctors_DoctorId",
                table: "FamilyDoctors");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyDoctors_Patients_PatientId",
                table: "FamilyDoctors");

            migrationBuilder.DropTable(
                name: "AppointmentsMedications");

            migrationBuilder.DropTable(
                name: "FamilyDoctorChanges");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "DoctorSpecialties");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "FamilyDoctors");
        }
    }
}

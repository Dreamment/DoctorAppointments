using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorAppointmentsAPI.Migrations
{
    public partial class RemoveUnnecessaryEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_FamilyDoctors_PatientFamilyDoctorId",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "FamilyDoctors");

            migrationBuilder.AddColumn<DateTime>(
                name: "PatientFamilyDoctorAppointDate",
                table: "Patients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Doctors_PatientFamilyDoctorId",
                table: "Patients",
                column: "PatientFamilyDoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Doctors_PatientFamilyDoctorId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientFamilyDoctorAppointDate",
                table: "Patients");

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
                    table.ForeignKey(
                        name: "FK_FamilyDoctors_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FamilyDoctors_DoctorId",
                table: "FamilyDoctors",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyDoctors_PatientId",
                table: "FamilyDoctors",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_FamilyDoctors_PatientFamilyDoctorId",
                table: "Patients",
                column: "PatientFamilyDoctorId",
                principalTable: "FamilyDoctors",
                principalColumn: "FamilyDoctorId");
        }
    }
}

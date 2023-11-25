using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorAppointmentsAPI.Migrations
{
    public partial class FixBugInPatientsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Doctors_DoctorsDoctorCode",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_DoctorsDoctorCode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DoctorsDoctorCode",
                table: "Patients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorsDoctorCode",
                table: "Patients",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoctorsDoctorCode",
                table: "Patients",
                column: "DoctorsDoctorCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Doctors_DoctorsDoctorCode",
                table: "Patients",
                column: "DoctorsDoctorCode",
                principalTable: "Doctors",
                principalColumn: "DoctorCode");
        }
    }
}

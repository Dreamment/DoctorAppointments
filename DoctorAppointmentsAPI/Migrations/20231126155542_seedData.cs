using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorAppointmentsAPI.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DoctorSpecialties",
                columns: new[] { "DoctorSpecialityId", "DoctorSpecialtyName" },
                values: new object[] { 1, "Family Doctor" });

            migrationBuilder.InsertData(
                table: "DoctorSpecialties",
                columns: new[] { "DoctorSpecialityId", "DoctorSpecialtyName" },
                values: new object[] { 2, "Other" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DoctorSpecialties",
                keyColumn: "DoctorSpecialityId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DoctorSpecialties",
                keyColumn: "DoctorSpecialityId",
                keyValue: 2);
        }
    }
}

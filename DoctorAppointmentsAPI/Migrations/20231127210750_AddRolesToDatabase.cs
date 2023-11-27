using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorAppointmentsAPI.Migrations
{
    public partial class AddRolesToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "359ce375-376c-42b6-b52e-a02b48e116a3", "15fc8678-cd60-405a-bb86-063c67572e56", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8203b0fd-a012-4f37-a2fc-30b977a94518", "fcc2c8ad-7aa1-4cb8-931c-515d14f480ff", "Doctor", "DOCTOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8f416413-0a82-477c-a646-43f1ba04715b", "ae43ff93-a979-4898-a527-f1b2685f395f", "Patient", "PATIENT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "359ce375-376c-42b6-b52e-a02b48e116a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8203b0fd-a012-4f37-a2fc-30b977a94518");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f416413-0a82-477c-a646-43f1ba04715b");
        }
    }
}

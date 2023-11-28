using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorAppointmentsAPI.Migrations
{
    public partial class AddAdminUserToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "359ce375-376c-42b6-b52e-a02b48e116a3",
                column: "ConcurrencyStamp",
                value: "cd0157b8-5c8d-407e-b13e-97344b7b3144");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8203b0fd-a012-4f37-a2fc-30b977a94518",
                column: "ConcurrencyStamp",
                value: "1cd8d92e-f1e3-47f4-b6d8-04d459e43c89");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f416413-0a82-477c-a646-43f1ba04715b",
                column: "ConcurrencyStamp",
                value: "122a8b64-391a-4377-bc5d-cb357aebcfa0");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "18dd2552-17ae-4bdb-9621-6061e1308bd4", 0, "5a506bc1-c149-4a55-b107-a6508f060690", "admin@admin.com", false, true, null, "admin", "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAEK1UJlTVFUSnIG7wzErpGrmGlG8/+UwZiCkLEhu8cP+XpYYMznxyZc2sVPsFN3Aytw==", "admin", false, "7ce22d4e-cc48-474c-8a20-e5165fd928b9", "admin", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "359ce375-376c-42b6-b52e-a02b48e116a3", "18dd2552-17ae-4bdb-9621-6061e1308bd4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "359ce375-376c-42b6-b52e-a02b48e116a3", "18dd2552-17ae-4bdb-9621-6061e1308bd4" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "18dd2552-17ae-4bdb-9621-6061e1308bd4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "359ce375-376c-42b6-b52e-a02b48e116a3",
                column: "ConcurrencyStamp",
                value: "15fc8678-cd60-405a-bb86-063c67572e56");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8203b0fd-a012-4f37-a2fc-30b977a94518",
                column: "ConcurrencyStamp",
                value: "fcc2c8ad-7aa1-4cb8-931c-515d14f480ff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f416413-0a82-477c-a646-43f1ba04715b",
                column: "ConcurrencyStamp",
                value: "ae43ff93-a979-4898-a527-f1b2685f395f");
        }
    }
}

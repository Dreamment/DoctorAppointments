using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorAppointmentsAPI.Migrations
{
    public partial class ChangeNullabilityOfNewFamilyDoctorCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NewFamilyDoctorCode",
                table: "FamilyDoctorChanges",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NewFamilyDoctorCode",
                table: "FamilyDoctorChanges",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);
        }
    }
}

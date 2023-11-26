using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorAppointmentsAPI.Migrations
{
    public partial class RemoveIdentityInPatientTcId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_FamilyDoctorChanges_Patients_PatientTCId", "FamilyDoctorChanges");
            migrationBuilder.DropForeignKey("FK_Appointments_Patients_PatientTCId", "Appointments");

            migrationBuilder.DropIndex("IX_FamilyDoctorChanges_PatientTCId", "FamilyDoctorChanges");
            migrationBuilder.DropIndex("IX_Appointments_PatientTCId", "Appointments");

            migrationBuilder.DropPrimaryKey("PK_Patients", "Patients");

            migrationBuilder.DropColumn("PatientTCId", "Patients");

            migrationBuilder.AddColumn<long>(
                name: "PatientTCId",
                table: "Patients",
                type: "bigint",
                nullable: false);

            migrationBuilder.AddPrimaryKey("PK_Patients", "Patients", "PatientTCId");

            migrationBuilder.CreateIndex("IX_FamilyDoctorChanges_PatientTCId", "FamilyDoctorChanges", "PatientTCId");
            migrationBuilder.CreateIndex("IX_Appointments_PatientTCId", "Appointments", "PatientTCId");
            migrationBuilder.AddForeignKey("FK_FamilyDoctorChanges_Patients_PatientTCId", "FamilyDoctorChanges", "PatientTCId", "Patients", principalColumn: "PatientTCId", onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientTCId",
                table: "Patients");

            migrationBuilder.AddColumn<long>(
                name: "PatientTCId",
                table: "Patients",
                type: "bigint",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}

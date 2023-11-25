using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorAppointmentsAPI.Migrations
{
    public partial class ReconfigurationOfEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentsMedications_Appointments_AppointmentId",
                table: "AppointmentsMedications");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyDoctorChanges_Doctors_NewFamilyDoctorId",
                table: "FamilyDoctorChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyDoctorChanges_Doctors_PreviousFamilyDoctorId",
                table: "FamilyDoctorChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyDoctorChanges_Patients_PatientId",
                table: "FamilyDoctorChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Doctors_PatientFamilyDoctorId",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_PatientFamilyDoctorId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_FamilyDoctorChanges_NewFamilyDoctorId",
                table: "FamilyDoctorChanges");

            migrationBuilder.DropIndex(
                name: "IX_FamilyDoctorChanges_PatientId",
                table: "FamilyDoctorChanges");

            migrationBuilder.DropIndex(
                name: "IX_FamilyDoctorChanges_PreviousFamilyDoctorId",
                table: "FamilyDoctorChanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentsMedications",
                table: "AppointmentsMedications");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentsMedications_AppointmentId",
                table: "AppointmentsMedications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AppointmentCode",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientFamilyDoctorId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "NewFamilyDoctorId",
                table: "FamilyDoctorChanges");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "FamilyDoctorChanges");

            migrationBuilder.DropColumn(
                name: "PreviousFamilyDoctorId",
                table: "FamilyDoctorChanges");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "MedicationId",
                table: "AppointmentsMedications");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "AppointmentsMedications");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "PatientSurname",
                table: "Patients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PatientName",
                table: "Patients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PatientGender",
                table: "Patients",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<long>(
                name: "PatientTCId",
                table: "Patients",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "DoctorsDoctorCode",
                table: "Patients",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientFamilyDoctorCode",
                table: "Patients",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewFamilyDoctorCode",
                table: "FamilyDoctorChanges",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "PatientTCId",
                table: "FamilyDoctorChanges",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "PreviousFamilyDoctorCode",
                table: "FamilyDoctorChanges",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DoctorSurname",
                table: "Doctors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorName",
                table: "Doctors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DoctorCode",
                table: "Doctors",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UsageInstructions",
                table: "AppointmentsMedications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MedicationName",
                table: "AppointmentsMedications",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Dosage",
                table: "AppointmentsMedications",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "MedicationCode",
                table: "AppointmentsMedications",
                type: "nvarchar(17)",
                maxLength: 17,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AppointmentCode",
                table: "AppointmentsMedications",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AppointmentCode",
                table: "Appointments",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "DoctorCode",
                table: "Appointments",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "PatientTCId",
                table: "Appointments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "PatientTCId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors",
                column: "DoctorCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentsMedications",
                table: "AppointmentsMedications",
                column: "MedicationCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "AppointmentCode");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoctorsDoctorCode",
                table: "Patients",
                column: "DoctorsDoctorCode");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientFamilyDoctorCode",
                table: "Patients",
                column: "PatientFamilyDoctorCode");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyDoctorChanges_NewFamilyDoctorCode",
                table: "FamilyDoctorChanges",
                column: "NewFamilyDoctorCode");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyDoctorChanges_PatientTCId",
                table: "FamilyDoctorChanges",
                column: "PatientTCId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyDoctorChanges_PreviousFamilyDoctorCode",
                table: "FamilyDoctorChanges",
                column: "PreviousFamilyDoctorCode");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsMedications_AppointmentCode",
                table: "AppointmentsMedications",
                column: "AppointmentCode");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorCode",
                table: "Appointments",
                column: "DoctorCode");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientTCId",
                table: "Appointments",
                column: "PatientTCId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorCode",
                table: "Appointments",
                column: "DoctorCode",
                principalTable: "Doctors",
                principalColumn: "DoctorCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientTCId",
                table: "Appointments",
                column: "PatientTCId",
                principalTable: "Patients",
                principalColumn: "PatientTCId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentsMedications_Appointments_AppointmentCode",
                table: "AppointmentsMedications",
                column: "AppointmentCode",
                principalTable: "Appointments",
                principalColumn: "AppointmentCode");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyDoctorChanges_Doctors_NewFamilyDoctorCode",
                table: "FamilyDoctorChanges",
                column: "NewFamilyDoctorCode",
                principalTable: "Doctors",
                principalColumn: "DoctorCode");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyDoctorChanges_Doctors_PreviousFamilyDoctorCode",
                table: "FamilyDoctorChanges",
                column: "PreviousFamilyDoctorCode",
                principalTable: "Doctors",
                principalColumn: "DoctorCode");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyDoctorChanges_Patients_PatientTCId",
                table: "FamilyDoctorChanges",
                column: "PatientTCId",
                principalTable: "Patients",
                principalColumn: "PatientTCId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Doctors_DoctorsDoctorCode",
                table: "Patients",
                column: "DoctorsDoctorCode",
                principalTable: "Doctors",
                principalColumn: "DoctorCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Doctors_PatientFamilyDoctorCode",
                table: "Patients",
                column: "PatientFamilyDoctorCode",
                principalTable: "Doctors",
                principalColumn: "DoctorCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorCode",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientTCId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentsMedications_Appointments_AppointmentCode",
                table: "AppointmentsMedications");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyDoctorChanges_Doctors_NewFamilyDoctorCode",
                table: "FamilyDoctorChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyDoctorChanges_Doctors_PreviousFamilyDoctorCode",
                table: "FamilyDoctorChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyDoctorChanges_Patients_PatientTCId",
                table: "FamilyDoctorChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Doctors_DoctorsDoctorCode",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Doctors_PatientFamilyDoctorCode",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_DoctorsDoctorCode",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_PatientFamilyDoctorCode",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_FamilyDoctorChanges_NewFamilyDoctorCode",
                table: "FamilyDoctorChanges");

            migrationBuilder.DropIndex(
                name: "IX_FamilyDoctorChanges_PatientTCId",
                table: "FamilyDoctorChanges");

            migrationBuilder.DropIndex(
                name: "IX_FamilyDoctorChanges_PreviousFamilyDoctorCode",
                table: "FamilyDoctorChanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentsMedications",
                table: "AppointmentsMedications");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentsMedications_AppointmentCode",
                table: "AppointmentsMedications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorCode",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientTCId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PatientTCId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DoctorsDoctorCode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientFamilyDoctorCode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "NewFamilyDoctorCode",
                table: "FamilyDoctorChanges");

            migrationBuilder.DropColumn(
                name: "PatientTCId",
                table: "FamilyDoctorChanges");

            migrationBuilder.DropColumn(
                name: "PreviousFamilyDoctorCode",
                table: "FamilyDoctorChanges");

            migrationBuilder.DropColumn(
                name: "DoctorCode",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "MedicationCode",
                table: "AppointmentsMedications");

            migrationBuilder.DropColumn(
                name: "AppointmentCode",
                table: "AppointmentsMedications");

            migrationBuilder.DropColumn(
                name: "DoctorCode",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PatientTCId",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "PatientSurname",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "PatientName",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "PatientGender",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PatientFamilyDoctorId",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NewFamilyDoctorId",
                table: "FamilyDoctorChanges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "FamilyDoctorChanges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PreviousFamilyDoctorId",
                table: "FamilyDoctorChanges",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DoctorSurname",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "DoctorName",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UsageInstructions",
                table: "AppointmentsMedications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "MedicationName",
                table: "AppointmentsMedications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Dosage",
                table: "AppointmentsMedications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "MedicationId",
                table: "AppointmentsMedications",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "AppointmentsMedications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "AppointmentCode",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors",
                column: "DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentsMedications",
                table: "AppointmentsMedications",
                column: "MedicationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientFamilyDoctorId",
                table: "Patients",
                column: "PatientFamilyDoctorId");

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
                name: "IX_AppointmentsMedications_AppointmentId",
                table: "AppointmentsMedications",
                column: "AppointmentId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentsMedications_Appointments_AppointmentId",
                table: "AppointmentsMedications",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyDoctorChanges_Doctors_NewFamilyDoctorId",
                table: "FamilyDoctorChanges",
                column: "NewFamilyDoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyDoctorChanges_Doctors_PreviousFamilyDoctorId",
                table: "FamilyDoctorChanges",
                column: "PreviousFamilyDoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyDoctorChanges_Patients_PatientId",
                table: "FamilyDoctorChanges",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Doctors_PatientFamilyDoctorId",
                table: "Patients",
                column: "PatientFamilyDoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");
        }
    }
}

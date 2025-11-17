using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmbulanceWPF.Migrations
{
    /// <inheritdoc />
    public partial class AddInsuranceToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Insurance",
                table: "Patient",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "InterventionDoctor",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "InterventionDoctor",
                keyColumns: new[] { "DoctorJMB", "InterventionId" },
                keyValues: new object[] { "6482157394021", 1 },
                column: "Role",
                value: "Lead Doctor");

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "JMB",
                keyValue: "4185270936518",
                column: "Insurance",
                value: true);

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "JMB",
                keyValue: "5729618430725",
                column: "Insurance",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Insurance",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "InterventionDoctor");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmbulanceWPF.Migrations
{
    /// <inheritdoc />
    public partial class AddDateOfBirthToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateOfBirth",
                table: "Patient",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "JMB",
                keyValue: "4185270936518",
                column: "DateOfBirth",
                value: "28/02/1995");

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "JMB",
                keyValue: "5729618430725",
                column: "DateOfBirth",
                value: "12/07/2001");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Patient");
        }
    }
}

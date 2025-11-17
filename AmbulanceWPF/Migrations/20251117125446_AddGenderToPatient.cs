using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmbulanceWPF.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "Patient",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "JMB",
                keyValue: "4185270936518",
                column: "Gender",
                value: true);

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "JMB",
                keyValue: "5729618430725",
                column: "Gender",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Patient");
        }
    }
}

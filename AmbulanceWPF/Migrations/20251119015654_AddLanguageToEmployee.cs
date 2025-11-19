using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmbulanceWPF.Migrations
{
    /// <inheritdoc />
    public partial class AddLanguageToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Employee",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "English");

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "JMB",
                keyValue: "6482157394021",
                column: "Language",
                value: "English");

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "JMB",
                keyValue: "9035172846109",
                column: "Language",
                value: "English");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "Employee");
        }
    }
}

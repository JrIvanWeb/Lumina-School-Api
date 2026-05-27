using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuminiSchool.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNameToAcademicPeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Agregar columna Name a AcademicPeriods
            migrationBuilder.AddColumn<string>(
                name:         "Name",
                table:        "AcademicPeriods",
                type:         "nvarchar(max)",
                nullable:     true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name:  "Name",
                table: "AcademicPeriods");
        }
    }
}

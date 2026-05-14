using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuminiSchool.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionAndOrderToGrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Grades");

            migrationBuilder.RenameColumn(
                name: "Section",
                table: "Grades",
                newName: "Description");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Grades",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Grades");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Grades",
                newName: "Section");

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Grades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

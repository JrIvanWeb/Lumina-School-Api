using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuminiSchool.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEnrollmentModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Guardians");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Guardians",
                newName: "FullName");

            migrationBuilder.AddColumn<string>(
                name: "BirthPlace",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BloodType",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Eps",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PreviousInstitution",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Relationship",
                table: "Guardians",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "Guardians",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "Guardians",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Guardians",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GuardianId",
                table: "Enrollments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentParents",
                columns: table => new
                {
                    ParentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentParents", x => new { x.ParentsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_StudentParents_Parents_ParentsId",
                        column: x => x.ParentsId,
                        principalTable: "Parents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentParents_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guardians_ParentId",
                table: "Guardians",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_GuardianId",
                table: "Enrollments",
                column: "GuardianId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentParents_StudentsId",
                table: "StudentParents",
                column: "StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Guardians_GuardianId",
                table: "Enrollments",
                column: "GuardianId",
                principalTable: "Guardians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guardians_Parents_ParentId",
                table: "Guardians",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Guardians_GuardianId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Guardians_Parents_ParentId",
                table: "Guardians");

            migrationBuilder.DropTable(
                name: "StudentParents");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Guardians_ParentId",
                table: "Guardians");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_GuardianId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "BirthPlace",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Eps",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PreviousInstitution",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "Guardians");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "Guardians");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Guardians");

            migrationBuilder.DropColumn(
                name: "GuardianId",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Guardians",
                newName: "LastName");

            migrationBuilder.AlterColumn<string>(
                name: "Relationship",
                table: "Guardians",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Guardians",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

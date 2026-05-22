using System;
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
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Subjects_SubjectId",
                table: "Achievements");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Teachers_TeacherId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Achievements");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "Achievements",
                newName: "Performance");

            migrationBuilder.AlterColumn<string>(
                name: "Indicator",
                table: "Achievements",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Achievement",
                table: "Achievements",
                type: "nvarchar(800)",
                maxLength: 800,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "GradeId",
                table: "Achievements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "NoteMax",
                table: "Achievements",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NoteMin",
                table: "Achievements",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "PeriodId",
                table: "Achievements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Achievements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_GradeId",
                table: "Achievements",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_PeriodId_GradeId_SubjectId_Performance",
                table: "Achievements",
                columns: new[] { "PeriodId", "GradeId", "SubjectId", "Performance" });

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_AcademicPeriods_PeriodId",
                table: "Achievements",
                column: "PeriodId",
                principalTable: "AcademicPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Grades_GradeId",
                table: "Achievements",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Subjects_SubjectId",
                table: "Achievements",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Teachers_TeacherId",
                table: "Activities",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_AcademicPeriods_PeriodId",
                table: "Achievements");

            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Grades_GradeId",
                table: "Achievements");

            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Subjects_SubjectId",
                table: "Achievements");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Teachers_TeacherId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Achievements_GradeId",
                table: "Achievements");

            migrationBuilder.DropIndex(
                name: "IX_Achievements_PeriodId_GradeId_SubjectId_Performance",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "Achievement",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "NoteMax",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "NoteMin",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "PeriodId",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Achievements");

            migrationBuilder.RenameColumn(
                name: "Performance",
                table: "Achievements",
                newName: "Level");

            migrationBuilder.AlterColumn<string>(
                name: "Indicator",
                table: "Achievements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Achievements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Achievements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Subjects_SubjectId",
                table: "Achievements",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Teachers_TeacherId",
                table: "Activities",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuminiSchool.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSubjectGradeRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_GradeSubjects_Grades_GradeEntityId",
                table: "GradeSubjects");

            migrationBuilder.RenameColumn(
                name: "GradeEntityId",
                table: "GradeSubjects",
                newName: "GradesId");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeSubjects_Grades_GradesId",
                table: "GradeSubjects",
                column: "GradesId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GradeSubjects_Grades_GradesId",
                table: "GradeSubjects");

            migrationBuilder.RenameColumn(
                name: "GradesId",
                table: "GradeSubjects",
                newName: "GradeEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeSubjects_Grades_GradeEntityId",
                table: "GradeSubjects",
                column: "GradeEntityId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuminiSchool.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExpandAchievementsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Renombrar columna Level → Performance (enum alineado con frontend)
            migrationBuilder.RenameColumn(
                name:  "Level",
                table: "Achievements",
                newName: "Performance");

            // 2. Renombrar Title → Achievement (campo principal del logro)
            migrationBuilder.RenameColumn(
                name:  "Title",
                table: "Achievements",
                newName: "Achievement");

            // 3. Renombrar Description → (se elimina; el logro queda en Achievement)
            //    Si Description tenía contenido útil, se migra a Achievement.
            migrationBuilder.Sql(@"
                UPDATE Achievements
                SET Achievement = COALESCE(NULLIF(LTRIM(RTRIM(Achievement)), ''), Description)
                WHERE Description IS NOT NULL AND Description <> ''
            ");

            migrationBuilder.DropColumn(name: "Description", table: "Achievements");

            // 4. Agregar PeriodId (FK a AcademicPeriods)
            migrationBuilder.AddColumn<Guid>(
                name:       "PeriodId",
                table:      "Achievements",
                type:       "uniqueidentifier",
                nullable:   false,
                defaultValue: Guid.Empty);

            // 5. Agregar GradeId (FK a Grades)
            migrationBuilder.AddColumn<Guid>(
                name:       "GradeId",
                table:      "Achievements",
                type:       "uniqueidentifier",
                nullable:   false,
                defaultValue: Guid.Empty);

            // 6. Agregar NoteMin y NoteMax
            migrationBuilder.AddColumn<decimal>(
                name:      "NoteMin",
                table:     "Achievements",
                type:      "decimal(4,2)",
                nullable:  false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name:      "NoteMax",
                table:     "Achievements",
                type:      "decimal(4,2)",
                nullable:  false,
                defaultValue: 5m);

            // 7. Agregar UpdatedAt
            migrationBuilder.AddColumn<DateTime>(
                name:         "UpdatedAt",
                table:        "Achievements",
                type:         "datetime2",
                nullable:     false,
                defaultValueSql: "GETUTCDATE()");

            // 8. Ampliar Indicator a nvarchar(400)
            migrationBuilder.AlterColumn<string>(
                name:      "Indicator",
                table:     "Achievements",
                type:      "nvarchar(400)",
                maxLength: 400,
                nullable:  true,
                oldClrType: typeof(string),
                oldType:   "nvarchar(max)",
                oldNullable: true);

            // 9. Ampliar Achievement a nvarchar(800)
            migrationBuilder.AlterColumn<string>(
                name:      "Achievement",
                table:     "Achievements",
                type:      "nvarchar(800)",
                maxLength: 800,
                nullable:  false,
                oldClrType: typeof(string),
                oldType:   "nvarchar(max)");

            // 10. FK → AcademicPeriods
            migrationBuilder.AddForeignKey(
                name:            "FK_Achievements_AcademicPeriods_PeriodId",
                table:           "Achievements",
                column:          "PeriodId",
                principalTable:  "AcademicPeriods",
                principalColumn: "Id",
                onDelete:        ReferentialAction.Restrict);

            // 11. FK → Grades
            migrationBuilder.AddForeignKey(
                name:            "FK_Achievements_Grades_GradeId",
                table:           "Achievements",
                column:          "GradeId",
                principalTable:  "Grades",
                principalColumn: "Id",
                onDelete:        ReferentialAction.Restrict);

            // 12. Índice compuesto para búsquedas frecuentes
            migrationBuilder.CreateIndex(
                name:    "IX_Achievements_PeriodId_GradeId_SubjectId_Performance",
                table:   "Achievements",
                columns: new[] { "PeriodId", "GradeId", "SubjectId", "Performance" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name:  "IX_Achievements_PeriodId_GradeId_SubjectId_Performance",
                table: "Achievements");

            migrationBuilder.DropForeignKey(name: "FK_Achievements_AcademicPeriods_PeriodId", table: "Achievements");
            migrationBuilder.DropForeignKey(name: "FK_Achievements_Grades_GradeId",           table: "Achievements");

            migrationBuilder.DropColumn(name: "PeriodId",   table: "Achievements");
            migrationBuilder.DropColumn(name: "GradeId",    table: "Achievements");
            migrationBuilder.DropColumn(name: "NoteMin",    table: "Achievements");
            migrationBuilder.DropColumn(name: "NoteMax",    table: "Achievements");
            migrationBuilder.DropColumn(name: "UpdatedAt",  table: "Achievements");

            migrationBuilder.RenameColumn(name: "Performance", table: "Achievements", newName: "Level");
            migrationBuilder.RenameColumn(name: "Achievement",  table: "Achievements", newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Description", table: "Achievements",
                type: "nvarchar(max)", nullable: false, defaultValue: "");
        }
    }
}

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
                name:    "Level",
                table:   "Achievements",
                newName: "Performance");

            // 2. Renombrar Title → Achievement (campo principal del logro)
            migrationBuilder.RenameColumn(
                name:    "Title",
                table:   "Achievements",
                newName: "Achievement");

            // 3. Migrar Description a Achievement y eliminar la columna
            migrationBuilder.Sql(@"
                UPDATE Achievements
                SET Achievement = COALESCE(NULLIF(LTRIM(RTRIM(Achievement)), ''), Description)
                WHERE Description IS NOT NULL AND Description <> ''
            ");

            migrationBuilder.DropColumn(name: "Description", table: "Achievements");

            // 4. Agregar PeriodId como NULLABLE primero (evita conflicto de FK con Guid.Empty)
            migrationBuilder.AddColumn<Guid>(
                name:      "PeriodId",
                table:     "Achievements",
                type:      "uniqueidentifier",
                nullable:  true);

            // 5. Agregar GradeId como NULLABLE primero
            migrationBuilder.AddColumn<Guid>(
                name:      "GradeId",
                table:     "Achievements",
                type:      "uniqueidentifier",
                nullable:  true);

            // 6. Asignar valores válidos a los registros existentes
            //    Se toma el primer Period y Grade disponibles como valor de relleno para datos legacy.
            migrationBuilder.Sql(@"
                DECLARE @periodId uniqueidentifier = (SELECT TOP 1 Id FROM AcademicPeriods ORDER BY (SELECT NULL));
                DECLARE @gradeId  uniqueidentifier = (SELECT TOP 1 Id FROM Grades ORDER BY (SELECT NULL));

                UPDATE Achievements
                SET PeriodId = @periodId,
                    GradeId  = @gradeId
                WHERE PeriodId IS NULL OR GradeId IS NULL;
            ");

            // 7. Convertir PeriodId a NOT NULL
            migrationBuilder.AlterColumn<Guid>(
                name:       "PeriodId",
                table:      "Achievements",
                type:       "uniqueidentifier",
                nullable:   false,
                oldClrType: typeof(Guid),
                oldType:    "uniqueidentifier",
                oldNullable: true);

            // 8. Convertir GradeId a NOT NULL
            migrationBuilder.AlterColumn<Guid>(
                name:       "GradeId",
                table:      "Achievements",
                type:       "uniqueidentifier",
                nullable:   false,
                oldClrType: typeof(Guid),
                oldType:    "uniqueidentifier",
                oldNullable: true);

            // 9. Agregar NoteMin y NoteMax
            migrationBuilder.AddColumn<decimal>(
                name:         "NoteMin",
                table:        "Achievements",
                type:         "decimal(4,2)",
                nullable:     false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name:         "NoteMax",
                table:        "Achievements",
                type:         "decimal(4,2)",
                nullable:     false,
                defaultValue: 5m);

            // 10. Agregar UpdatedAt
            migrationBuilder.AddColumn<DateTime>(
                name:            "UpdatedAt",
                table:           "Achievements",
                type:            "datetime2",
                nullable:        false,
                defaultValueSql: "GETUTCDATE()");

            // 11. Ampliar Indicator a nvarchar(400)
            migrationBuilder.AlterColumn<string>(
                name:       "Indicator",
                table:      "Achievements",
                type:       "nvarchar(400)",
                maxLength:  400,
                nullable:   true,
                oldClrType: typeof(string),
                oldType:    "nvarchar(max)",
                oldNullable: true);

            // 12. Ampliar Achievement a nvarchar(800)
            migrationBuilder.AlterColumn<string>(
                name:       "Achievement",
                table:      "Achievements",
                type:       "nvarchar(800)",
                maxLength:  800,
                nullable:   false,
                oldClrType: typeof(string),
                oldType:    "nvarchar(max)");

            // 13. FK → AcademicPeriods
            migrationBuilder.AddForeignKey(
                name:            "FK_Achievements_AcademicPeriods_PeriodId",
                table:           "Achievements",
                column:          "PeriodId",
                principalTable:  "AcademicPeriods",
                principalColumn: "Id",
                onDelete:        ReferentialAction.Restrict);

            // 14. FK → Grades
            migrationBuilder.AddForeignKey(
                name:            "FK_Achievements_Grades_GradeId",
                table:           "Achievements",
                column:          "GradeId",
                principalTable:  "Grades",
                principalColumn: "Id",
                onDelete:        ReferentialAction.Restrict);

            // 15. Índice compuesto para búsquedas frecuentes
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

            migrationBuilder.DropColumn(name: "PeriodId",  table: "Achievements");
            migrationBuilder.DropColumn(name: "GradeId",   table: "Achievements");
            migrationBuilder.DropColumn(name: "NoteMin",   table: "Achievements");
            migrationBuilder.DropColumn(name: "NoteMax",   table: "Achievements");
            migrationBuilder.DropColumn(name: "UpdatedAt", table: "Achievements");

            migrationBuilder.RenameColumn(name: "Performance", table: "Achievements", newName: "Level");
            migrationBuilder.RenameColumn(name: "Achievement",  table: "Achievements", newName: "Title");

            migrationBuilder.AddColumn<string>(
                name:         "Description",
                table:        "Achievements",
                type:         "nvarchar(max)",
                nullable:     false,
                defaultValue: "");
        }
    }
}

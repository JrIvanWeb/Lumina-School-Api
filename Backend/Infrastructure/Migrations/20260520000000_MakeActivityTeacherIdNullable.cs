using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuminiSchool.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeActivityTeacherIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Eliminar el FK existente (NOT NULL) hacia Teachers
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Teachers_TeacherId",
                table: "Activities");

            // 2. Hacer la columna nullable
            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "Activities",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            // 3. Recrear el FK pero ahora con nullable (OnDelete = NoAction para evitar ciclos)
            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Teachers_TeacherId",
                table: "Activities",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Teachers_TeacherId",
                table: "Activities");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "Activities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Teachers_TeacherId",
                table: "Activities",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

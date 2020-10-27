using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceSystem.Data.Migrations
{
    public partial class FixingStudentAttendace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Students_studentId",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "studentId",
                table: "Attendances",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_studentId",
                table: "Attendances",
                newName: "IX_Attendances_StudentId");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Attendances",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Students_StudentId",
                table: "Attendances",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Students_StudentId",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Attendances",
                newName: "studentId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances",
                newName: "IX_Attendances_studentId");

            migrationBuilder.AlterColumn<int>(
                name: "studentId",
                table: "Attendances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Students_studentId",
                table: "Attendances",
                column: "studentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

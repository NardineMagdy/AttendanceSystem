using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceSystem.Data.Migrations
{
    public partial class MadeEventIdNotRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Events_EventId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Students",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Events_EventId",
                table: "Students",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Events_EventId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Students",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Events_EventId",
                table: "Students",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roebi.Migrations
{
    public partial class upPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Patient",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Patient_RoomId",
                table: "Patient",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Room_RoomId",
                table: "Patient",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Room_RoomId",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Patient_RoomId",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Patient");
        }
    }
}

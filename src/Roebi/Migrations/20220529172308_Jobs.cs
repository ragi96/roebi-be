using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roebi.Migrations
{
    public partial class Jobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "JobId",
                table: "Medication",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Medication_JobId",
                table: "Medication",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medication_Job_JobId",
                table: "Medication",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medication_Job_JobId",
                table: "Medication");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Medication_JobId",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Medication");
        }
    }
}

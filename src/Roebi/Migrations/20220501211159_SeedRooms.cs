using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roebi.Migrations
{
    public partial class SeedRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Room(Name) Values ('Raum 1')");
            migrationBuilder.Sql("INSERT INTO Room(Name) Values ('Raum 2')");
            migrationBuilder.Sql("INSERT INTO Room(Name) Values ('Raum 3')");
            migrationBuilder.Sql("INSERT INTO Room(Name) Values ('Raum 4')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE Room");
        }
    }
}

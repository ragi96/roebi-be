using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roebi.Migrations
{
    public partial class SeedUserAndRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Room(Name) Values ('Raum 1')");
            migrationBuilder.Sql("INSERT INTO Room(Name) Values ('Raum 2')");
            migrationBuilder.Sql("INSERT INTO Room(Name) Values ('Raum 3')");
            migrationBuilder.Sql("INSERT INTO Room(Name) Values ('Raum 4')");

            var adminPw = BCrypt.Net.BCrypt.HashPassword("admin");
            var userPw = BCrypt.Net.BCrypt.HashPassword("user");
            var robPw = BCrypt.Net.BCrypt.HashPassword("roboter");

            migrationBuilder.Sql($"INSERT INTO User(FirstName,LastName,Username,PasswordHash,Role) VALUES ('Admin', 'User', 'admin', '{adminPw}', 1)");
            migrationBuilder.Sql($"INSERT INTO User(FirstName,LastName,Username,PasswordHash,Role) VALUES ('User', 'User', 'user', '{userPw}', 2)");
            migrationBuilder.Sql($"INSERT INTO User(FirstName,LastName,Username,PasswordHash,Role) VALUES ('roebi', 'User', 'roebi','{robPw}', 3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE Room");
            migrationBuilder.Sql("TRUNCATE User");
        }
    }
}

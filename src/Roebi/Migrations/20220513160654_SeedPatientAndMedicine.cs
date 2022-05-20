using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roebi.Migrations
{
    public partial class SeedPatientAndMedicine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Medicine(Name) Values ('Medikament 1')");
            migrationBuilder.Sql("INSERT INTO Medicine(Name) Values ('Medikament 2')");
            migrationBuilder.Sql("INSERT INTO Medicine(Name) Values ('Medikament 3')");
            migrationBuilder.Sql("INSERT INTO Medicine(Name) Values ('Medikament 4')");

            migrationBuilder.Sql("INSERT INTO Patient(LastName,Firstname,EntryStamp,ExitStamp,CaseHistory,RoomId) Values ('Müller','Peter', 1653055234, 0, 'Hat Kopfschmerzen', 1)");
            migrationBuilder.Sql("INSERT INTO Patient(LastName,Firstname,EntryStamp,ExitStamp,CaseHistory,RoomId) Values ('Solomon','Marius', 1653055234, 0, 'Hat Bauchschmerzen', 2)");
            migrationBuilder.Sql("INSERT INTO Patient(LastName,Firstname,EntryStamp,ExitStamp,CaseHistory,RoomId) Values ('Rogers','Damian', 1653055234, 0, 'Hat Gliederschmerzen', 3)");
            migrationBuilder.Sql("INSERT INTO Patient(LastName,Firstname,EntryStamp,ExitStamp,CaseHistory,RoomId) Values ('Ritter','Kane', 1653055234, 0, 'Hat ein gebrochenes Bein', 4)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE Medicine");
            migrationBuilder.Sql("TRUNCATE Patient");
        }
    }
}

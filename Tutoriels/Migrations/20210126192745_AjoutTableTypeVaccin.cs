using Microsoft.EntityFrameworkCore.Migrations;

namespace Vaccins.Migrations
{
    public partial class AjoutTableTypeVaccin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_vaccins",
                table: "vaccins");

            migrationBuilder.RenameTable(
                name: "vaccins",
                newName: "Vaccins");

            migrationBuilder.AddColumn<int>(
                name: "TypeVaccinId",
                table: "Vaccins",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vaccins",
                table: "Vaccins",
                column: "VaccinId");

            migrationBuilder.CreateTable(
                name: "TypeVaccins",
                columns: table => new
                {
                    TypeVaccinId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeVaccins", x => x.TypeVaccinId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vaccins_TypeVaccinId",
                table: "Vaccins",
                column: "TypeVaccinId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccins_TypeVaccins_TypeVaccinId",
                table: "Vaccins",
                column: "TypeVaccinId",
                principalTable: "TypeVaccins",
                principalColumn: "TypeVaccinId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vaccins_TypeVaccins_TypeVaccinId",
                table: "Vaccins");

            migrationBuilder.DropTable(
                name: "TypeVaccins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vaccins",
                table: "Vaccins");

            migrationBuilder.DropIndex(
                name: "IX_Vaccins_TypeVaccinId",
                table: "Vaccins");

            migrationBuilder.DropColumn(
                name: "TypeVaccinId",
                table: "Vaccins");

            migrationBuilder.RenameTable(
                name: "Vaccins",
                newName: "vaccins");

            migrationBuilder.AddPrimaryKey(
                name: "PK_vaccins",
                table: "vaccins",
                column: "VaccinId");
        }
    }
}

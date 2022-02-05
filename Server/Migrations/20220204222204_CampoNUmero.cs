using Microsoft.EntityFrameworkCore.Migrations;

namespace ProvaCode7.Server.Migrations
{
    public partial class CampoNUmero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Numero",
                table: "Endereco",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Endereco");
        }
    }
}

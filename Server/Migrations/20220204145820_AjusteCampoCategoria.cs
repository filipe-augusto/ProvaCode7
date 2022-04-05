using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoCallCenter.Server.Migrations
{
    public partial class AjusteCampoCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "CategoriaProduto",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "CategoriaProduto");
        }
    }
}

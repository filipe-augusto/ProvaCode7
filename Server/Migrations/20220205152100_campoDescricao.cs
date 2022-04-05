using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoCallCenter.Server.Migrations
{
    public partial class campoDescricao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "RegistroAtendimentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "RegistroAtendimentos");
        }
    }
}

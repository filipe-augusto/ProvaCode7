using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProvaCode7.Server.Migrations
{
    public partial class relacaoAtendimentoOferta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdRegistroAtendimento",
                table: "ProdutoOfertadoCliente",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RegistroAtendimentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsAtivo = table.Column<bool>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    IdCliente = table.Column<int>(nullable: false),
                    NomeDaMaquinaOuIP = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroAtendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_RegistroAtendimentoso_Cliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoOfertadoCliente_IdRegistroAtendimento",
                table: "ProdutoOfertadoCliente",
                column: "IdRegistroAtendimento");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroAtendimentos_IdCliente",
                table: "RegistroAtendimentos",
                column: "IdCliente");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ForeignKey_ProdutoOfertadoCliente_RegistroAtendimentos",
                table: "ProdutoOfertadoCliente");

            migrationBuilder.DropTable(
                name: "RegistroAtendimentos");

            migrationBuilder.DropIndex(
                name: "IX_ProdutoOfertadoCliente_IdRegistroAtendimento",
                table: "ProdutoOfertadoCliente");

            migrationBuilder.DropColumn(
                name: "IdRegistroAtendimento",
                table: "ProdutoOfertadoCliente");
        }
    }
}

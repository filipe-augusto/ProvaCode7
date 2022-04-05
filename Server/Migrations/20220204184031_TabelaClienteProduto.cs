using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoCallCenter.Server.Migrations
{
    public partial class TabelaClienteProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProdutoOfertadoCliente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdCliente = table.Column<int>(nullable: false),
                    IdProduto = table.Column<int>(nullable: false),
                    DiaDaOferta = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoOfertadoCliente", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_ProdutoOfertadoCliente_Cliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_ProdutoOfertadoCliente_Produto",
                        column: x => x.IdProduto,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoOfertadoCliente_IdCliente",
                table: "ProdutoOfertadoCliente",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoOfertadoCliente_IdProduto",
                table: "ProdutoOfertadoCliente",
                column: "IdProduto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdutoOfertadoCliente");
        }
    }
}

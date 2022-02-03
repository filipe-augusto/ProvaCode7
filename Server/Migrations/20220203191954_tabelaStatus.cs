using Microsoft.EntityFrameworkCore.Migrations;

namespace ProvaCode7.Server.Migrations
{
    public partial class tabelaStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusCliente",
                columns: table => new
                {
                    IdStatus = table.Column<byte>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    IsContabilizaVenda = table.Column<bool>(nullable: false),
                    IsFinalizaCliente = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusCliente", x => x.IdStatus);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_IdStatus",
                table: "Cliente",
                column: "IdStatus");

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_Cliente_StatusCliente",
                table: "Cliente",
                column: "IdStatus",
                principalTable: "StatusCliente",
                principalColumn: "IdStatus",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ForeignKey_Cliente_StatusCliente",
                table: "Cliente");

            migrationBuilder.DropTable(
                name: "StatusCliente");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_IdStatus",
                table: "Cliente");
        }
    }
}

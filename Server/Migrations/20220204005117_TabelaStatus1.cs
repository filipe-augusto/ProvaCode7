using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProvaCode7.Server.Migrations
{
    public partial class TabelaStatus1 : Migration
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

        }


   

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropTable(
                name: "StatusCliente");

        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegracaoVendas.Data.Migrations
{
    public partial class AddInformacoesCorreios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "INFORMACOES_CORREIOS",
                columns: table => new
                {
                    OBJETO = table.Column<string>(nullable: false),
                    NOTA = table.Column<string>(nullable: true),
                    STATUS = table.Column<int>(nullable: false),
                    DATA = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INFORMACOES_CORREIOS", x => x.OBJETO);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INFORMACOES_CORREIOS");
        }
    }
}

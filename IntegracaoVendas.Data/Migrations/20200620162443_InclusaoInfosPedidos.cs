using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegracaoVendas.Data.Migrations
{
    public partial class InclusaoInfosPedidos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "INFORMACOES_PEDIDOS",
                columns: table => new
                {
                    PEDIDO = table.Column<string>(nullable: false),
                    VOLUME = table.Column<int>(nullable: false),
                    PESO = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INFORMACOES_PEDIDOS", x => x.PEDIDO);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INFORMACOES_PEDIDOS");
        }
    }
}

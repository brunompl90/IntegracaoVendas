using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegracaoVendas.Data.Migrations
{
    public partial class NovosCampoShipping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "Shipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine3",
                table: "Shipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine4",
                table: "Shipments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "AddressLine3",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "AddressLine4",
                table: "Shipments");
        }
    }
}

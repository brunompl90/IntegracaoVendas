using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegracaoVendas.Data.Migrations
{
    public partial class NovosCamposAdress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "BillingAddresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine3",
                table: "BillingAddresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine4",
                table: "BillingAddresses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "BillingAddresses");

            migrationBuilder.DropColumn(
                name: "AddressLine3",
                table: "BillingAddresses");

            migrationBuilder.DropColumn(
                name: "AddressLine4",
                table: "BillingAddresses");
        }
    }
}

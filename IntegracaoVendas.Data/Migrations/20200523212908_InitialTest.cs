using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegracaoVendas.Data.Migrations
{
    public partial class InitialTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderNumber = table.Column<string>(nullable: false),
                    CustomerCode = table.Column<string>(nullable: true),
                    LineTotal = table.Column<string>(nullable: true),
                    CustomerTotal = table.Column<string>(nullable: true),
                    CustomerDiscountTotal = table.Column<string>(nullable: true),
                    DiscountCode = table.Column<string>(nullable: true),
                    CustomerTaxTotal = table.Column<string>(nullable: true),
                    VatCode = table.Column<string>(nullable: true),
                    Culture = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderNumber);
                });

            migrationBuilder.CreateTable(
                name: "BillingAddresses",
                columns: table => new
                {
                    BillingAddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    AddressLine1 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CountryCode = table.Column<string>(nullable: true),
                    CountryName = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingAddresses", x => x.BillingAddressId);
                    table.ForeignKey(
                        name: "FK_BillingAddresses_Orders_OrderNumber",
                        column: x => x.OrderNumber,
                        principalTable: "Orders",
                        principalColumn: "OrderNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LineItens",
                columns: table => new
                {
                    LineItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(nullable: true),
                    LineNumber = table.Column<string>(nullable: true),
                    VariantId = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Quantity = table.Column<string>(nullable: true),
                    UnitListPrice = table.Column<string>(nullable: true),
                    UnitCustomerPrice = table.Column<string>(nullable: true),
                    LineListPrice = table.Column<string>(nullable: true),
                    LineCustomerPrice = table.Column<string>(nullable: true),
                    LineTaxAmount = table.Column<string>(nullable: true),
                    TaxPercentage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItens", x => x.LineItemId);
                    table.ForeignKey(
                        name: "FK_LineItens_Orders_OrderNumber",
                        column: x => x.OrderNumber,
                        principalTable: "Orders",
                        principalColumn: "OrderNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<string>(nullable: false),
                    OrderNumber = table.Column<string>(nullable: true),
                    PaymentType = table.Column<string>(nullable: true),
                    PaymentCurrency = table.Column<string>(nullable: true),
                    PaymentValue = table.Column<string>(nullable: true),
                    AdyenInstallmentsNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderNumber",
                        column: x => x.OrderNumber,
                        principalTable: "Orders",
                        principalColumn: "OrderNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    ShipmentID = table.Column<string>(nullable: false),
                    OrderNumber = table.Column<string>(nullable: true),
                    ShippingMethodId = table.Column<string>(nullable: true),
                    ShippingMethodName = table.Column<string>(nullable: true),
                    ShipmentAmount = table.Column<string>(nullable: true),
                    ShipmentTaxAmount = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    AddressLine1 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CountryCode = table.Column<string>(nullable: true),
                    CountryName = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.ShipmentID);
                    table.ForeignKey(
                        name: "FK_Shipments_Orders_OrderNumber",
                        column: x => x.OrderNumber,
                        principalTable: "Orders",
                        principalColumn: "OrderNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillingAddresses_OrderNumber",
                table: "BillingAddresses",
                column: "OrderNumber",
                unique: true,
                filter: "[OrderNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LineItens_OrderNumber",
                table: "LineItens",
                column: "OrderNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderNumber",
                table: "Payments",
                column: "OrderNumber",
                unique: true,
                filter: "[OrderNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_OrderNumber",
                table: "Shipments",
                column: "OrderNumber",
                unique: true,
                filter: "[OrderNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingAddresses");

            migrationBuilder.DropTable(
                name: "LineItens");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}

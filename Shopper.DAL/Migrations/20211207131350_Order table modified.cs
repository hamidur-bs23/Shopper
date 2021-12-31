using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.DAL.Migrations
{
    public partial class Ordertablemodified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Products",
                newName: "StockInHand");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StockInHand",
                table: "Products",
                newName: "Quantity");
        }
    }
}

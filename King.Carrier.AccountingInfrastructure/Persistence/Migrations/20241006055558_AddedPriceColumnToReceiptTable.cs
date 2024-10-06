using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace King.Carrier.AccountingInfrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedPriceColumnToReceiptTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Receipts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Receipts");
        }
    }
}

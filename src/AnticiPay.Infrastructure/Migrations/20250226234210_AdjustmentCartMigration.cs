using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnticiPay.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustmentCartMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "NetValueAtCheckout",
                table: "Invoices",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckoutDate",
                table: "Carts",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxRateAtCheckout",
                table: "Carts",
                type: "decimal(65,30)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetValueAtCheckout",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CheckoutDate",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "TaxRateAtCheckout",
                table: "Carts");
        }
    }
}

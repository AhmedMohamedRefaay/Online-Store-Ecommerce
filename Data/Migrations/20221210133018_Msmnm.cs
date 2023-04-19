using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Data.Migrations
{
    public partial class Msmnm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderDetails_products_PorductId",
                table: "orderDetails");

            migrationBuilder.DropIndex(
                name: "IX_orderDetails_PorductId",
                table: "orderDetails");

            migrationBuilder.DropColumn(
                name: "PorductId",
                table: "orderDetails");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "orderDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_orderDetails_ProductId",
                table: "orderDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_orderDetails_products_ProductId",
                table: "orderDetails",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderDetails_products_ProductId",
                table: "orderDetails");

            migrationBuilder.DropIndex(
                name: "IX_orderDetails_ProductId",
                table: "orderDetails");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "orderDetails");

            migrationBuilder.AddColumn<int>(
                name: "PorductId",
                table: "orderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_orderDetails_PorductId",
                table: "orderDetails",
                column: "PorductId");

            migrationBuilder.AddForeignKey(
                name: "FK_orderDetails_products_PorductId",
                table: "orderDetails",
                column: "PorductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

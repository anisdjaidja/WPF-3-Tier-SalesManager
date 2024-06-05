using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPF_N_Tier_Test_Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class AddedProductToBatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TransactionBatch_ProductId",
                table: "TransactionBatch",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionBatch_Products_ProductId",
                table: "TransactionBatch",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionBatch_Products_ProductId",
                table: "TransactionBatch");

            migrationBuilder.DropIndex(
                name: "IX_TransactionBatch_ProductId",
                table: "TransactionBatch");
        }
    }
}

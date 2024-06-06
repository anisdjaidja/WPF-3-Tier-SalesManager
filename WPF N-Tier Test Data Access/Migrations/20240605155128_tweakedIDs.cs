using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPF_N_Tier_Test_Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class tweakedIDs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipmentDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ValidationDate",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ShipmentDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidationDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);
        }
    }
}

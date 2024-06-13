using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddInventoryReceipts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inventory",
                columns: table => new
                {
                    i_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    i_sn = table.Column<string>(type: "nchar(127)", nullable: true),
                    i_name = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory", x => x.i_id);
                });

            migrationBuilder.CreateTable(
                name: "inventory_receipt",
                columns: table => new
                {
                    ir_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ir_price = table.Column<decimal>(type: "decimal(10,0)", nullable: false),
                    ir_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    InventoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_receipt", x => x.ir_id);
                    table.ForeignKey(
                        name: "FK_inventory_receipt_inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "inventory",
                        principalColumn: "i_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventory_receipt_supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "supplier",
                        principalColumn: "s_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_inventory_receipt_InventoryId",
                table: "inventory_receipt",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_receipt_SupplierId",
                table: "inventory_receipt",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inventory_receipt");

            migrationBuilder.DropTable(
                name: "inventory");
        }
    }
}

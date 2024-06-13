using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApplication.Migrations
{
    /// <inheritdoc />
    public partial class RenameForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventory_receipt_inventory_InventoryId",
                table: "inventory_receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_inventory_receipt_supplier_SupplierId",
                table: "inventory_receipt");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "inventory_receipt",
                newName: "ir_supplier");

            migrationBuilder.RenameColumn(
                name: "InventoryId",
                table: "inventory_receipt",
                newName: "ir_inventory");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_receipt_SupplierId",
                table: "inventory_receipt",
                newName: "IX_inventory_receipt_ir_supplier");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_receipt_InventoryId",
                table: "inventory_receipt",
                newName: "IX_inventory_receipt_ir_inventory");

            migrationBuilder.CreateTable(
                name: "inventory_transfer",
                columns: table => new
                {
                    it_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    it_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    it_operation = table.Column<int>(type: "int", nullable: false),
                    it_signatory = table.Column<int>(type: "int", nullable: false),
                    it_department = table.Column<int>(type: "int", nullable: false),
                    it_department_responsible = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_transfer", x => x.it_id);
                    table.ForeignKey(
                        name: "FK_inventory_transfer_department_it_department",
                        column: x => x.it_department,
                        principalTable: "department",
                        principalColumn: "d_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventory_transfer_employee_it_department_responsible",
                        column: x => x.it_department_responsible,
                        principalTable: "employee",
                        principalColumn: "e_id");
                    table.ForeignKey(
                        name: "FK_inventory_transfer_employee_it_signatory",
                        column: x => x.it_signatory,
                        principalTable: "employee",
                        principalColumn: "e_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventory_transfer_operation_it_operation",
                        column: x => x.it_operation,
                        principalTable: "operation",
                        principalColumn: "o_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inventory_transfer_list",
                columns: table => new
                {
                    itl_transfer = table.Column<int>(type: "int", nullable: false),
                    itl_inventory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_transfer_list", x => new { x.itl_transfer, x.itl_inventory });
                    table.ForeignKey(
                        name: "FK_inventory_transfer_list_inventory_itl_inventory",
                        column: x => x.itl_inventory,
                        principalTable: "inventory",
                        principalColumn: "i_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventory_transfer_list_inventory_transfer_itl_transfer",
                        column: x => x.itl_transfer,
                        principalTable: "inventory_transfer",
                        principalColumn: "it_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_inventory_transfer_it_department",
                table: "inventory_transfer",
                column: "it_department");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_transfer_it_department_responsible",
                table: "inventory_transfer",
                column: "it_department_responsible");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_transfer_it_operation",
                table: "inventory_transfer",
                column: "it_operation");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_transfer_it_signatory",
                table: "inventory_transfer",
                column: "it_signatory");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_transfer_list_itl_inventory",
                table: "inventory_transfer_list",
                column: "itl_inventory");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_receipt_inventory_ir_inventory",
                table: "inventory_receipt",
                column: "ir_inventory",
                principalTable: "inventory",
                principalColumn: "i_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_receipt_supplier_ir_supplier",
                table: "inventory_receipt",
                column: "ir_supplier",
                principalTable: "supplier",
                principalColumn: "s_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventory_receipt_inventory_ir_inventory",
                table: "inventory_receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_inventory_receipt_supplier_ir_supplier",
                table: "inventory_receipt");

            migrationBuilder.DropTable(
                name: "inventory_transfer_list");

            migrationBuilder.DropTable(
                name: "inventory_transfer");

            migrationBuilder.RenameColumn(
                name: "ir_supplier",
                table: "inventory_receipt",
                newName: "SupplierId");

            migrationBuilder.RenameColumn(
                name: "ir_inventory",
                table: "inventory_receipt",
                newName: "InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_receipt_ir_supplier",
                table: "inventory_receipt",
                newName: "IX_inventory_receipt_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_receipt_ir_inventory",
                table: "inventory_receipt",
                newName: "IX_inventory_receipt_InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_receipt_inventory_InventoryId",
                table: "inventory_receipt",
                column: "InventoryId",
                principalTable: "inventory",
                principalColumn: "i_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_receipt_supplier_SupplierId",
                table: "inventory_receipt",
                column: "SupplierId",
                principalTable: "supplier",
                principalColumn: "s_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

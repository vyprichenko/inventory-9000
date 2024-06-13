using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApplication.Migrations
{
    /// <inheritdoc />
    public partial class DepartmentResponsibleEmployeeFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_department_employee_ResponsibleEmployeeId",
                table: "department");

            migrationBuilder.RenameColumn(
                name: "ResponsibleEmployeeId",
                table: "department",
                newName: "d_responsible");

            migrationBuilder.RenameIndex(
                name: "IX_department_ResponsibleEmployeeId",
                table: "department",
                newName: "IX_department_d_responsible");

            migrationBuilder.AddForeignKey(
                name: "FK_department_employee_d_responsible",
                table: "department",
                column: "d_responsible",
                principalTable: "employee",
                principalColumn: "e_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_department_employee_d_responsible",
                table: "department");

            migrationBuilder.RenameColumn(
                name: "d_responsible",
                table: "department",
                newName: "ResponsibleEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_department_d_responsible",
                table: "department",
                newName: "IX_department_ResponsibleEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_department_employee_ResponsibleEmployeeId",
                table: "department",
                column: "ResponsibleEmployeeId",
                principalTable: "employee",
                principalColumn: "e_id");
        }
    }
}

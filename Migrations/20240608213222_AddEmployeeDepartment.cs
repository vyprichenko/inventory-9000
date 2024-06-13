using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    e_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    e_first_name = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    e_last_name = table.Column<string>(type: "nvarchar(127)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.e_id);
                });

            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    d_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    d_name = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    ResponsibleEmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.d_id);
                    table.ForeignKey(
                        name: "FK_department_employee_ResponsibleEmployeeId",
                        column: x => x.ResponsibleEmployeeId,
                        principalTable: "employee",
                        principalColumn: "e_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_department_ResponsibleEmployeeId",
                table: "department",
                column: "ResponsibleEmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "department");

            migrationBuilder.DropTable(
                name: "employee");
        }
    }
}

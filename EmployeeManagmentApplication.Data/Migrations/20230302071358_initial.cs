using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagmentApplication.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeFirstName = table.Column<string>(nullable: false),
                    EmployeeLastName = table.Column<string>(nullable: false),
                    EmailId = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    AdharCard = table.Column<string>(nullable: true),
                    PanCllard = table.Column<string>(nullable: true),
                    EmployeeProfilePhoto = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "SalaryModule",
                columns: table => new
                {
                    SalaryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    Basic = table.Column<double>(nullable: false),
                    HRA = table.Column<double>(nullable: false),
                    DA = table.Column<double>(nullable: false),
                    PT = table.Column<double>(nullable: false),
                    Deduction = table.Column<double>(nullable: false),
                    NetSalary = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryModule", x => x.SalaryId);
                    table.ForeignKey(
                        name: "FK_SalaryModule_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalaryModule_EmployeeId",
                table: "SalaryModule",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaryModule");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}

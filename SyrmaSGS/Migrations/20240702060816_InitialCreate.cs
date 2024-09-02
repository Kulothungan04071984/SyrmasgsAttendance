using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyrmaSGS.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeMaster",
                columns: table => new
                {
                    SNO = table.Column<double>(name: "S.NO", type: "float", nullable: true),
                    EMPLOYEEID = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: true),
                    EMPLOYEENAME = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DOB = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DOJ = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GENDER = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CATEGORY_NAME = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DESIGNATION_NAME = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DEPARTMENT_NAME = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EMAILID = table.Column<string>(name: "E-MAIL ID", type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UNIT = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    REPORTINGMANAGERID = table.Column<double>(name: "REPORTING MANAGER ID", type: "float", nullable: true),
                    REPORTINGMANAGERNAME = table.Column<string>(name: "REPORTING MANAGER NAME", type: "nvarchar(255)", maxLength: 255, nullable: true),
                    REPORTINGMANAGEREMAILID = table.Column<string>(name: "REPORTING MANAGER E-MAIL ID", type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeMaster");
        }
    }
}

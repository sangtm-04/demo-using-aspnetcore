using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Migrations
{
    public partial class AddRoleCompanyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleCompany",
                table: "UserRoleCompany");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleCompany",
                table: "UserRoleCompany",
                columns: new[] { "CompanyId", "RoleId", "UserId" });

            migrationBuilder.CreateTable(
                name: "RoleCompany",
                columns: table => new
                {
                    CompanyId = table.Column<string>(maxLength: 36, nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleCompany", x => new { x.CompanyId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RoleCompany_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleCompany_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleCompany_RoleId",
                table: "RoleCompany",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleCompany");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleCompany",
                table: "UserRoleCompany");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleCompany",
                table: "UserRoleCompany",
                columns: new[] { "CompanyId", "UserId", "RoleId" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarcAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPhotoTable12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_companies_CompanyId",
                table: "Photo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photo",
                table: "Photo");

            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "photos");

            migrationBuilder.RenameIndex(
                name: "IX_Photo_CompanyId",
                table: "photos",
                newName: "IX_photos_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_photos",
                table: "photos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_photos_EmployeeId",
                table: "photos",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_photos_companies_CompanyId",
                table: "photos",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_photos_employees_EmployeeId",
                table: "photos",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_photos_companies_CompanyId",
                table: "photos");

            migrationBuilder.DropForeignKey(
                name: "FK_photos_employees_EmployeeId",
                table: "photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_photos",
                table: "photos");

            migrationBuilder.DropIndex(
                name: "IX_photos_EmployeeId",
                table: "photos");

            migrationBuilder.RenameTable(
                name: "photos",
                newName: "Photo");

            migrationBuilder.RenameIndex(
                name: "IX_photos_CompanyId",
                table: "Photo",
                newName: "IX_Photo_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photo",
                table: "Photo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_companies_CompanyId",
                table: "Photo",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id");
        }
    }
}

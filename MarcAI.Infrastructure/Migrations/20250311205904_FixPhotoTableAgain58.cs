using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarcAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPhotoTableAgain58 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "photos",
                newName: "WebUrl");

            migrationBuilder.AddColumn<string>(
                name: "PathName",
                table: "photos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathName",
                table: "photos");

            migrationBuilder.RenameColumn(
                name: "WebUrl",
                table: "photos",
                newName: "Url");
        }
    }
}

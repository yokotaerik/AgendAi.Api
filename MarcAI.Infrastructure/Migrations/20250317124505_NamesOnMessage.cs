using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarcAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NamesOnMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                table: "messages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "messages",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverName",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "SenderName",
                table: "messages");
        }
    }
}

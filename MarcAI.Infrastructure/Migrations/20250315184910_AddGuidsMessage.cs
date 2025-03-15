using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarcAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGuidsMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "messages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ReceiverId",
                table: "messages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SenderId",
                table: "messages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_messages_Id",
                table: "messages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_messages_ReceiverId",
                table: "messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_SenderId",
                table: "messages",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_messages_Id",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "IX_messages_ReceiverId",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "IX_messages_SenderId",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "messages");
        }
    }
}

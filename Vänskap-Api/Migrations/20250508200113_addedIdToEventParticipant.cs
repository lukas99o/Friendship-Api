using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vänskap_Api.Migrations
{
    /// <inheritdoc />
    public partial class addedIdToEventParticipant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventParticipants",
                table: "EventParticipants");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "EventParticipants");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EventParticipants",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinedAt",
                table: "ConversationParticipants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "ConversationParticipants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventParticipants",
                table: "EventParticipants",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipants_UserId",
                table: "EventParticipants",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventParticipants",
                table: "EventParticipants");

            migrationBuilder.DropIndex(
                name: "IX_EventParticipants_UserId",
                table: "EventParticipants");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EventParticipants");

            migrationBuilder.DropColumn(
                name: "JoinedAt",
                table: "ConversationParticipants");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "ConversationParticipants");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "EventParticipants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventParticipants",
                table: "EventParticipants",
                columns: new[] { "UserId", "EventId" });
        }
    }
}

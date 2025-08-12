using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vänskap_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddingMessagesInEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_EventId",
                table: "Messages",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Events_EventId",
                table: "Messages",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Events_EventId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_EventId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Messages");
        }
    }
}

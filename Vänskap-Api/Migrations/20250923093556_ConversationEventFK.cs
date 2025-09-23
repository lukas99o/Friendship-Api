using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vänskap_Api.Migrations
{
    /// <inheritdoc />
    public partial class ConversationEventFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Conversations_ConversationId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_ConversationId",
                table: "Events");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_EventId",
                table: "Conversations",
                column: "EventId",
                unique: true,
                filter: "[EventId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Events_EventId",
                table: "Conversations",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Events_EventId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_EventId",
                table: "Conversations");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ConversationId",
                table: "Events",
                column: "ConversationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Conversations_ConversationId",
                table: "Events",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

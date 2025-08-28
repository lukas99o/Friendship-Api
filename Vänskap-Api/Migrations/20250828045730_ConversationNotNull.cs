using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vänskap_Api.Migrations
{
    /// <inheritdoc />
    public partial class ConversationNotNull : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Conversations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_EventId",
                table: "Conversations",
                column: "EventId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Events_EventId",
                table: "Conversations",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Conversations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

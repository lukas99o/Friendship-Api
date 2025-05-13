using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vänskap_Api.Migrations
{
    /// <inheritdoc />
    public partial class FixingBugs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrivateConversationId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PrivateConversation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateConversation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PrivateConversationId",
                table: "Messages",
                column: "PrivateConversationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_PrivateConversation_PrivateConversationId",
                table: "Messages",
                column: "PrivateConversationId",
                principalTable: "PrivateConversation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_PrivateConversation_PrivateConversationId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "PrivateConversation");

            migrationBuilder.DropIndex(
                name: "IX_Messages_PrivateConversationId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "PrivateConversationId",
                table: "Messages");
        }
    }
}

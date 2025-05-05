using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vänskap_Api.Migrations
{
    /// <inheritdoc />
    public partial class InterestRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interests_AspNetUsers_ApplicationUserId",
                table: "Interests");

            migrationBuilder.DropForeignKey(
                name: "FK_Interests_Events_EventId",
                table: "Interests");

            migrationBuilder.DropIndex(
                name: "IX_Interests_ApplicationUserId",
                table: "Interests");

            migrationBuilder.DropIndex(
                name: "IX_Interests_EventId",
                table: "Interests");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Interests");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Interests");

            migrationBuilder.CreateTable(
                name: "EventInterests",
                columns: table => new
                {
                    EventsId = table.Column<int>(type: "int", nullable: false),
                    InterestsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventInterests", x => new { x.EventsId, x.InterestsId });
                    table.ForeignKey(
                        name: "FK_EventInterests_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventInterests_Interests_InterestsId",
                        column: x => x.InterestsId,
                        principalTable: "Interests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInterests",
                columns: table => new
                {
                    InterestsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInterests", x => new { x.InterestsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserInterests_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInterests_Interests_InterestsId",
                        column: x => x.InterestsId,
                        principalTable: "Interests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventInterests_InterestsId",
                table: "EventInterests",
                column: "InterestsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInterests_UsersId",
                table: "UserInterests",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventInterests");

            migrationBuilder.DropTable(
                name: "UserInterests");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Interests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Interests",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "ApplicationUserId", "EventId" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Interests_ApplicationUserId",
                table: "Interests",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Interests_EventId",
                table: "Interests",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interests_AspNetUsers_ApplicationUserId",
                table: "Interests",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Interests_Events_EventId",
                table: "Interests",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}

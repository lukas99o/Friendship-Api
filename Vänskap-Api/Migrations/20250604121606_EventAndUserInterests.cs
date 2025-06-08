using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Vänskap_Api.Migrations
{
    /// <inheritdoc />
    public partial class EventAndUserInterests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventInterests");

            migrationBuilder.DropTable(
                name: "UserInterests");

            migrationBuilder.AddColumn<int>(
                name: "InterestId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InterestId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventInterest",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    InterestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventInterest", x => new { x.EventId, x.InterestId });
                    table.ForeignKey(
                        name: "FK_EventInterest_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventInterest_Interests_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInterest",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InterestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInterest", x => new { x.UserId, x.InterestId });
                    table.ForeignKey(
                        name: "FK_UserInterest_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInterest_Interests_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EventInterest",
                columns: new[] { "EventId", "InterestId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 4 },
                    { 4, 5 },
                    { 5, 6 }
                });

            migrationBuilder.InsertData(
                table: "EventParticipants",
                columns: new[] { "Id", "EventId", "JoinedAt", "Role", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5946), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 2, 2, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5949), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 3, 3, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5951), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 4, 4, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5953), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 5, 5, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5981), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 6, 6, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5983), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 7, 7, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5986), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 8, 8, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5988), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 9, 9, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5990), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 10, 10, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5992), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 11, 11, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5995), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 12, 12, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5997), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 13, 13, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5999), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 14, 14, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(6001), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 15, 15, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(6004), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 16, 16, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(6006), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 17, 17, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(6008), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 18, 18, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(6010), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 19, 19, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(6013), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" },
                    { 20, 20, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(6015), "Host", "89fff030-be5c-40a2-903d-82f5c6ffef6a" }
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime", "Title" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5679), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5675), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5635), "Cooking & Travel Night" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime", "Title" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5686), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5685), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5684), "Photography Workshop" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime", "Title" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5689), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5688), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5687), "Fitness Bootcamp" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime", "Title" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5692), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5691), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5690), "Morning Run" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime", "Title" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5695), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5694), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5693), "Hiking Adventure" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5698), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5697), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5696) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5702), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5701), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5700) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5705), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5704), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5703) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5709), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5708), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5707) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5713), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5712), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5711) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5717), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5716), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5715) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5721), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5720), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5718) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5724), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5723), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5722) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5727), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5726), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5725) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5730), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5729), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5728) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5733), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5732), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5731) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5736), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5735), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5734) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5739), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5738), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5737) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5742), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5741), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5740) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "EndTime", "InterestId", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 12, 16, 5, 679, DateTimeKind.Utc).AddTicks(5745), new DateTime(2025, 6, 5, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5744), null, new DateTime(2025, 6, 4, 14, 16, 5, 679, DateTimeKind.Local).AddTicks(5743) });

            migrationBuilder.CreateIndex(
                name: "IX_Events_InterestId",
                table: "Events",
                column: "InterestId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_InterestId",
                table: "AspNetUsers",
                column: "InterestId");

            migrationBuilder.CreateIndex(
                name: "IX_EventInterest_InterestId",
                table: "EventInterest",
                column: "InterestId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInterest_InterestId",
                table: "UserInterest",
                column: "InterestId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Interests_InterestId",
                table: "AspNetUsers",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Interests_InterestId",
                table: "Events",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Interests_InterestId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Interests_InterestId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "EventInterest");

            migrationBuilder.DropTable(
                name: "UserInterest");

            migrationBuilder.DropIndex(
                name: "IX_Events_InterestId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_InterestId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DropColumn(
                name: "InterestId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "InterestId",
                table: "AspNetUsers");

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

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndTime", "StartTime", "Title" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9476), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9473), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9424), "Pool Party" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EndTime", "StartTime", "Title" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9484), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9483), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9482), "Board Game Night" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "EndTime", "StartTime", "Title" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9487), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9486), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9485), "Coding Jam" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "EndTime", "StartTime", "Title" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9490), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9489), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9488), "Pizza Friday" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "EndTime", "StartTime", "Title" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9493), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9492), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9491), "Karaoke Night" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9496), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9495), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9494) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9499), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9498), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9533), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9532), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9530) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9536), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9535), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9534) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9539), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9538), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9537) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9542), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9541), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9540) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9545), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9544), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9543) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9548), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9547), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9546) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9551), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9550), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9549) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9554), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9553), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9552) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9557), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9556), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9555) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9560), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9559), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9558) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9564), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9563), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9562) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9567), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9566), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9565) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9569), new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9568) });

            migrationBuilder.CreateIndex(
                name: "IX_EventInterests_InterestsId",
                table: "EventInterests",
                column: "InterestsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInterests_UsersId",
                table: "UserInterests",
                column: "UsersId");
        }
    }
}

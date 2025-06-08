using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Vänskap_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddingEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AgeRangeMax", "AgeRangeMin", "CreatedAt", "CreatedByUserId", "Description", "EndTime", "IsPublic", "Location", "StartTime", "Title" },
                values: new object[,]
                {
                    { 1, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9476), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9473), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9424), "Pool Party" },
                    { 2, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9484), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9483), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9482), "Board Game Night" },
                    { 3, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9487), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9486), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9485), "Coding Jam" },
                    { 4, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9490), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9489), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9488), "Pizza Friday" },
                    { 5, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9493), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9492), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9491), "Karaoke Night" },
                    { 6, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9496), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9495), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9494), "Book Club" },
                    { 7, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9499), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9498), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9497), "Hiking Trip" },
                    { 8, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9533), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9532), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9530), "Wine Tasting" },
                    { 9, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9536), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9535), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9534), "Sushi Workshop" },
                    { 10, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9539), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9538), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9537), "Beach Cleanup" },
                    { 11, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9542), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9541), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9540), "Stand-up Comedy" },
                    { 12, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9545), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9544), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9543), "Coffee Meetup" },
                    { 13, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9548), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9547), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9546), "Art & Chill" },
                    { 14, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9551), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9550), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9549), "Outdoor Yoga" },
                    { 15, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9554), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9553), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9552), "Language Exchange" },
                    { 16, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9557), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9556), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9555), "Bike Ride" },
                    { 17, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9560), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9559), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9558), "Movie Marathon" },
                    { 18, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9564), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9563), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9562), "Street Food Tour" },
                    { 19, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9567), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9566), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9565), "Photography Walk" },
                    { 20, null, null, new DateTime(2025, 5, 29, 8, 55, 18, 533, DateTimeKind.Utc).AddTicks(9570), "89fff030-be5c-40a2-903d-82f5c6ffef6a", null, new DateTime(2025, 5, 30, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9569), true, null, new DateTime(2025, 5, 29, 10, 55, 18, 533, DateTimeKind.Local).AddTicks(9568), "Midnight Picnic" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}

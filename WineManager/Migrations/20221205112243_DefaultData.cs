using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WineManager.Migrations
{
    /// <inheritdoc />
    public partial class DefaultData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bottles",
                columns: new[] { "BottleId", "Color", "Designation", "DrawerId", "DrawerPosition", "EndKeepingYear", "Name", "StartKeepingYear", "UserId", "Vintage" },
                values: new object[,]
                {
                    { 2, "red", "Pessac-Leognan", null, null, 8, "Chateau Pape Clement", 5, null, 2007 },
                    { 3, "red", "Pessac-Leognan", null, null, 8, "Chateau Pape Clement", 5, null, 2007 },
                    { 4, "White", "Riesling", null, null, 8, "Krick Vin D'Alsace", 5, null, 2017 },
                    { 5, "White", "Riesling", null, null, 8, "Krick Vin D'Alsace", 5, null, 2017 },
                    { 6, "White", "Riesling", null, null, 8, "Krick Vin D'Alsace", 5, null, 2017 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "BirthDate", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 5, 12, 22, 43, 263, DateTimeKind.Local).AddTicks(8741), "Jerry.Seinfeld@aol.com", "Jerry Seinfeld", "password" },
                    { 2, new DateTime(10, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "George.Costanza@aol.com", "George Costanza", "george" },
                    { 3, new DateTime(10, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elaine.Benes@aol.com", "Elaine Benes", "jerry" },
                    { 4, new DateTime(10, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cosmo.Kramer@aol.com", "Cosmo Kramer", "qzerty" }
                });

            migrationBuilder.InsertData(
                table: "Caves",
                columns: new[] { "CaveId", "Brand", "CaveType", "Family", "Temperature", "UserId" },
                values: new object[,]
                {
                    { 1, "Acme", "Batman", "Wayne", 12, 1 },
                    { 2, "Acme", "Batman", "Wayne", 12, 2 }
                });

            migrationBuilder.InsertData(
                table: "Drawers",
                columns: new[] { "DrawerId", "CaveId", "Level", "MaxPosition", "UserId" },
                values: new object[,]
                {
                    { 1, 2, 1, 10, 2 },
                    { 2, 1, 2, 6, 1 }
                });

            migrationBuilder.InsertData(
                table: "Bottles",
                columns: new[] { "BottleId", "Color", "Designation", "DrawerId", "DrawerPosition", "EndKeepingYear", "Name", "StartKeepingYear", "UserId", "Vintage" },
                values: new object[] { 1, "red", "Pessac-Leognan", 2, "1", 8, "Chateau Pape Clement", 5, 2, 2007 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Drawers",
                keyColumn: "DrawerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Caves",
                keyColumn: "CaveId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Drawers",
                keyColumn: "DrawerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Caves",
                keyColumn: "CaveId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}

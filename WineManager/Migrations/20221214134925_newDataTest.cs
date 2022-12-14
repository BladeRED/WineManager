using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WineManager.Migrations
{
    /// <inheritdoc />
    public partial class newDataTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StartKeepingYear",
                table: "Bottles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EndKeepingYear",
                table: "Bottles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 1,
                columns: new[] { "DrawerId", "DrawerPosition" },
                values: new object[] { 1, "A1" });

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 2,
                columns: new[] { "DrawerId", "DrawerPosition" },
                values: new object[] { 1, "A2" });

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 3,
                columns: new[] { "DrawerId", "DrawerPosition", "UserId" },
                values: new object[] { 1, "A3", 1 });

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 4,
                columns: new[] { "DrawerId", "DrawerPosition" },
                values: new object[] { 1, "A4" });

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 5,
                columns: new[] { "DrawerId", "DrawerPosition", "UserId" },
                values: new object[] { 2, "B1", 1 });

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 6,
                columns: new[] { "DrawerId", "DrawerPosition", "UserId" },
                values: new object[] { 2, "B2", 1 });

            migrationBuilder.InsertData(
                table: "Bottles",
                columns: new[] { "BottleId", "Color", "Designation", "DrawerId", "DrawerPosition", "EndKeepingYear", "Name", "StartKeepingYear", "UserId", "Vintage" },
                values: new object[,]
                {
                    { 7, "red", "Pessac-Leognan", 2, "B3", 8, "Chateau Pape Clement", 5, 1, 2007 },
                    { 8, "red", "Pessac-Leognan", 2, "B4", 8, "Chateau Pape Clement", 5, 1, 2007 },
                    { 9, "red", "Pessac-Leognan", 2, "B5", 8, "Chateau Pape Clement", 5, 1, 2007 },
                    { 10, "White", "Riesling", 2, "B6", 8, "Krick Vin D'Alsace", 5, 1, 2017 },
                    { 11, "White", "Riesling", null, null, 8, "Krick Vin D'Alsace", 5, 2, 2017 },
                    { 12, "White", "Riesling", null, null, 8, "Krick Vin D'Alsace", 5, 3, 2017 }
                });

            migrationBuilder.UpdateData(
                table: "Caves",
                keyColumn: "CaveId",
                keyValue: 1,
                columns: new[] { "Brand", "CaveType", "Family", "NbMaxDrawer", "Temperature" },
                values: new object[] { "Liebherr", "Cellar of the kitchen", "Service cellar", 6, 16 });

            migrationBuilder.UpdateData(
                table: "Caves",
                keyColumn: "CaveId",
                keyValue: 2,
                columns: new[] { "Brand", "CaveType", "Family", "NbMaxDrawer", "Temperature", "UserId" },
                values: new object[] { "La Sommelière", "Garage cellar", "Cellar of guard", 6, 14, 1 });

            migrationBuilder.UpdateData(
                table: "Drawers",
                keyColumn: "DrawerId",
                keyValue: 1,
                columns: new[] { "CaveId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Drawers",
                columns: new[] { "DrawerId", "CaveId", "Level", "MaxPosition", "UserId" },
                values: new object[,]
                {
                    { 3, 1, 3, 6, 1 },
                    { 4, 1, 4, 6, 1 },
                    { 5, 1, 5, 6, 1 },
                    { 6, 1, 6, 6, 1 },
                    { 7, 2, 1, 6, 1 },
                    { 8, 2, 2, 6, 1 },
                    { 9, 2, 3, 6, 1 },
                    { 10, 2, 4, 6, 1 },
                    { 11, 2, 5, 6, 1 },
                    { 12, 2, 6, 6, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "BirthDate", "Email", "Password" },
                values: new object[] { new DateTime(2022, 12, 14, 14, 49, 25, 345, DateTimeKind.Local).AddTicks(4298), "jerry@aol.com", "pwd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Drawers",
                keyColumn: "DrawerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Drawers",
                keyColumn: "DrawerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Drawers",
                keyColumn: "DrawerId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Drawers",
                keyColumn: "DrawerId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Drawers",
                keyColumn: "DrawerId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Drawers",
                keyColumn: "DrawerId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Drawers",
                keyColumn: "DrawerId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Drawers",
                keyColumn: "DrawerId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Drawers",
                keyColumn: "DrawerId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Drawers",
                keyColumn: "DrawerId",
                keyValue: 12);

            migrationBuilder.AlterColumn<int>(
                name: "StartKeepingYear",
                table: "Bottles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EndKeepingYear",
                table: "Bottles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 1,
                columns: new[] { "DrawerId", "DrawerPosition" },
                values: new object[] { 2, "1" });

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 2,
                columns: new[] { "DrawerId", "DrawerPosition" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 3,
                columns: new[] { "DrawerId", "DrawerPosition", "UserId" },
                values: new object[] { null, null, 3 });

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 4,
                columns: new[] { "DrawerId", "DrawerPosition" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 5,
                columns: new[] { "DrawerId", "DrawerPosition", "UserId" },
                values: new object[] { null, null, 2 });

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 6,
                columns: new[] { "DrawerId", "DrawerPosition", "UserId" },
                values: new object[] { null, null, 3 });

            migrationBuilder.UpdateData(
                table: "Caves",
                keyColumn: "CaveId",
                keyValue: 1,
                columns: new[] { "Brand", "CaveType", "Family", "NbMaxDrawer", "Temperature" },
                values: new object[] { "Acme", "Batman", "Wayne", 8, 12 });

            migrationBuilder.UpdateData(
                table: "Caves",
                keyColumn: "CaveId",
                keyValue: 2,
                columns: new[] { "Brand", "CaveType", "Family", "NbMaxDrawer", "Temperature", "UserId" },
                values: new object[] { "Acme", "Batman", "Wayne", 8, 12, 2 });

            migrationBuilder.UpdateData(
                table: "Drawers",
                keyColumn: "DrawerId",
                keyValue: 1,
                columns: new[] { "CaveId", "UserId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "BirthDate", "Email", "Password" },
                values: new object[] { new DateTime(2022, 12, 12, 12, 34, 16, 778, DateTimeKind.Local).AddTicks(6963), "Jerry.Seinfeld@aol.com", "password" });
        }
    }
}

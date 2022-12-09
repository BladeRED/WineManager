using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineManager.Migrations
{
    /// <inheritdoc />
    public partial class addInfoOnCave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bottles_Users_UserId",
                table: "Bottles");

            migrationBuilder.DropForeignKey(
                name: "FK_Caves_Users_UserId",
                table: "Caves");

            migrationBuilder.DropForeignKey(
                name: "FK_Drawers_Users_UserId",
                table: "Drawers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Drawers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "Drawers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Caves",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NbMaxBottlePerDrawer",
                table: "Caves",
                type: "int",
                nullable: false,
                defaultValue: 8);

            migrationBuilder.AddColumn<int>(
                name: "NbMaxDrawer",
                table: "Caves",
                type: "int",
                nullable: false,
                defaultValue: 8);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
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
                keyValue: 2,
                column: "UserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 3,
                column: "UserId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 4,
                column: "UserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 5,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 6,
                column: "UserId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Caves",
                keyColumn: "CaveId",
                keyValue: 1,
                columns: new[] { "NbMaxBottlePerDrawer", "NbMaxDrawer" },
                values: new object[] { 6, 8 });

            migrationBuilder.UpdateData(
                table: "Caves",
                keyColumn: "CaveId",
                keyValue: 2,
                columns: new[] { "NbMaxBottlePerDrawer", "NbMaxDrawer" },
                values: new object[] { 6, 8 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "BirthDate",
                value: new DateTime(2022, 12, 9, 11, 47, 39, 93, DateTimeKind.Local).AddTicks(7928));

            migrationBuilder.AddForeignKey(
                name: "FK_Bottles_Users_UserId",
                table: "Bottles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Caves_Users_UserId",
                table: "Caves",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Drawers_Users_UserId",
                table: "Drawers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bottles_Users_UserId",
                table: "Bottles");

            migrationBuilder.DropForeignKey(
                name: "FK_Caves_Users_UserId",
                table: "Caves");

            migrationBuilder.DropForeignKey(
                name: "FK_Drawers_Users_UserId",
                table: "Drawers");

            migrationBuilder.DropColumn(
                name: "NbMaxBottlePerDrawer",
                table: "Caves");

            migrationBuilder.DropColumn(
                name: "NbMaxDrawer",
                table: "Caves");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Drawers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "Drawers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Caves",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Bottles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 2,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 3,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 4,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 5,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bottles",
                keyColumn: "BottleId",
                keyValue: 6,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "BirthDate",
                value: new DateTime(2022, 12, 5, 12, 22, 43, 263, DateTimeKind.Local).AddTicks(8741));

            migrationBuilder.AddForeignKey(
                name: "FK_Bottles_Users_UserId",
                table: "Bottles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Caves_Users_UserId",
                table: "Caves",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drawers_Users_UserId",
                table: "Drawers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}

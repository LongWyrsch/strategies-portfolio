using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Strategies.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAddColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TradeCount",
                table: "Results",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<DateTime>(
                name: "ChartEndTime",
                table: "Results",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ChartStartTime",
                table: "Results",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "ChartTotalDays",
                table: "Results",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RatioTimeTrading",
                table: "Results",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChartEndTime",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "ChartStartTime",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "ChartTotalDays",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "RatioTimeTrading",
                table: "Results");

            migrationBuilder.AlterColumn<double>(
                name: "TradeCount",
                table: "Results",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}

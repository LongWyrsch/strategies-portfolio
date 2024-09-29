using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Strategies.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddColToTrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Resolution",
                table: "Trades",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Strategy",
                table: "Trades",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Trades",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resolution",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "Strategy",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "Trades");
        }
    }
}

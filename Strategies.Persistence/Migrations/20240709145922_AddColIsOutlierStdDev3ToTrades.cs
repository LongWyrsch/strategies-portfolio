using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Strategies.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddColIsOutlierStdDev3ToTrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOutlierStdDev3",
                table: "Trades",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOutlierStdDev3",
                table: "Trades");
        }
    }
}

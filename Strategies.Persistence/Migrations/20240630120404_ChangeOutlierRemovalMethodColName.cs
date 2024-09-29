using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Strategies.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOutlierRemovalMethodColName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OutlierType",
                table: "Results",
                newName: "OutlierRemovalMethod");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OutlierRemovalMethod",
                table: "Results",
                newName: "OutlierType");
        }
    }
}

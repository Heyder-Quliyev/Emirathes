using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace emirathes.Migrations
{
    /// <inheritdoc />
    public partial class edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightType",
                table: "Ticktes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FlightType",
                table: "Ticktes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

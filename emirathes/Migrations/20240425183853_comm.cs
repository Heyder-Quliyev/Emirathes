using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace emirathes.Migrations
{
    /// <inheritdoc />
    public partial class comm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Landing",
                table: "Ticktes");

            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "Ticktes");

            migrationBuilder.RenameColumn(
                name: "Takeoff",
                table: "Ticktes",
                newName: "Way");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Ticktes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Way",
                table: "Ticktes",
                newName: "Takeoff");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Ticktes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Landing",
                table: "Ticktes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "Ticktes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

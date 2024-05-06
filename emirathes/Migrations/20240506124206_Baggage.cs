using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace emirathes.Migrations
{
    /// <inheritdoc />
    public partial class Baggage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "CabinBaggage");

            migrationBuilder.AddColumn<string>(
                name: "Baggage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Baggage",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CabinBaggage",
                table: "Products",
                newName: "Description");
        }
    }
}

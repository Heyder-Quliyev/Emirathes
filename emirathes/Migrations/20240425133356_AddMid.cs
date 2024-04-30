using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace emirathes.Migrations
{
    /// <inheritdoc />
    public partial class AddMid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Ticktes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FlightType",
                table: "Ticktes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Ticktes");

            migrationBuilder.DropColumn(
                name: "FlightType",
                table: "Ticktes");
        }
    }
}

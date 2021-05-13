using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoadIBKData.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Prices");

            migrationBuilder.AddColumn<DateTime>(
                name: "startDate",
                table: "Prices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "startDate",
                table: "Prices");

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Prices",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

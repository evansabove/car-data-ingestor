using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDataIngestor.Migrations
{
    public partial class AddIngestedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "IngestedDate",
                table: "Drives",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IngestedDate",
                table: "Drives");
        }
    }
}

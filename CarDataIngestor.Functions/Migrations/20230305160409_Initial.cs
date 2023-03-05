using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDataIngestor.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriveSnapshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false),
                    CoolantTemp = table.Column<double>(type: "float", nullable: false),
                    EngineLoad = table.Column<double>(type: "float", nullable: false),
                    RPM = table.Column<double>(type: "float", nullable: false),
                    Speed = table.Column<double>(type: "float", nullable: false),
                    IntakeTemperature = table.Column<double>(type: "float", nullable: false),
                    FuelLevel = table.Column<double>(type: "float", nullable: false),
                    DriveId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriveSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriveSnapshots_Drives_DriveId",
                        column: x => x.DriveId,
                        principalTable: "Drives",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriveSnapshots_DriveId",
                table: "DriveSnapshots",
                column: "DriveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriveSnapshots");

            migrationBuilder.DropTable(
                name: "Drives");
        }
    }
}

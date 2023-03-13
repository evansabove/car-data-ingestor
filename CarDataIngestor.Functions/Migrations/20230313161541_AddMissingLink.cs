using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDataIngestor.Migrations
{
    public partial class AddMissingLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriveSnapshots_Drives_DriveId",
                table: "DriveSnapshots");

            migrationBuilder.AlterColumn<Guid>(
                name: "DriveId",
                table: "DriveSnapshots",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DriveSnapshots_Drives_DriveId",
                table: "DriveSnapshots",
                column: "DriveId",
                principalTable: "Drives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriveSnapshots_Drives_DriveId",
                table: "DriveSnapshots");

            migrationBuilder.AlterColumn<Guid>(
                name: "DriveId",
                table: "DriveSnapshots",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_DriveSnapshots_Drives_DriveId",
                table: "DriveSnapshots",
                column: "DriveId",
                principalTable: "Drives",
                principalColumn: "Id");
        }
    }
}

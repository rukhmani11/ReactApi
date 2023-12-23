using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class CBUAddedSubSegmentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BusinessSubSegmentId",
                table: "ClientBusinessUnits",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("32f9ce05-c92d-4422-8cfc-c791d1a56547"));

            migrationBuilder.CreateIndex(
                name: "IX_ClientBusinessUnits_BusinessSubSegmentId",
                table: "ClientBusinessUnits",
                column: "BusinessSubSegmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientBusinessUnit_BusinessSubSegment",
                table: "ClientBusinessUnits",
                column: "BusinessSubSegmentId",
                principalTable: "BusinessSubSegments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientBusinessUnit_BusinessSubSegment",
                table: "ClientBusinessUnits");

            migrationBuilder.DropIndex(
                name: "IX_ClientBusinessUnits_BusinessSubSegmentId",
                table: "ClientBusinessUnits");

            migrationBuilder.DropColumn(
                name: "BusinessSubSegmentId",
                table: "ClientBusinessUnits");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("c5a3ba1e-e0ac-4b8f-bb28-c406af22e882"));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class ClientEmployeeColDuplicateRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientEmployees_ClientBusinessUnits_ClientBusinessUnitId1",
                table: "ClientEmployees");

            migrationBuilder.DropIndex(
                name: "IX_ClientEmployees_ClientBusinessUnitId1",
                table: "ClientEmployees");

            migrationBuilder.DropColumn(
                name: "ClientBusinessUnitId1",
                table: "ClientEmployees");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("32ff7ca8-015d-4097-9c47-0b238203d503"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClientBusinessUnitId1",
                table: "ClientEmployees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("8a59ab33-b644-4828-80e8-08767e128ae5"));

            migrationBuilder.CreateIndex(
                name: "IX_ClientEmployees_ClientBusinessUnitId1",
                table: "ClientEmployees",
                column: "ClientBusinessUnitId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientEmployees_ClientBusinessUnits_ClientBusinessUnitId1",
                table: "ClientEmployees",
                column: "ClientBusinessUnitId1",
                principalTable: "ClientBusinessUnits",
                principalColumn: "Id");
        }
    }
}

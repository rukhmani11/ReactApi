using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class visitedColumnsInMeeting1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancelRemark",
                table: "Meetings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VisitedClientBusinessUnitId",
                table: "Meetings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VisitedClientEmployeeId",
                table: "Meetings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VisitedCompanyUserId",
                table: "Meetings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("47c0a92b-2be8-4c7d-bdf0-1ee81c2532fe"));

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_VisitedClientBusinessUnitId",
                table: "Meetings",
                column: "VisitedClientBusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_VisitedClientEmployeeId",
                table: "Meetings",
                column: "VisitedClientEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_VisitedCompanyUserId",
                table: "Meetings",
                column: "VisitedCompanyUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meeting_VisitedClientBusinessUnit",
                table: "Meetings",
                column: "VisitedClientBusinessUnitId",
                principalTable: "ClientBusinessUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meeting_VisitedClientEmployee",
                table: "Meetings",
                column: "VisitedClientEmployeeId",
                principalTable: "ClientEmployees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meeting_VisitedCompanyUser",
                table: "Meetings",
                column: "VisitedCompanyUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meeting_VisitedClientBusinessUnit",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Meeting_VisitedClientEmployee",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Meeting_VisitedCompanyUser",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_VisitedClientBusinessUnitId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_VisitedClientEmployeeId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_VisitedCompanyUserId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CancelRemark",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "VisitedClientBusinessUnitId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "VisitedClientEmployeeId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "VisitedCompanyUserId",
                table: "Meetings");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("775ffa53-9983-460e-bb9a-94cf87074e49"));
        }
    }
}

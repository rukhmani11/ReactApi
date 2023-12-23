using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class MeetingConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Client",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_ClientBusinessUnit",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_ClientEmployee",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Company",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_CompanyUser",
                table: "Meetings");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("09aeab92-ce36-473f-ab29-92c4ceb42c84"));

            migrationBuilder.AddForeignKey(
                name: "FK_Meeting_Client",
                table: "Meetings",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meeting_ClientBusinessUnit",
                table: "Meetings",
                column: "ClientBusinessUnitId",
                principalTable: "ClientBusinessUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meeting_ClientEmployee",
                table: "Meetings",
                column: "ClientEmployeeId",
                principalTable: "ClientEmployees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meeting_Company",
                table: "Meetings",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meeting_CompanyUser",
                table: "Meetings",
                column: "CompanyUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meeting_Client",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Meeting_ClientBusinessUnit",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Meeting_ClientEmployee",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Meeting_Company",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Meeting_CompanyUser",
                table: "Meetings");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("4ca586ef-c5df-4f0f-9873-c84d47f0cf67"));

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Client",
                table: "Meetings",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_ClientBusinessUnit",
                table: "Meetings",
                column: "ClientBusinessUnitId",
                principalTable: "ClientBusinessUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_ClientEmployee",
                table: "Meetings",
                column: "ClientEmployeeId",
                principalTable: "ClientEmployees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Company",
                table: "Meetings",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_CompanyUser",
                table: "Meetings",
                column: "CompanyUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class ClientBUnitUserCascadeDeleteRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientBusinessUnits_Users_RoUserId",
                table: "ClientBusinessUnits");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("b91e90d5-6bda-48b5-bb5b-c042fb670164"));

            migrationBuilder.AddForeignKey(
                name: "FK_ClientBusinessUnit_User",
                table: "ClientBusinessUnits",
                column: "RoUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientBusinessUnit_User",
                table: "ClientBusinessUnits");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("32ff7ca8-015d-4097-9c47-0b238203d503"));

            migrationBuilder.AddForeignKey(
                name: "FK_ClientBusinessUnits_Users_RoUserId",
                table: "ClientBusinessUnits",
                column: "RoUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class CompanyInClientGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "ClientGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("CF1D1C0C-2F49-4941-BDA0-8F32232571B1"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("fef51506-814c-432e-903e-389f4491c47f"));

            migrationBuilder.CreateIndex(
                name: "IX_ClientGroups_CompanyId",
                table: "ClientGroups",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientGroups_Companies_CompanyId",
                table: "ClientGroups",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientGroups_Companies_CompanyId",
                table: "ClientGroups");

            migrationBuilder.DropIndex(
                name: "IX_ClientGroups_CompanyId",
                table: "ClientGroups");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ClientGroups");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("04637dd9-5fdd-47a6-90c4-e234fc464ee0"));
        }
    }
}

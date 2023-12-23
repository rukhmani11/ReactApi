using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class ClientGroupUniqueWithCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClientGroups_GroupName",
                table: "ClientGroups");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("c99046f8-84c1-4c39-ac7e-76059ab19993"));

            migrationBuilder.CreateIndex(
                name: "IX_ClientGroups_GroupName_CompanyId",
                table: "ClientGroups",
                columns: new[] { "GroupName", "CompanyId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClientGroups_GroupName_CompanyId",
                table: "ClientGroups");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("94f0a53d-6bd5-4e5a-b5b1-7ae1dec2e08e"));

            migrationBuilder.CreateIndex(
                name: "IX_ClientGroups_GroupName",
                table: "ClientGroups",
                column: "GroupName",
                unique: true);
        }
    }
}

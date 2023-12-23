using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class EmpCodeNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_CompanyId_EmpCode",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "EmpCode",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("66820e35-041d-4485-acfc-f06b0e52683d"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId_EmpCode",
                table: "Users",
                columns: new[] { "CompanyId", "EmpCode" },
                unique: true,
                filter: "[CompanyId] IS NOT NULL AND [EmpCode] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_CompanyId_EmpCode",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "EmpCode",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("eb041919-5dd5-47bb-afdb-955df07b68a1"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId_EmpCode",
                table: "Users",
                columns: new[] { "CompanyId", "EmpCode" },
                unique: true,
                filter: "[CompanyId] IS NOT NULL");
        }
    }
}

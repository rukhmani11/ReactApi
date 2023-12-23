using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class CurrencyCodeInClientAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currency_CurrencyCode",
                table: "ClientFinancials");

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "ClientAccounts",
                type: "nchar(3)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("94f0a53d-6bd5-4e5a-b5b1-7ae1dec2e08e"));

            migrationBuilder.CreateIndex(
                name: "IX_ClientAccounts_CurrencyCode",
                table: "ClientAccounts",
                column: "CurrencyCode");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientAccounts_Currency",
                table: "ClientAccounts",
                column: "CurrencyCode",
                principalTable: "Currencies",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientFinancials_Currency",
                table: "ClientFinancials",
                column: "CurrencyCode",
                principalTable: "Currencies",
                principalColumn: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientAccounts_Currency",
                table: "ClientAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientFinancials_Currency",
                table: "ClientFinancials");

            migrationBuilder.DropIndex(
                name: "IX_ClientAccounts_CurrencyCode",
                table: "ClientAccounts");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "ClientAccounts");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("fef51506-814c-432e-903e-389f4491c47f"));

            migrationBuilder.AddForeignKey(
                name: "FK_Currency_CurrencyCode",
                table: "ClientFinancials",
                column: "CurrencyCode",
                principalTable: "Currencies",
                principalColumn: "Code");
        }
    }
}

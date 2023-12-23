using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class MeetingStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "MeetingStatusId",
                table: "Meetings",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AlterColumn<string>(
                name: "MeetingStatus",
                table: "Meetings",
                type: "varchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: false,
                computedColumnSql: "(case [MeetingStatusId] when (0) then 'Pending' when (1) then 'Expired' when (2) then 'Closed' when (3) then 'Rejected' when (4) then 'OnHold' end)",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("c88017d1-9533-48bd-b5df-535c5808bfdb"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeetingStatusId",
                table: "Meetings");

            migrationBuilder.AlterColumn<string>(
                name: "MeetingStatus",
                table: "Meetings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldUnicode: false,
                oldMaxLength: 15,
                oldComputedColumnSql: "(case [MeetingStatusId] when (0) then 'Pending' when (1) then 'Expired' when (2) then 'Closed' when (3) then 'Rejected' when (4) then 'OnHold' end)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("8a2d14ca-935a-4776-9e20-47e839890c86"));
        }
    }
}

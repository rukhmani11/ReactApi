using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class meetingStatusAsPrivate12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MeetingStatus",
                table: "Meetings",
                type: "nvarchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: false,
                computedColumnSql: "(case [MeetingStatusId] when (0) then 'Pending' when (1) then 'Expired' when (2) then 'Closed' when (3) then 'Rejected' when (4) then 'OnHold' end)",
                stored: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldUnicode: false,
                oldMaxLength: 15,
                oldComputedColumnSql: "(case [MeetingStatusId] when (0) then 'Pending' when (1) then 'Submitted' when (2) then 'Approved' when (3) then 'Rejected' when (4) then 'OnHold' when (5) then 'Approved By Office' when (6) then 'Rejected By Office'  end)",
                oldStored: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("5a776123-b65e-4f67-9bfe-7a177922100c"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MeetingStatus",
                table: "Meetings",
                type: "nvarchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: false,
                computedColumnSql: "(case [MeetingStatusId] when (0) then 'Pending' when (1) then 'Submitted' when (2) then 'Approved' when (3) then 'Rejected' when (4) then 'OnHold' when (5) then 'Approved By Office' when (6) then 'Rejected By Office'  end)",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldUnicode: false,
                oldMaxLength: 15,
                oldComputedColumnSql: "(case [MeetingStatusId] when (0) then 'Pending' when (1) then 'Expired' when (2) then 'Closed' when (3) then 'Rejected' when (4) then 'OnHold' end)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("4ebf832c-c7e2-4d61-943d-e77f871bc77e"));
        }
    }
}

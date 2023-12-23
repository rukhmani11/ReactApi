﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class MeetingStatus13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MeetingStatus",
                table: "Meetings",
                type: "nvarchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: false,
                computedColumnSql: "(case [MeetingStatusId] when (0) then 'Pending' when (1) then 'Expired' when (2) then 'Closed' when (3) then 'Rejected' when (4) then 'OnHold' end)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("b73b8f8a-97d7-44c6-8f1f-a5a0ea47cabf"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeetingStatus",
                table: "Meetings");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("85293170-7abb-4897-a06d-2c3916712d5c"));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class ActionDetailsNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetingRisks",
                table: "MeetingRisks");

            migrationBuilder.RenameTable(
                name: "MeetingRisks",
                newName: "MeetingOpportunity");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingRisks_MeetingId",
                table: "MeetingOpportunity",
                newName: "IX_MeetingOpportunity_MeetingId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingRisks_CompanyRiskId",
                table: "MeetingOpportunity",
                newName: "IX_MeetingOpportunity_CompanyRiskId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingRisks_AssignedToUserId",
                table: "MeetingOpportunity",
                newName: "IX_MeetingOpportunity_AssignedToUserId");

            migrationBuilder.AlterColumn<string>(
                name: "ActionDetails",
                table: "MeetingOpportunities",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "ActionDetails",
                table: "MeetingObservationAndOtherMatters",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "ActionDetails",
                table: "MeetingOpportunity",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetingOpportunity",
                table: "MeetingOpportunity",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("a2f04bb8-3b6b-4d29-8f9f-ac3844b04d10"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetingOpportunity",
                table: "MeetingOpportunity");

            migrationBuilder.RenameTable(
                name: "MeetingOpportunity",
                newName: "MeetingRisks");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingOpportunity_MeetingId",
                table: "MeetingRisks",
                newName: "IX_MeetingRisks_MeetingId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingOpportunity_CompanyRiskId",
                table: "MeetingRisks",
                newName: "IX_MeetingRisks_CompanyRiskId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingOpportunity_AssignedToUserId",
                table: "MeetingRisks",
                newName: "IX_MeetingRisks_AssignedToUserId");

            migrationBuilder.AlterColumn<string>(
                name: "ActionDetails",
                table: "MeetingOpportunities",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActionDetails",
                table: "MeetingObservationAndOtherMatters",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActionDetails",
                table: "MeetingRisks",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetingRisks",
                table: "MeetingRisks",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("b73b8f8a-97d7-44c6-8f1f-a5a0ea47cabf"));
        }
    }
}

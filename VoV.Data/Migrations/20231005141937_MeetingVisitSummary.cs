using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class MeetingVisitSummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "VisitSummary",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetingRisks",
                table: "MeetingRisks",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("ade746d9-f423-4ed8-b792-31ee7bd592f9"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "VisitSummary",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}

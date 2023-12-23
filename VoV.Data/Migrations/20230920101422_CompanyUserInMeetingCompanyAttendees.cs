using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class CompanyUserInMeetingCompanyAttendees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingCompanyAttendee_ClientEmployee",
                table: "MeetingCompanyAttendees");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingObservation_CompanyObservation",
                table: "MeetingObservationAndOtherMatters");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingObservation_Meeting",
                table: "MeetingObservationAndOtherMatters");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingObservation_User",
                table: "MeetingObservationAndOtherMatters");

            migrationBuilder.RenameColumn(
                name: "ClientEmployeeId",
                table: "MeetingCompanyAttendees",
                newName: "CompanyUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingCompanyAttendees_ClientEmployeeId",
                table: "MeetingCompanyAttendees",
                newName: "IX_MeetingCompanyAttendees_CompanyUserId");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("d69bb620-e799-4247-868f-82ed70be4c5e"));

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingCompanyAttendee_User",
                table: "MeetingCompanyAttendees",
                column: "CompanyUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingObservationAndOtherMatter_CompanyObservation",
                table: "MeetingObservationAndOtherMatters",
                column: "CompanyObservationId",
                principalTable: "CompanyObservations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingObservationAndOtherMatter_Meeting",
                table: "MeetingObservationAndOtherMatters",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingObservationAndOtherMatter_User",
                table: "MeetingObservationAndOtherMatters",
                column: "AssignedToUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingCompanyAttendee_User",
                table: "MeetingCompanyAttendees");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingObservationAndOtherMatter_CompanyObservation",
                table: "MeetingObservationAndOtherMatters");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingObservationAndOtherMatter_Meeting",
                table: "MeetingObservationAndOtherMatters");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingObservationAndOtherMatter_User",
                table: "MeetingObservationAndOtherMatters");

            migrationBuilder.RenameColumn(
                name: "CompanyUserId",
                table: "MeetingCompanyAttendees",
                newName: "ClientEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingCompanyAttendees_CompanyUserId",
                table: "MeetingCompanyAttendees",
                newName: "IX_MeetingCompanyAttendees_ClientEmployeeId");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("74b85067-61b9-4516-8719-e15903921e71"));

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingCompanyAttendee_ClientEmployee",
                table: "MeetingCompanyAttendees",
                column: "ClientEmployeeId",
                principalTable: "ClientEmployees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingObservation_CompanyObservation",
                table: "MeetingObservationAndOtherMatters",
                column: "CompanyObservationId",
                principalTable: "CompanyObservations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingObservation_Meeting",
                table: "MeetingObservationAndOtherMatters",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingObservation_User",
                table: "MeetingObservationAndOtherMatters",
                column: "AssignedToUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

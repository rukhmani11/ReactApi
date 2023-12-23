using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class MeetingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduledOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SrNo = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MeetingNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClientEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientBusinessUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeetingPurpose = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Agenda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeetingStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VisitedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VisitSummary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Client",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_ClientBusinessUnit",
                        column: x => x.ClientBusinessUnitId,
                        principalTable: "ClientBusinessUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_ClientEmployee",
                        column: x => x.ClientEmployeeId,
                        principalTable: "ClientEmployees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_Company",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_CompanyUser",
                        column: x => x.CompanyUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("4ca586ef-c5df-4f0f-9873-c84d47f0cf67"));

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_ClientBusinessUnitId",
                table: "Meetings",
                column: "ClientBusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_ClientEmployeeId",
                table: "Meetings",
                column: "ClientEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_ClientId",
                table: "Meetings",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_CompanyId",
                table: "Meetings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_CompanyUserId",
                table: "Meetings",
                column: "CompanyUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ClientBusinessUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Agenda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    MeetingNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MeetingPurpose = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MeetingStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ScheduledOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SrNo = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    VisitSummary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    VisitedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Client",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_ClientBusinessUnit",
                        column: x => x.ClientBusinessUnitId,
                        principalTable: "ClientBusinessUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_ClientEmployee",
                        column: x => x.ClientEmployeeId,
                        principalTable: "ClientEmployees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_Company",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_CompanyUser",
                        column: x => x.CompanyUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("f4e57dce-64ee-457a-9cff-6dfd0ded0b17"));

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ClientBusinessUnitId",
                table: "Messages",
                column: "ClientBusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ClientEmployeeId",
                table: "Messages",
                column: "ClientEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ClientId",
                table: "Messages",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_CompanyId",
                table: "Messages",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_CompanyUserId",
                table: "Messages",
                column: "CompanyUserId");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class MeetingAndEmpCodeOfUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Message",
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
                    table.PrimaryKey("PK_Message", x => x.Id);
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
                value: new Guid("eb041919-5dd5-47bb-afdb-955df07b68a1"));

            migrationBuilder.CreateIndex(
                name: "IX_Message_ClientBusinessUnitId",
                table: "Message",
                column: "ClientBusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ClientEmployeeId",
                table: "Message",
                column: "ClientEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ClientId",
                table: "Message",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_CompanyId",
                table: "Message",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_CompanyUserId",
                table: "Message",
                column: "CompanyUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("b91e90d5-6bda-48b5-bb5b-c042fb670164"));
        }
    }
}

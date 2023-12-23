using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class MeetingRelated6Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleMeetingObservationDetails");

            migrationBuilder.DropTable(
                name: "ScheduleMeetingOpportunityDetails");

            migrationBuilder.DropTable(
                name: "ScheduleMeetingRiskDetails");

            migrationBuilder.DropTable(
                name: "ScheduleMeetings");

            migrationBuilder.CreateTable(
                name: "MeetingClientAttendees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingClientAttendees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingClientAttendee_ClientEmployee",
                        column: x => x.ClientEmployeeId,
                        principalTable: "ClientEmployees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingClientAttendee_Meeting",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MeetingCompanyAttendees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingCompanyAttendees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingCompanyAttendee_ClientEmployee",
                        column: x => x.ClientEmployeeId,
                        principalTable: "ClientEmployees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingCompanyAttendee_Meeting",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MeetingObservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyObservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCritical = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ActionRequired = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AssignedToUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Responsibility = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DeadLine = table.Column<DateTime>(type: "date", nullable: true),
                    ObservationStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    DateOfClosing = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingObservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingObservation_CompanyObservation",
                        column: x => x.CompanyObservationId,
                        principalTable: "CompanyObservations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingObservation_Meeting",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingObservation_User",
                        column: x => x.AssignedToUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MeetingOpportunities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyOpportunityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCritical = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ActionRequired = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AssignedToUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Responsibility = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DeadLine = table.Column<DateTime>(type: "date", nullable: true),
                    OpportunityStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    DateOfClosing = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingOpportunities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingOpportunity_CompanyOpportunity",
                        column: x => x.CompanyOpportunityId,
                        principalTable: "CompanyOpportunities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingOpportunity_Meeting",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingOpportunity_User",
                        column: x => x.AssignedToUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MeetingOtherMatters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCritical = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionRequired = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AssignedToUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Responsibility = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DeadLine = table.Column<DateTime>(type: "date", nullable: true),
                    OtherMatterStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    DateOfClosing = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingOtherMatters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingOtherMatter_Meeting",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingOtherMatter_User",
                        column: x => x.AssignedToUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MeetingOthers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCritical = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ActionRequired = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AssignedToUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Responsibility = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DeadLine = table.Column<DateTime>(type: "date", nullable: true),
                    OtherStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    DateOfClosing = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingOthers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingOther_Meeting",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingOther_User",
                        column: x => x.AssignedToUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MeetingRisks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyRiskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCritical = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionRequired = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AssignedToUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Responsibility = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DeadLine = table.Column<DateTime>(type: "date", nullable: true),
                    RiskStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    DateOfClosing = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingRisks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingRisk_CompanyRisk",
                        column: x => x.CompanyRiskId,
                        principalTable: "CompanyRisks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingRisk_Meeting",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingRisk_User",
                        column: x => x.AssignedToUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("15c07de0-0499-404e-828f-5197def3f190"));

            migrationBuilder.CreateIndex(
                name: "IX_MeetingClientAttendees_ClientEmployeeId",
                table: "MeetingClientAttendees",
                column: "ClientEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingClientAttendees_MeetingId",
                table: "MeetingClientAttendees",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingCompanyAttendees_ClientEmployeeId",
                table: "MeetingCompanyAttendees",
                column: "ClientEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingCompanyAttendees_MeetingId",
                table: "MeetingCompanyAttendees",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingObservations_AssignedToUserId",
                table: "MeetingObservations",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingObservations_CompanyObservationId",
                table: "MeetingObservations",
                column: "CompanyObservationId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingObservations_MeetingId",
                table: "MeetingObservations",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingOpportunities_AssignedToUserId",
                table: "MeetingOpportunities",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingOpportunities_CompanyOpportunityId",
                table: "MeetingOpportunities",
                column: "CompanyOpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingOpportunities_MeetingId",
                table: "MeetingOpportunities",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingOtherMatters_AssignedToUserId",
                table: "MeetingOtherMatters",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingOtherMatters_MeetingId",
                table: "MeetingOtherMatters",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingOthers_AssignedToUserId",
                table: "MeetingOthers",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingOthers_MeetingId",
                table: "MeetingOthers",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRisks_AssignedToUserId",
                table: "MeetingRisks",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRisks_CompanyRiskId",
                table: "MeetingRisks",
                column: "CompanyRiskId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRisks_MeetingId",
                table: "MeetingRisks",
                column: "MeetingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetingClientAttendees");

            migrationBuilder.DropTable(
                name: "MeetingCompanyAttendees");

            migrationBuilder.DropTable(
                name: "MeetingObservations");

            migrationBuilder.DropTable(
                name: "MeetingOpportunities");

            migrationBuilder.DropTable(
                name: "MeetingOtherMatters");

            migrationBuilder.DropTable(
                name: "MeetingOthers");

            migrationBuilder.DropTable(
                name: "MeetingRisks");

            migrationBuilder.CreateTable(
                name: "ScheduleMeetingObservationDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleMeetingObservationDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleMeetingOpportunityDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleMeetingOpportunityDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleMeetingRiskDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleMeetingRiskDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleMeetings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleMeetings", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("09aeab92-ce36-473f-ab29-92c4ceb42c84"));
        }
    }
}

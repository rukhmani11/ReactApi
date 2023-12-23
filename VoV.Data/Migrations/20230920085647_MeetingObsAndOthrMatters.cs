using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class MeetingObsAndOthrMatters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetingObservations");

            migrationBuilder.DropTable(
                name: "MeetingOtherMatters");

            migrationBuilder.CreateTable(
                name: "MeetingObservationAndOtherMatters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyObservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCritical = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ActionRequired = table.Column<bool>(type: "bit", nullable: false),
                    ActionDetails = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                    table.PrimaryKey("PK_MeetingObservationAndOtherMatters", x => x.Id);
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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("74b85067-61b9-4516-8719-e15903921e71"));

            migrationBuilder.CreateIndex(
                name: "IX_MeetingObservationAndOtherMatters_AssignedToUserId",
                table: "MeetingObservationAndOtherMatters",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingObservationAndOtherMatters_CompanyObservationId",
                table: "MeetingObservationAndOtherMatters",
                column: "CompanyObservationId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingObservationAndOtherMatters_MeetingId",
                table: "MeetingObservationAndOtherMatters",
                column: "MeetingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetingObservationAndOtherMatters");

            migrationBuilder.CreateTable(
                name: "MeetingObservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    AssignedToUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyObservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionDetails = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ActionRequired = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    DateOfClosing = table.Column<DateTime>(type: "date", nullable: true),
                    DeadLine = table.Column<DateTime>(type: "date", nullable: true),
                    IsCritical = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ObservationStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Responsibility = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                name: "MeetingOtherMatters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    AssignedToUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionDetails = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ActionRequired = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    DateOfClosing = table.Column<DateTime>(type: "date", nullable: true),
                    DeadLine = table.Column<DateTime>(type: "date", nullable: true),
                    IsCritical = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    OtherMatterStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Responsibility = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("4c250db0-92cc-4648-ad51-e26317100e3f"));

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
                name: "IX_MeetingOtherMatters_AssignedToUserId",
                table: "MeetingOtherMatters",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingOtherMatters_MeetingId",
                table: "MeetingOtherMatters",
                column: "MeetingId");
        }
    }
}

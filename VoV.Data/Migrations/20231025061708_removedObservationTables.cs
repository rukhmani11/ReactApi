using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class removedObservationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingObservationAndOtherMatter_CompanyObservation",
                table: "MeetingObservationAndOtherMatters");

            migrationBuilder.DropTable(
                name: "CompanyObservations");

            migrationBuilder.DropTable(
                name: "StandardObservations");

            migrationBuilder.DropIndex(
                name: "IX_MeetingObservationAndOtherMatters_CompanyObservationId",
                table: "MeetingObservationAndOtherMatters");

            migrationBuilder.DropColumn(
                name: "CompanyObservationId",
                table: "MeetingObservationAndOtherMatters");

            migrationBuilder.AddColumn<string>(
                name: "CompanyObservation",
                table: "MeetingObservationAndOtherMatters",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("775ffa53-9983-460e-bb9a-94cf87074e49"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyObservation",
                table: "MeetingObservationAndOtherMatters");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyObservationId",
                table: "MeetingObservationAndOtherMatters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyObservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    BusinessSegmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyObservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyObservations_BusinessSegment",
                        column: x => x.BusinessSegmentId,
                        principalTable: "BusinessSegments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyObservations_Company",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StandardObservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    BusinessSegmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardObservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandardObservations_BusinessSegments_BusinessSegmentId",
                        column: x => x.BusinessSegmentId,
                        principalTable: "BusinessSegments",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("5a776123-b65e-4f67-9bfe-7a177922100c"));

            migrationBuilder.CreateIndex(
                name: "IX_MeetingObservationAndOtherMatters_CompanyObservationId",
                table: "MeetingObservationAndOtherMatters",
                column: "CompanyObservationId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyObservations_BusinessSegmentId",
                table: "CompanyObservations",
                column: "BusinessSegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyObservations_CompanyId_Name",
                table: "CompanyObservations",
                columns: new[] { "CompanyId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StandardObservations_BusinessSegmentId_Name",
                table: "StandardObservations",
                columns: new[] { "BusinessSegmentId", "Name" },
                unique: true,
                filter: "[BusinessSegmentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingObservationAndOtherMatter_CompanyObservation",
                table: "MeetingObservationAndOtherMatters",
                column: "CompanyObservationId",
                principalTable: "CompanyObservations",
                principalColumn: "Id");
        }
    }
}

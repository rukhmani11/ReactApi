using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class DBCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Audit_DDL_Changes",
                columns: table => new
                {
                    ObjectId = table.Column<int>(type: "int", nullable: false),
                    ObjectSchema = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObjectSQL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Object_Host_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObjectType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mod_Dt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit_DDL_Changes", x => x.ObjectId);
                });

            migrationBuilder.CreateTable(
                name: "BusinessSegments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessSegments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    GroupName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GroupCIFNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ADLoginYn = table.Column<bool>(type: "bit", nullable: false),
                    MobileIronYn = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ThemeLightHexCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThemeDarkHexCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nchar(3)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "FinancialYears",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    FromDate = table.Column<DateTime>(type: "date", nullable: false),
                    ToDate = table.Column<DateTime>(type: "date", nullable: false),
                    Abbr = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "StandardObservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    BusinessSegmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
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

            migrationBuilder.CreateTable(
                name: "StandardOpportunities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    BusinessSegmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardOpportunities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandardOpportunities_BusinessSegments_BusinessSegmentId",
                        column: x => x.BusinessSegmentId,
                        principalTable: "BusinessSegments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StandardRisks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    BusinessSegmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardRisks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandardRisks_BusinessSegments_BusinessSegmentId",
                        column: x => x.BusinessSegmentId,
                        principalTable: "BusinessSegments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSettings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessUnits_BusinessUnits",
                        column: x => x.ParentId,
                        principalTable: "BusinessUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusinessUnits_Company",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CIFNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitingFrequencyInMonth = table.Column<byte>(type: "tinyint", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_ClientGroup",
                        column: x => x.ClientGroupId,
                        principalTable: "ClientGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Clients_Company",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyObservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    BusinessSegmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
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
                name: "CompanyOpportunities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    BusinessSegmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyOpportunities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyOpportunities_BusinessSegment",
                        column: x => x.BusinessSegmentId,
                        principalTable: "BusinessSegments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyOpportunities_Company",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyRisks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    BusinessSegmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyRisks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyRisks_BusinessSegment",
                        column: x => x.BusinessSegmentId,
                        principalTable: "BusinessSegments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyRisks_Company",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Designations_Company",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Designations_Designations",
                        column: x => x.ParentId,
                        principalTable: "Designations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Company",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Location_Location",
                        column: x => x.ParentId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    AccountNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BalanceAsOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientAccounts_AccountType",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientAccounts_Client",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientFinancials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Turnover = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Profit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinancialYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nchar(3)", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientFinancials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientFinancials_Client",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientFinancials_FinancialYear",
                        column: x => x.FinancialYearId,
                        principalTable: "FinancialYears",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Currency_CurrencyCode",
                        column: x => x.CurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    EmpCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesignationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReportingToUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BusinessUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    JwtTokenExpiresOn = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    JwtToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_BusinessUnit",
                        column: x => x.BusinessUnitId,
                        principalTable: "BusinessUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Company",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Designation",
                        column: x => x.DesignationId,
                        principalTable: "Designations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Location",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_ReportingUsers",
                        column: x => x.ReportingToUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Role",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientFinancialFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ClientFinancialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientFinancialFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientFinancialFiles_ClientFinancial",
                        column: x => x.ClientFinancialId,
                        principalTable: "ClientFinancials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientBusinessUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessSegmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    RoUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientBusinessUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientBusinessUnit_BusinessSegment",
                        column: x => x.BusinessSegmentId,
                        principalTable: "BusinessSegments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientBusinessUnit_Client",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientBusinessUnits_Users_RoUserId",
                        column: x => x.RoUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientBusinessUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ClientBusinessUnitId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientEmployees_Client",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientEmployees_ClientBusinessUnit",
                        column: x => x.ClientBusinessUnitId,
                        principalTable: "ClientBusinessUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientEmployees_ClientBusinessUnits_ClientBusinessUnitId1",
                        column: x => x.ClientBusinessUnitId1,
                        principalTable: "ClientBusinessUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "BusinessSegments",
                columns: new[] { "Id", "Active", "CreatedById", "Name", "UpdatedById", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("013ca040-e5e0-4563-b1a9-c3cdedc4241b"), true, new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "Automotive industry", null, null },
                    { new Guid("02ac7728-f244-45f4-9c48-784befb32a5a"), true, new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "Entertainment industry", null, null },
                    { new Guid("04e16ff4-7f76-4a21-9915-68c9cfd3f412"), true, new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "Education industry", null, null },
                    { new Guid("35d20eff-481d-4907-8a26-2de3e479c91f"), true, new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "Chemical industry", null, null },
                    { new Guid("515655a2-4432-438a-b264-fa0399432e3c"), true, new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "Agricultural industry", null, null },
                    { new Guid("583c79a8-ee74-46e4-a946-6ecc9bd715d0"), true, new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "Aerospace industry", null, null },
                    { new Guid("58bcb599-6860-43d4-8a0d-53739780eb69"), true, new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "Defense industry", null, null },
                    { new Guid("7a396f20-50c1-4f46-9383-e05abf23d086"), true, new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "Computer industry", null, null },
                    { new Guid("aa49d145-2f39-4384-9471-1b093fadef00"), true, new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "Fashion industry", null, null },
                    { new Guid("c38dd507-ab7b-4dc6-8aca-5ea584832ce1"), true, new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "Construction industry", null, null },
                    { new Guid("e26cd5b5-ab62-4442-b335-455623379623"), true, new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "Hotels industry", null, null }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "ADLoginYn", "Active", "Address", "CreatedById", "Email", "Logo", "MobileIronYn", "Name", "ThemeDarkHexCode", "ThemeLightHexCode", "UpdatedById", "UpdatedOn", "Website" },
                values: new object[] { new Guid("cf1d1c0c-2f49-4941-bda0-8f32232571b1"), false, false, "Goregaon(West)", new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "sentient2008@gmail.com", null, false, "Sentient", "#0560b2", "#e8f1fa", null, null, null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedById", "Name", "UpdatedById", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("1d0bf51c-3c8f-45b0-9a16-2732b251e88d"), new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "SiteAdmin", null, null },
                    { new Guid("633d5fd6-776f-4af1-8f76-d52adde38d79"), new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "User", null, null },
                    { new Guid("76cde1f7-950e-4826-8666-bea361d27772"), new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "Admin", null, null },
                    { new Guid("947d14f6-bad9-49c6-8830-781fc9807963"), new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "DomainUser", null, null }
                });

            migrationBuilder.InsertData(
                table: "AppSettings",
                columns: new[] { "Id", "CompanyId" },
                values: new object[] { new Guid("345ef14c-f86f-4716-9659-55b1dec2ca1a"), new Guid("cf1d1c0c-2f49-4941-bda0-8f32232571b1") });

            migrationBuilder.InsertData(
                table: "BusinessUnits",
                columns: new[] { "Id", "Code", "CompanyId", "CreatedById", "Name", "ParentId", "UpdatedById", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("13f6e635-5482-4f30-9336-c03e31c768de"), "P", new Guid("cf1d1c0c-2f49-4941-bda0-8f32232571b1"), new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "Purchase", null, null, null },
                    { new Guid("89d82c7b-95ce-4e37-970f-516f30a63522"), "S", new Guid("cf1d1c0c-2f49-4941-bda0-8f32232571b1"), new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "Sales", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Designations",
                columns: new[] { "Id", "Code", "CompanyId", "CreatedById", "Name", "ParentId", "UpdatedById", "UpdatedOn" },
                values: new object[] { new Guid("cf1d1c0c-2f49-4941-bda0-8f32232571b1"), "RO", new Guid("cf1d1c0c-2f49-4941-bda0-8f32232571b1"), new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "Relationship Officer", null, null, null });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Active", "Code", "CompanyId", "CreatedById", "Name", "ParentId", "UpdatedById", "UpdatedOn" },
                values: new object[] { new Guid("3b988bb1-83fb-4fe9-8611-c6101940ffe3"), false, "GRN", new Guid("cf1d1c0c-2f49-4941-bda0-8f32232571b1"), new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "Goregaon", null, null, null });

            migrationBuilder.InsertData(
                table: "Designations",
                columns: new[] { "Id", "Code", "CompanyId", "CreatedById", "Name", "ParentId", "UpdatedById", "UpdatedOn" },
                values: new object[] { new Guid("4870af54-c766-4e0a-a4ef-9f5fbc2201fc"), "TL", new Guid("cf1d1c0c-2f49-4941-bda0-8f32232571b1"), new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "Team Lead", new Guid("cf1d1c0c-2f49-4941-bda0-8f32232571b1"), null, null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "BusinessUnitId", "CompanyId", "CreatedById", "DesignationId", "Email", "EmpCode", "JwtToken", "JwtTokenExpiresOn", "LocationId", "Mobile", "Name", "Password", "ReportingToUserId", "RoleId", "UpdatedById", "UpdatedOn", "UserName" },
                values: new object[] { new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), true, null, new Guid("cf1d1c0c-2f49-4941-bda0-8f32232571b1"), new Guid("04637dd9-5fdd-47a6-90c4-e234fc464ee0"), null, "sentient2008@gmail.com", "VOV_SENTI_001", null, null, new Guid("3b988bb1-83fb-4fe9-8611-c6101940ffe3"), "9969875308", "Sentient Admin", "2220tbYOIVYfrreLt4BsDg==", null, new Guid("76cde1f7-950e-4826-8666-bea361d27772"), null, null, "SentientAdmin" });

            migrationBuilder.InsertData(
                table: "Designations",
                columns: new[] { "Id", "Code", "CompanyId", "CreatedById", "Name", "ParentId", "UpdatedById", "UpdatedOn" },
                values: new object[] { new Guid("290b0d1e-d796-4d36-b0fd-7679634f8c33"), "UH", new Guid("cf1d1c0c-2f49-4941-bda0-8f32232571b1"), new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"), "Unit Head", new Guid("4870af54-c766-4e0a-a4ef-9f5fbc2201fc"), null, null });

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypes_Name",
                table: "AccountTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSettings_CompanyId",
                table: "AppSettings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSegments_Name",
                table: "BusinessSegments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessUnits_CompanyId_Code",
                table: "BusinessUnits",
                columns: new[] { "CompanyId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessUnits_CompanyId_Name",
                table: "BusinessUnits",
                columns: new[] { "CompanyId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessUnits_ParentId",
                table: "BusinessUnits",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAccounts_AccountTypeId",
                table: "ClientAccounts",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAccounts_ClientId_AccountNo_AccountTypeId",
                table: "ClientAccounts",
                columns: new[] { "ClientId", "AccountNo", "AccountTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientBusinessUnits_BusinessSegmentId",
                table: "ClientBusinessUnits",
                column: "BusinessSegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientBusinessUnits_ClientId_Name",
                table: "ClientBusinessUnits",
                columns: new[] { "ClientId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientBusinessUnits_RoUserId",
                table: "ClientBusinessUnits",
                column: "RoUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEmployees_ClientBusinessUnitId",
                table: "ClientEmployees",
                column: "ClientBusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEmployees_ClientBusinessUnitId1",
                table: "ClientEmployees",
                column: "ClientBusinessUnitId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEmployees_ClientId_Email",
                table: "ClientEmployees",
                columns: new[] { "ClientId", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientEmployees_ClientId_Mobile",
                table: "ClientEmployees",
                columns: new[] { "ClientId", "Mobile" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientFinancialFiles_ClientFinancialId_FileName",
                table: "ClientFinancialFiles",
                columns: new[] { "ClientFinancialId", "FileName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientFinancials_ClientId",
                table: "ClientFinancials",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientFinancials_CurrencyCode",
                table: "ClientFinancials",
                column: "CurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_ClientFinancials_FinancialYearId",
                table: "ClientFinancials",
                column: "FinancialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientGroups_GroupName",
                table: "ClientGroups",
                column: "GroupName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientGroupId",
                table: "Clients",
                column: "ClientGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CompanyId_CIFNo",
                table: "Clients",
                columns: new[] { "CompanyId", "CIFNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CompanyId_Name",
                table: "Clients",
                columns: new[] { "CompanyId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Name",
                table: "Companies",
                column: "Name",
                unique: true);

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
                name: "IX_CompanyOpportunities_BusinessSegmentId",
                table: "CompanyOpportunities",
                column: "BusinessSegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyOpportunities_CompanyId_Name",
                table: "CompanyOpportunities",
                columns: new[] { "CompanyId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRisks_BusinessSegmentId",
                table: "CompanyRisks",
                column: "BusinessSegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRisks_CompanyId_Name",
                table: "CompanyRisks",
                columns: new[] { "CompanyId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Code",
                table: "Currencies",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Name",
                table: "Currencies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Designations_CompanyId_Name",
                table: "Designations",
                columns: new[] { "CompanyId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Designations_ParentId",
                table: "Designations",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYears_Abbr",
                table: "FinancialYears",
                column: "Abbr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CompanyId_Code",
                table: "Locations",
                columns: new[] { "CompanyId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CompanyId_Name",
                table: "Locations",
                columns: new[] { "CompanyId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ParentId",
                table: "Locations",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StandardObservations_BusinessSegmentId_Name",
                table: "StandardObservations",
                columns: new[] { "BusinessSegmentId", "Name" },
                unique: true,
                filter: "[BusinessSegmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StandardOpportunities_BusinessSegmentId_Name",
                table: "StandardOpportunities",
                columns: new[] { "BusinessSegmentId", "Name" },
                unique: true,
                filter: "[BusinessSegmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StandardRisks_BusinessSegmentId_Name",
                table: "StandardRisks",
                columns: new[] { "BusinessSegmentId", "Name" },
                unique: true,
                filter: "[BusinessSegmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BusinessUnitId",
                table: "Users",
                column: "BusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId_EmpCode",
                table: "Users",
                columns: new[] { "CompanyId", "EmpCode" },
                unique: true,
                filter: "[CompanyId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId_UserName",
                table: "Users",
                columns: new[] { "CompanyId", "UserName" },
                unique: true,
                filter: "[CompanyId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DesignationId",
                table: "Users",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LocationId",
                table: "Users",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ReportingToUserId",
                table: "Users",
                column: "ReportingToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSettings");

            migrationBuilder.DropTable(
                name: "Audit_DDL_Changes");

            migrationBuilder.DropTable(
                name: "ClientAccounts");

            migrationBuilder.DropTable(
                name: "ClientEmployees");

            migrationBuilder.DropTable(
                name: "ClientFinancialFiles");

            migrationBuilder.DropTable(
                name: "CompanyObservations");

            migrationBuilder.DropTable(
                name: "CompanyOpportunities");

            migrationBuilder.DropTable(
                name: "CompanyRisks");

            migrationBuilder.DropTable(
                name: "ScheduleMeetingObservationDetails");

            migrationBuilder.DropTable(
                name: "ScheduleMeetingOpportunityDetails");

            migrationBuilder.DropTable(
                name: "ScheduleMeetingRiskDetails");

            migrationBuilder.DropTable(
                name: "ScheduleMeetings");

            migrationBuilder.DropTable(
                name: "StandardObservations");

            migrationBuilder.DropTable(
                name: "StandardOpportunities");

            migrationBuilder.DropTable(
                name: "StandardRisks");

            migrationBuilder.DropTable(
                name: "AccountTypes");

            migrationBuilder.DropTable(
                name: "ClientBusinessUnits");

            migrationBuilder.DropTable(
                name: "ClientFinancials");

            migrationBuilder.DropTable(
                name: "BusinessSegments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "FinancialYears");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "BusinessUnits");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "ClientGroups");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}

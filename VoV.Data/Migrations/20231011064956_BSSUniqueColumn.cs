using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class BSSUniqueColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BusinessSubSegments_BusinessSegmentId",
                table: "BusinessSubSegments");

            migrationBuilder.DropIndex(
                name: "IX_BusinessSubSegments_Name",
                table: "BusinessSubSegments");

            migrationBuilder.DropIndex(
                name: "IX_BusinessSegments_Code",
                table: "BusinessSegments");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "BusinessSegments",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "1",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("df9fd48c-a314-47bc-8395-9ab647310bb8"));

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSubSegments_BusinessSegmentId_Name",
                table: "BusinessSubSegments",
                columns: new[] { "BusinessSegmentId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSegments_Code",
                table: "BusinessSegments",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BusinessSubSegments_BusinessSegmentId_Name",
                table: "BusinessSubSegments");

            migrationBuilder.DropIndex(
                name: "IX_BusinessSegments_Code",
                table: "BusinessSegments");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "BusinessSegments",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldDefaultValue: "1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("9228bce0-d750-4b7d-899a-216a06165cd8"));

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSubSegments_BusinessSegmentId",
                table: "BusinessSubSegments",
                column: "BusinessSegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSubSegments_Name",
                table: "BusinessSubSegments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSegments_Code",
                table: "BusinessSegments",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoV.Data.Migrations
{
    public partial class tableRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "Messages");

            migrationBuilder.RenameIndex(
                name: "IX_Message_CompanyUserId",
                table: "Messages",
                newName: "IX_Messages_CompanyUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_CompanyId",
                table: "Messages",
                newName: "IX_Messages_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ClientId",
                table: "Messages",
                newName: "IX_Messages_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ClientEmployeeId",
                table: "Messages",
                newName: "IX_Messages_ClientEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ClientBusinessUnitId",
                table: "Messages",
                newName: "IX_Messages_ClientBusinessUnitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("f4e57dce-64ee-457a-9cff-6dfd0ded0b17"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Message");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_CompanyUserId",
                table: "Message",
                newName: "IX_Message_CompanyUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_CompanyId",
                table: "Message",
                newName: "IX_Message_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ClientId",
                table: "Message",
                newName: "IX_Message_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ClientEmployeeId",
                table: "Message",
                newName: "IX_Message_ClientEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ClientBusinessUnitId",
                table: "Message",
                newName: "IX_Message_ClientBusinessUnitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9bad75f9-442e-447a-8385-4f7fe824fa90"),
                column: "CreatedById",
                value: new Guid("66820e35-041d-4485-acfc-f06b0e52683d"));
        }
    }
}

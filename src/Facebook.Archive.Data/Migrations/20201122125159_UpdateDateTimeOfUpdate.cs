using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Facebook.Archive.Data.Migrations
{
    public partial class UpdateDateTimeOfUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostUpdates_Update_UpdateId",
                schema: "facebook",
                table: "PostUpdates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Update",
                schema: "facebook",
                table: "Update");

            migrationBuilder.RenameTable(
                name: "Update",
                schema: "facebook",
                newName: "Updates",
                newSchema: "facebook");

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                schema: "facebook",
                table: "PostUpdates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Updates",
                schema: "facebook",
                table: "Updates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostUpdates_Updates_UpdateId",
                schema: "facebook",
                table: "PostUpdates",
                column: "UpdateId",
                principalSchema: "facebook",
                principalTable: "Updates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostUpdates_Updates_UpdateId",
                schema: "facebook",
                table: "PostUpdates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Updates",
                schema: "facebook",
                table: "Updates");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                schema: "facebook",
                table: "PostUpdates");

            migrationBuilder.RenameTable(
                name: "Updates",
                schema: "facebook",
                newName: "Update",
                newSchema: "facebook");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Update",
                schema: "facebook",
                table: "Update",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostUpdates_Update_UpdateId",
                schema: "facebook",
                table: "PostUpdates",
                column: "UpdateId",
                principalSchema: "facebook",
                principalTable: "Update",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

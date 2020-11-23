using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Facebook.Archive.Data.Migrations
{
    public partial class SetupAttachmentTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostAttachmentTypes_PostAttachments_TypeId",
                schema: "facebook",
                table: "PostAttachmentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PostAttachmentTypes_PostContents_PostContentId",
                schema: "facebook",
                table: "PostAttachmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_PostAttachmentTypes_PostContentId",
                schema: "facebook",
                table: "PostAttachmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_PostAttachmentTypes_TypeId",
                schema: "facebook",
                table: "PostAttachmentTypes");

            migrationBuilder.DropColumn(
                name: "Data",
                schema: "facebook",
                table: "PostAttachmentTypes");

            migrationBuilder.DropColumn(
                name: "PostContentId",
                schema: "facebook",
                table: "PostAttachmentTypes");

            migrationBuilder.DropColumn(
                name: "TypeId",
                schema: "facebook",
                table: "PostAttachmentTypes");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "facebook",
                table: "PostAttachmentTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "facebook",
                table: "PostAttachments",
                newName: "Description");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                schema: "facebook",
                table: "PostAttachments",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "PostContentId",
                schema: "facebook",
                table: "PostAttachments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                schema: "facebook",
                table: "PostAttachments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostAttachments_PostContentId",
                schema: "facebook",
                table: "PostAttachments",
                column: "PostContentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostAttachments_TypeId",
                schema: "facebook",
                table: "PostAttachments",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostAttachments_PostAttachmentTypes_TypeId",
                schema: "facebook",
                table: "PostAttachments",
                column: "TypeId",
                principalSchema: "facebook",
                principalTable: "PostAttachmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostAttachments_PostContents_PostContentId",
                schema: "facebook",
                table: "PostAttachments",
                column: "PostContentId",
                principalSchema: "facebook",
                principalTable: "PostContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql("INSERT INTO [facebook].[PostAttachmentTypes] ([Name]) VALUES ('IMAGE')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostAttachments_PostAttachmentTypes_TypeId",
                schema: "facebook",
                table: "PostAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostAttachments_PostContents_PostContentId",
                schema: "facebook",
                table: "PostAttachments");

            migrationBuilder.DropIndex(
                name: "IX_PostAttachments_PostContentId",
                schema: "facebook",
                table: "PostAttachments");

            migrationBuilder.DropIndex(
                name: "IX_PostAttachments_TypeId",
                schema: "facebook",
                table: "PostAttachments");

            migrationBuilder.DropColumn(
                name: "Data",
                schema: "facebook",
                table: "PostAttachments");

            migrationBuilder.DropColumn(
                name: "PostContentId",
                schema: "facebook",
                table: "PostAttachments");

            migrationBuilder.DropColumn(
                name: "TypeId",
                schema: "facebook",
                table: "PostAttachments");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "facebook",
                table: "PostAttachmentTypes",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "facebook",
                table: "PostAttachments",
                newName: "Name");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                schema: "facebook",
                table: "PostAttachmentTypes",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "PostContentId",
                schema: "facebook",
                table: "PostAttachmentTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                schema: "facebook",
                table: "PostAttachmentTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostAttachmentTypes_PostContentId",
                schema: "facebook",
                table: "PostAttachmentTypes",
                column: "PostContentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostAttachmentTypes_TypeId",
                schema: "facebook",
                table: "PostAttachmentTypes",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostAttachmentTypes_PostAttachments_TypeId",
                schema: "facebook",
                table: "PostAttachmentTypes",
                column: "TypeId",
                principalSchema: "facebook",
                principalTable: "PostAttachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostAttachmentTypes_PostContents_PostContentId",
                schema: "facebook",
                table: "PostAttachmentTypes",
                column: "PostContentId",
                principalSchema: "facebook",
                principalTable: "PostContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

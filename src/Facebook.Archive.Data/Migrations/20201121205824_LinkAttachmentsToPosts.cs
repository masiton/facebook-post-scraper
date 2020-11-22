using Microsoft.EntityFrameworkCore.Migrations;

namespace Facebook.Archive.Data.Migrations
{
    public partial class LinkAttachmentsToPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostContentId",
                schema: "facebook",
                table: "PostAttachmentTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostAttachmentTypes_PostContentId",
                schema: "facebook",
                table: "PostAttachmentTypes",
                column: "PostContentId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostAttachmentTypes_PostContents_PostContentId",
                schema: "facebook",
                table: "PostAttachmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_PostAttachmentTypes_PostContentId",
                schema: "facebook",
                table: "PostAttachmentTypes");

            migrationBuilder.DropColumn(
                name: "PostContentId",
                schema: "facebook",
                table: "PostAttachmentTypes");
        }
    }
}

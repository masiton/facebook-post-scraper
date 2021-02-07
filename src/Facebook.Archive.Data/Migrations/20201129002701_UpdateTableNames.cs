using Microsoft.EntityFrameworkCore.Migrations;

namespace Facebook.Archive.Data.Migrations
{
    public partial class UpdateTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostContentPhotos_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_PostContentUrls_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentUrls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostContentUrls",
                schema: "facebook",
                table: "PostContentUrls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostContentPhotos",
                schema: "facebook",
                table: "PostContentPhotos");

            migrationBuilder.RenameTable(
                name: "PostContentUrls",
                schema: "facebook",
                newName: "PostContentLinks",
                newSchema: "facebook");

            migrationBuilder.RenameTable(
                name: "PostContentPhotos",
                schema: "facebook",
                newName: "PostContentImages",
                newSchema: "facebook");

            migrationBuilder.RenameIndex(
                name: "IX_PostContentUrls_PostContentId",
                schema: "facebook",
                table: "PostContentLinks",
                newName: "IX_PostContentLinks_PostContentId");

            migrationBuilder.RenameIndex(
                name: "IX_PostContentPhotos_PostContentId",
                schema: "facebook",
                table: "PostContentImages",
                newName: "IX_PostContentImages_PostContentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostContentLinks",
                schema: "facebook",
                table: "PostContentLinks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostContentImages",
                schema: "facebook",
                table: "PostContentImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostContentImages_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentImages",
                column: "PostContentId",
                principalSchema: "facebook",
                principalTable: "PostContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostContentLinks_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentLinks",
                column: "PostContentId",
                principalSchema: "facebook",
                principalTable: "PostContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostContentImages_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentImages");

            migrationBuilder.DropForeignKey(
                name: "FK_PostContentLinks_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostContentLinks",
                schema: "facebook",
                table: "PostContentLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostContentImages",
                schema: "facebook",
                table: "PostContentImages");

            migrationBuilder.RenameTable(
                name: "PostContentLinks",
                schema: "facebook",
                newName: "PostContentUrls",
                newSchema: "facebook");

            migrationBuilder.RenameTable(
                name: "PostContentImages",
                schema: "facebook",
                newName: "PostContentPhotos",
                newSchema: "facebook");

            migrationBuilder.RenameIndex(
                name: "IX_PostContentLinks_PostContentId",
                schema: "facebook",
                table: "PostContentUrls",
                newName: "IX_PostContentUrls_PostContentId");

            migrationBuilder.RenameIndex(
                name: "IX_PostContentImages_PostContentId",
                schema: "facebook",
                table: "PostContentPhotos",
                newName: "IX_PostContentPhotos_PostContentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostContentUrls",
                schema: "facebook",
                table: "PostContentUrls",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostContentPhotos",
                schema: "facebook",
                table: "PostContentPhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostContentPhotos_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentPhotos",
                column: "PostContentId",
                principalSchema: "facebook",
                principalTable: "PostContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostContentUrls_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentUrls",
                column: "PostContentId",
                principalSchema: "facebook",
                principalTable: "PostContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Facebook.Archive.Data.Migrations
{
    public partial class UpdateNameAndAddHtmlSources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlHtml",
                schema: "facebook",
                table: "PostContentUrls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrlHtml",
                schema: "facebook",
                table: "PostContentPhotos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlHtml",
                schema: "facebook",
                table: "PostContentUrls");

            migrationBuilder.DropColumn(
                name: "ImageUrlHtml",
                schema: "facebook",
                table: "PostContentPhotos");
        }
    }
}

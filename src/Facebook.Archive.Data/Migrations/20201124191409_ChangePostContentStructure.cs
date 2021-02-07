using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Facebook.Archive.Data.Migrations
{
    public partial class ChangePostContentStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostAttachments",
                schema: "facebook");

            migrationBuilder.DropTable(
                name: "PostAttachmentTypes",
                schema: "facebook");

            migrationBuilder.DropColumn(
                name: "RawHtml",
                schema: "facebook",
                table: "PostContents");

            migrationBuilder.RenameColumn(
                name: "Text",
                schema: "facebook",
                table: "PostContents",
                newName: "ParserName");

            migrationBuilder.AddColumn<int>(
                name: "ParserVersion",
                schema: "facebook",
                table: "PostContents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PostContentPhotos",
                schema: "facebook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostContentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostContentPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostContentPhotos_PostContents_PostContentId",
                        column: x => x.PostContentId,
                        principalSchema: "facebook",
                        principalTable: "PostContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostContentTexts",
                schema: "facebook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Html = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostContentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostContentTexts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostContentTexts_PostContents_PostContentId",
                        column: x => x.PostContentId,
                        principalSchema: "facebook",
                        principalTable: "PostContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostContentUrls",
                schema: "facebook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostContentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostContentUrls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostContentUrls_PostContents_PostContentId",
                        column: x => x.PostContentId,
                        principalSchema: "facebook",
                        principalTable: "PostContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostContentPhotos_PostContentId",
                schema: "facebook",
                table: "PostContentPhotos",
                column: "PostContentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostContentTexts_PostContentId",
                schema: "facebook",
                table: "PostContentTexts",
                column: "PostContentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostContentUrls_PostContentId",
                schema: "facebook",
                table: "PostContentUrls",
                column: "PostContentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostContentPhotos",
                schema: "facebook");

            migrationBuilder.DropTable(
                name: "PostContentTexts",
                schema: "facebook");

            migrationBuilder.DropTable(
                name: "PostContentUrls",
                schema: "facebook");

            migrationBuilder.DropColumn(
                name: "ParserVersion",
                schema: "facebook",
                table: "PostContents");

            migrationBuilder.RenameColumn(
                name: "ParserName",
                schema: "facebook",
                table: "PostContents",
                newName: "Text");

            migrationBuilder.AddColumn<string>(
                name: "RawHtml",
                schema: "facebook",
                table: "PostContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PostAttachmentTypes",
                schema: "facebook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostAttachmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostAttachments",
                schema: "facebook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostContentId = table.Column<int>(type: "int", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostAttachments_PostAttachmentTypes_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "facebook",
                        principalTable: "PostAttachmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostAttachments_PostContents_PostContentId",
                        column: x => x.PostContentId,
                        principalSchema: "facebook",
                        principalTable: "PostContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
        }
    }
}

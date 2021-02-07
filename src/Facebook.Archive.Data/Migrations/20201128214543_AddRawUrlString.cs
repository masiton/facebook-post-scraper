using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Facebook.Archive.Data.Migrations
{
    public partial class AddRawUrlString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                schema: "facebook",
                table: "PostContentUrls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                schema: "facebook",
                table: "PostContentUrls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ParserName",
                schema: "facebook",
                table: "PostContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Html",
                schema: "facebook",
                table: "PostContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PostContentTimestamps",
                schema: "facebook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimestampUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimestampRaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostContentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostContentTimestamps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostContentTimestamps_PostContents_PostContentId",
                        column: x => x.PostContentId,
                        principalSchema: "facebook",
                        principalTable: "PostContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostContentTimestamps_PostContentId",
                schema: "facebook",
                table: "PostContentTimestamps",
                column: "PostContentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostContentTimestamps",
                schema: "facebook");

            migrationBuilder.DropColumn(
                name: "Text",
                schema: "facebook",
                table: "PostContentUrls");

            migrationBuilder.DropColumn(
                name: "Url",
                schema: "facebook",
                table: "PostContentUrls");

            migrationBuilder.DropColumn(
                name: "Html",
                schema: "facebook",
                table: "PostContents");

            migrationBuilder.AlterColumn<string>(
                name: "ParserName",
                schema: "facebook",
                table: "PostContents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

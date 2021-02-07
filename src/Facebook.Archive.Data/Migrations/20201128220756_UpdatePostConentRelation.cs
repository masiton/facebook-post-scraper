using Microsoft.EntityFrameworkCore.Migrations;

namespace Facebook.Archive.Data.Migrations
{
    public partial class UpdatePostConentRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostContentPhotos_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_PostContentTexts_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentTexts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostContentTimestamps_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentTimestamps");

            migrationBuilder.DropForeignKey(
                name: "FK_PostContentUrls_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentUrls");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                schema: "facebook",
                table: "PostContentUrls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostContentId",
                schema: "facebook",
                table: "PostContentUrls",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TimestampRaw",
                schema: "facebook",
                table: "PostContentTimestamps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostContentId",
                schema: "facebook",
                table: "PostContentTimestamps",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                schema: "facebook",
                table: "PostContentTexts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostContentId",
                schema: "facebook",
                table: "PostContentTexts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Html",
                schema: "facebook",
                table: "PostContentTexts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostContentId",
                schema: "facebook",
                table: "PostContentPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                name: "FK_PostContentTexts_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentTexts",
                column: "PostContentId",
                principalSchema: "facebook",
                principalTable: "PostContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostContentTimestamps_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentTimestamps",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostContentPhotos_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_PostContentTexts_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentTexts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostContentTimestamps_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentTimestamps");

            migrationBuilder.DropForeignKey(
                name: "FK_PostContentUrls_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentUrls");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                schema: "facebook",
                table: "PostContentUrls",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "PostContentId",
                schema: "facebook",
                table: "PostContentUrls",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TimestampRaw",
                schema: "facebook",
                table: "PostContentTimestamps",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "PostContentId",
                schema: "facebook",
                table: "PostContentTimestamps",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                schema: "facebook",
                table: "PostContentTexts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "PostContentId",
                schema: "facebook",
                table: "PostContentTexts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Html",
                schema: "facebook",
                table: "PostContentTexts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "PostContentId",
                schema: "facebook",
                table: "PostContentPhotos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PostContentPhotos_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentPhotos",
                column: "PostContentId",
                principalSchema: "facebook",
                principalTable: "PostContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostContentTexts_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentTexts",
                column: "PostContentId",
                principalSchema: "facebook",
                principalTable: "PostContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostContentTimestamps_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentTimestamps",
                column: "PostContentId",
                principalSchema: "facebook",
                principalTable: "PostContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostContentUrls_PostContents_PostContentId",
                schema: "facebook",
                table: "PostContentUrls",
                column: "PostContentId",
                principalSchema: "facebook",
                principalTable: "PostContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

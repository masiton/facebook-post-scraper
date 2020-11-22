using Microsoft.EntityFrameworkCore.Migrations;

namespace Facebook.Archive.Data.Migrations
{
    public partial class AddPostAttachmentTypeToPostAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostAttachmentTypes",
                schema: "facebook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostAttachmentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostAttachmentTypes_PostAttachments_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "facebook",
                        principalTable: "PostAttachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostAttachmentTypes_TypeId",
                schema: "facebook",
                table: "PostAttachmentTypes",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostAttachmentTypes",
                schema: "facebook");
        }
    }
}

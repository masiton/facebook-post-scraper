using Microsoft.EntityFrameworkCore.Migrations;

namespace Facebook.Archive.Data.Migrations
{
    public partial class AddPostUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostUpdates",
                schema: "facebook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostUpdates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostUpdates_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "facebook",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostUpdates_Update_UpdateId",
                        column: x => x.UpdateId,
                        principalSchema: "facebook",
                        principalTable: "Update",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostUpdates_PostId",
                schema: "facebook",
                table: "PostUpdates",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostUpdates_UpdateId",
                schema: "facebook",
                table: "PostUpdates",
                column: "UpdateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostUpdates",
                schema: "facebook");
        }
    }
}

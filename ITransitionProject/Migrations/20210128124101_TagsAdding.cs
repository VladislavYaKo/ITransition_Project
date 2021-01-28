using Microsoft.EntityFrameworkCore.Migrations;

namespace ITransitionProject.Migrations
{
    public partial class TagsAdding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    ItemCollectionUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    TagValue = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => new { x.ItemCollectionUserId, x.ItemId, x.TagValue });
                    table.ForeignKey(
                        name: "FK_Tags_Items_ItemId_ItemCollectionUserId",
                        columns: x => new { x.ItemId, x.ItemCollectionUserId },
                        principalTable: "Items",
                        principalColumns: new[] { "Id", "CollectionUserId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ItemId_ItemCollectionUserId",
                table: "Tags",
                columns: new[] { "ItemId", "ItemCollectionUserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}

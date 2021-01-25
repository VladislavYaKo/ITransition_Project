using Microsoft.EntityFrameworkCore.Migrations;

namespace ITransitionProject.Migrations
{
    public partial class AddCompositeKeyToCollectionModelStep2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Collections_CollectionForChangeId_CollectionUserId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "ForChangeId",
                table: "Collections");

            migrationBuilder.RenameColumn(
                name: "CollectionForChangeId",
                table: "Items",
                newName: "CollectionId1");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CollectionForChangeId_CollectionUserId",
                table: "Items",
                newName: "IX_Items_CollectionId1_CollectionUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                columns: new[] { "Id", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Collections_CollectionId1_CollectionUserId",
                table: "Items",
                columns: new[] { "CollectionId1", "CollectionUserId" },
                principalTable: "Collections",
                principalColumns: new[] { "Id", "UserId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Collections_CollectionId1_CollectionUserId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.RenameColumn(
                name: "CollectionId1",
                table: "Items",
                newName: "CollectionForChangeId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CollectionId1_CollectionUserId",
                table: "Items",
                newName: "IX_Items_CollectionForChangeId_CollectionUserId");

            migrationBuilder.AddColumn<int>(
                name: "ForChangeId",
                table: "Collections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                columns: new[] { "ForChangeId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Collections_CollectionForChangeId_CollectionUserId",
                table: "Items",
                columns: new[] { "CollectionForChangeId", "CollectionUserId" },
                principalTable: "Collections",
                principalColumns: new[] { "ForChangeId", "UserId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}

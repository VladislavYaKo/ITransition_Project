using Microsoft.EntityFrameworkCore.Migrations;

namespace ITransitionProject.Migrations
{
    public partial class DbSetAdding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collection_AspNetUsers_UserId",
                table: "Collection");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_AdditionalFields_AddFieldsId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Collection_CollectionId",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collection",
                table: "Collection");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "Items");

            migrationBuilder.RenameTable(
                name: "Collection",
                newName: "Collections");

            migrationBuilder.RenameIndex(
                name: "IX_Item_CollectionId",
                table: "Items",
                newName: "IX_Items_CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Item_AddFieldsId",
                table: "Items",
                newName: "IX_Items_AddFieldsId");

            migrationBuilder.RenameIndex(
                name: "IX_Collection_UserId",
                table: "Collections",
                newName: "IX_Collections_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "CollectionId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_AspNetUsers_UserId",
                table: "Collections",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AdditionalFields_AddFieldsId",
                table: "Items",
                column: "AddFieldsId",
                principalTable: "AdditionalFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Collections_CollectionId",
                table: "Items",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_AspNetUsers_UserId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AdditionalFields_AddFieldsId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Collections_CollectionId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Item");

            migrationBuilder.RenameTable(
                name: "Collections",
                newName: "Collection");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CollectionId",
                table: "Item",
                newName: "IX_Item_CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_AddFieldsId",
                table: "Item",
                newName: "IX_Item_AddFieldsId");

            migrationBuilder.RenameIndex(
                name: "IX_Collections_UserId",
                table: "Collection",
                newName: "IX_Collection_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "CollectionId",
                table: "Item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collection",
                table: "Collection",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Collection_AspNetUsers_UserId",
                table: "Collection",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_AdditionalFields_AddFieldsId",
                table: "Item",
                column: "AddFieldsId",
                principalTable: "AdditionalFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Collection_CollectionId",
                table: "Item",
                column: "CollectionId",
                principalTable: "Collection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

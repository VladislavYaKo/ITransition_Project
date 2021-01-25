using Microsoft.EntityFrameworkCore.Migrations;

namespace ITransitionProject.Migrations
{
    public partial class ItemModelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Collections_CollectionId1_CollectionUserId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_CollectionId1_CollectionUserId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CollectionId1",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "CollectionUserId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "CollectionId",
                table: "Items",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CollectionId_CollectionUserId",
                table: "Items",
                columns: new[] { "CollectionId", "CollectionUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Collections_CollectionId_CollectionUserId",
                table: "Items",
                columns: new[] { "CollectionId", "CollectionUserId" },
                principalTable: "Collections",
                principalColumns: new[] { "Id", "UserId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Collections_CollectionId_CollectionUserId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_CollectionId_CollectionUserId",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "CollectionUserId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CollectionId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollectionId1",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_CollectionId1_CollectionUserId",
                table: "Items",
                columns: new[] { "CollectionId1", "CollectionUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Collections_CollectionId1_CollectionUserId",
                table: "Items",
                columns: new[] { "CollectionId1", "CollectionUserId" },
                principalTable: "Collections",
                principalColumns: new[] { "Id", "UserId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}

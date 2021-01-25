using Microsoft.EntityFrameworkCore.Migrations;

namespace ITransitionProject.Migrations
{
    public partial class ColumnsOrderChangeAndManualIdIdentityRemoving : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_Items_Collections_CollectionId_CollectionUserId",
               table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Collections");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Collections",
                type: "int",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                columns: new[] { "Id", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Collections_CollectionId_CollectionUserId",
                table: "Items",
                columns: new[] { "CollectionId", "CollectionUserId" },
                principalTable: "Collections",
                principalColumns: new[] { "Id", "UserId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

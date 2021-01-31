using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITransitionProject.Migrations
{
    public partial class SomeModelsIdTypesChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Manual
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Collections_AspNetUsers_UserId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Collections_CollectionId_CollectionUserId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Items_ItemId_ItemCollectionUserId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ItemId_ItemCollectionUserId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_CollectionId_CollectionUserId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "intId",
                table: "AspNetUsers");

            //Manual
            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Tags");

            migrationBuilder.AddColumn<Guid>(  //Manual. Was AlterColumn
                name: "ItemId",
                table: "Tags",
                type: "uniqueidentifier",
                nullable: false/*,
                oldClrType: typeof(int),
                oldType: "int"*/);

            //Manual
            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Items");

            migrationBuilder.AddColumn<Guid>(  //Manual. Was AlterColumn
                name: "CollectionId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: false/*,
                oldClrType: typeof(int),
                oldType: "int"*/);

            migrationBuilder.AlterColumn<string>(
                name: "CollectionUserId",
                table: "Items",
                type: "nvarchar(450)", //Manual. Was (max)
                maxLength: 450, //Manual
                nullable: false,  //Manual. Was true
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");


            //Manual
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Items");

            migrationBuilder.AddColumn<Guid>(  //Manual. Was AlterColumn
                name: "Id",
                table: "Items",
                //type: "uniqueidentifier",  //Manual commented
                defaultValueSql: "newsequentialid()", //Manual
                nullable: false/*,
                oldClrType: typeof(int),
                oldType: "int"*/);           

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Collections",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,  //Manual. Was true
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            //Manual
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Collections");

            migrationBuilder.AddColumn<Guid>(  //Manual. Was AlterColumn
                name: "Id",
                table: "Collections",
                //type: "uniqueidentifier",  //Manual commented
                defaultValueSql: "newsequentialid()",  //Manual
                nullable: false/*,
                oldClrType: typeof(int),
                oldType: "int"*/);           

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                column: "Id");

            //Manual
            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                columns: new[] { "ItemId", "TagValue" });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ItemId",
                table: "Tags",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CollectionId",
                table: "Items",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_AspNetUsers_UserId",
                table: "Collections",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade); //Manual. Was restrict

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Collections_CollectionId",
                table: "Items",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Items_ItemId",
                table: "Tags",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_AspNetUsers_UserId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Collections_CollectionId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Items_ItemId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ItemId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_CollectionId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Tags",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "CollectionUserId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CollectionId",
                table: "Items",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Items",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Collections",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Collections",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "intId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                columns: new[] { "Id", "CollectionUserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                columns: new[] { "Id", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ItemId_ItemCollectionUserId",
                table: "Tags",
                columns: new[] { "ItemId", "ItemCollectionUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Items_CollectionId_CollectionUserId",
                table: "Items",
                columns: new[] { "CollectionId", "CollectionUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_AspNetUsers_UserId",
                table: "Collections",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Collections_CollectionId_CollectionUserId",
                table: "Items",
                columns: new[] { "CollectionId", "CollectionUserId" },
                principalTable: "Collections",
                principalColumns: new[] { "Id", "UserId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Items_ItemId_ItemCollectionUserId",
                table: "Tags",
                columns: new[] { "ItemId", "ItemCollectionUserId" },
                principalTable: "Items",
                principalColumns: new[] { "Id", "CollectionUserId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}

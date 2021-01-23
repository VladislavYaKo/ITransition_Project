using Microsoft.EntityFrameworkCore.Migrations;

namespace ITransitionProject.Migrations
{
    public partial class SmallItemModelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_AdditionalFieldsNames_AddFieldsId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_AddFieldsId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "AddFieldsId",
                table: "Items");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddFieldsId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_AddFieldsId",
                table: "Items",
                column: "AddFieldsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AdditionalFieldsNames_AddFieldsId",
                table: "Items",
                column: "AddFieldsId",
                principalTable: "AdditionalFieldsNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

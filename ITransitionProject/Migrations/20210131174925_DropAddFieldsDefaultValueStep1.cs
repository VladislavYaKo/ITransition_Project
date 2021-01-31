using Microsoft.EntityFrameworkCore.Migrations;

namespace ITransitionProject.Migrations
{
    public partial class DropAddFieldsDefaultValueStep1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //All manual

            migrationBuilder.DropIndex(
                name: "IX_Items_AddFieldsValuesId",
                table: "Items");
            migrationBuilder.DropForeignKey(
                name: "FK_Items_AdditionalFieldsValues_AddFieldsValuesId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "AddFieldsValuesId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Collections_AddFieldsNamesId",
                table: "Collections");
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_AdditionalFieldsNames_AddFieldsNamesId",
                table: "Collections");
            migrationBuilder.DropColumn(
                name: "AddFieldsNamesId",
                table: "Collections");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}

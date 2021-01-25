using Microsoft.EntityFrameworkCore.Migrations;

namespace ITransitionProject.Migrations
{
    public partial class AddFieldsKeysChangeStep2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_AdditionalFieldsNames_AddFieldsNamesForChangeId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AdditionalFieldsValues_AddFieldsValuesForChangeId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "AddFieldsValuesForChangeId",
                table: "Items",
                newName: "AddFieldsValuesId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_AddFieldsValuesForChangeId",
                table: "Items",
                newName: "IX_Items_AddFieldsValuesId");

            migrationBuilder.RenameColumn(
                name: "AddFieldsNamesForChangeId",
                table: "Collections",
                newName: "AddFieldsNamesId");

            migrationBuilder.RenameIndex(
                name: "IX_Collections_AddFieldsNamesForChangeId",
                table: "Collections",
                newName: "IX_Collections_AddFieldsNamesId");

            migrationBuilder.RenameColumn(
                name: "ForChangeId",
                table: "AdditionalFieldsValues",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ForChangeId",
                table: "AdditionalFieldsNames",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_AdditionalFieldsNames_AddFieldsNamesId",
                table: "Collections",
                column: "AddFieldsNamesId",
                principalTable: "AdditionalFieldsNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AdditionalFieldsValues_AddFieldsValuesId",
                table: "Items",
                column: "AddFieldsValuesId",
                principalTable: "AdditionalFieldsValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_AdditionalFieldsNames_AddFieldsNamesId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AdditionalFieldsValues_AddFieldsValuesId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "AddFieldsValuesId",
                table: "Items",
                newName: "AddFieldsValuesForChangeId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_AddFieldsValuesId",
                table: "Items",
                newName: "IX_Items_AddFieldsValuesForChangeId");

            migrationBuilder.RenameColumn(
                name: "AddFieldsNamesId",
                table: "Collections",
                newName: "AddFieldsNamesForChangeId");

            migrationBuilder.RenameIndex(
                name: "IX_Collections_AddFieldsNamesId",
                table: "Collections",
                newName: "IX_Collections_AddFieldsNamesForChangeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AdditionalFieldsValues",
                newName: "ForChangeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AdditionalFieldsNames",
                newName: "ForChangeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_AdditionalFieldsNames_AddFieldsNamesForChangeId",
                table: "Collections",
                column: "AddFieldsNamesForChangeId",
                principalTable: "AdditionalFieldsNames",
                principalColumn: "ForChangeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AdditionalFieldsValues_AddFieldsValuesForChangeId",
                table: "Items",
                column: "AddFieldsValuesForChangeId",
                principalTable: "AdditionalFieldsValues",
                principalColumn: "ForChangeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

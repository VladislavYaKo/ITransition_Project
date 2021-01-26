using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITransitionProject.Migrations
{
    public partial class AFNModelPropertyCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_AdditionalFieldsNames_AddFieldsNamesId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AdditionalFieldsValues_AddFieldsValuesId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "BooleabFieldsNames",
                table: "AdditionalFieldsNames",
                newName: "BooleanFieldsNames");

            migrationBuilder.AlterColumn<Guid>(
                name: "AddFieldsValuesId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: true, //generated: false
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AddFieldsNamesId",
                table: "Collections",
                type: "uniqueidentifier",
                nullable: true, //generated: false
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_AdditionalFieldsNames_AddFieldsNamesId",
                table: "Collections",
                column: "AddFieldsNamesId",
                principalTable: "AdditionalFieldsNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AdditionalFieldsValues_AddFieldsValuesId",
                table: "Items",
                column: "AddFieldsValuesId",
                principalTable: "AdditionalFieldsValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                name: "BooleanFieldsNames",
                table: "AdditionalFieldsNames",
                newName: "BooleabFieldsNames");

            migrationBuilder.AlterColumn<Guid>(
                name: "AddFieldsValuesId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "AddFieldsNamesId",
                table: "Collections",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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
    }
}

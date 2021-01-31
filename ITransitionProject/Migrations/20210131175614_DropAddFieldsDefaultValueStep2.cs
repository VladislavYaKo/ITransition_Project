using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ITransitionProject.Migrations
{
    public partial class DropAddFieldsDefaultValueStep2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //All manual

            migrationBuilder.AddColumn<Guid>(
                name: "AddFieldsValuesId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AddFieldsNamesId",
                table: "Collections",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_AddFieldsValuesId",
                table: "Items",
                column: "AddFieldsValuesId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_AddFieldsNamesId",
                table: "Collections",
                column: "AddFieldsNamesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AdditionalFieldsValues_AddFieldsValuesId",
                table: "Items",
                column: "AddFieldsValuesId",
                principalTable: "AdditionalFieldsValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_AdditionalFieldsNames_AddFieldsNamesId",
                table: "Collections",
                column: "AddFieldsNamesId",
                principalTable: "AdditionalFieldsNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

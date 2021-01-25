﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITransitionProject.Migrations
{
    public partial class AddFieldsPropertiesAdding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            /*migrationBuilder.CreateTable(
                name: "AdditionalFieldsNames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumericFieldsNames = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    SingleLineFieldsNames = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MultiLineFieldsNames = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DateFieldsNames = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    BooleabFieldsNames = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalFieldsNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalFieldsValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumericFieldsValues = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SingleLineFieldsValues = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    MultiLineFieldsValues = table.Column<string>(type: "nvarchar(3100)", maxLength: 3100, nullable: true),
                    DateFieldsValues = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BooleanFieldsValues = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalFieldsValues", x => x.Id);
                });*/

            migrationBuilder.CreateIndex(
                name: "IX_Items_AddFieldsValuesId",
                table: "Items",
                column: "AddFieldsValuesId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_AddFieldsNamesId",
                table: "Collections",
                column: "AddFieldsNamesId");

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

            migrationBuilder.DropTable(
                name: "AdditionalFieldsNames");

            migrationBuilder.DropTable(
                name: "AdditionalFieldsValues");

            migrationBuilder.DropIndex(
                name: "IX_Items_AddFieldsValuesId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Collections_AddFieldsNamesId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "AddFieldsValuesId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "AddFieldsNamesId",
                table: "Collections");
        }
    }
}

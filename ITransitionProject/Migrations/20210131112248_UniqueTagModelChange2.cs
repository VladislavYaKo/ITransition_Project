﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ITransitionProject.Migrations
{
    public partial class UniqueTagModelChange2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UniqueTags",
                table: "UniqueTags");

            migrationBuilder.AlterColumn<string>(
                name: "TagValue",
                table: "UniqueTags",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UniqueTags",
                table: "UniqueTags",
                column: "TagValue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UniqueTags",
                table: "UniqueTags");

            migrationBuilder.AlterColumn<string>(
                name: "TagValue",
                table: "UniqueTags",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UniqueTags",
                table: "UniqueTags",
                column: "TagValue");
        }
    }
}

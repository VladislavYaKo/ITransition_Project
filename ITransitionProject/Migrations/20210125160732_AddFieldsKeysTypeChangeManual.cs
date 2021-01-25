using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ITransitionProject.Migrations
{
    public partial class AddFieldsKeysTypeChangeManual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropPrimaryKey("PK_AdditionalFieldsNames", "AdditionalFieldsNames");
            migrationBuilder.DropPrimaryKey("PK_AdditionalFieldsValues", "AdditionalFieldsValues");

            migrationBuilder.DropColumn("Id", "AdditionalFieldsNames");
            migrationBuilder.DropColumn("Id", "AdditionalFieldsValues");
            migrationBuilder.AddColumn<Guid>("Id", "AdditionalFieldsNames", type: "guid", nullable: false);
            migrationBuilder.AddColumn<Guid>("Id", "AdditionalFieldsValues", type: "guid", nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdditionalFieldsNames", 
                table: "AdditionalFieldsNames", 
                column: "Id");
            migrationBuilder.AddPrimaryKey(
                name: "PK_AdditionalFieldsValues",
                table: "AdditionalFieldsValues",
                column: "Id");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

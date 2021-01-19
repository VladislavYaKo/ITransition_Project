using Microsoft.EntityFrameworkCore.Migrations;

namespace ITransitionProject.Migrations
{
    public partial class UsersCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumericFieldsNames = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NumericFieldsValues = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SingleLineFieldsNames = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    SingleLineFieldsValues = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    MultiLineFieldsNames = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MultiLineFieldsValues = table.Column<string>(type: "nvarchar(3100)", maxLength: 3100, nullable: true),
                    DateFieldsNAmes = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DateFieldsValues = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BooleabFieldsNames = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    BooleanFieldsValues = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Theme = table.Column<int>(type: "int", nullable: false),
                    imgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    briefDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collection_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AddFieldsId = table.Column<int>(type: "int", nullable: true),
                    CollectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_AdditionalFields_AddFieldsId",
                        column: x => x.AddFieldsId,
                        principalTable: "AdditionalFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Item_Collection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collection_UserId",
                table: "Collection",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_AddFieldsId",
                table: "Item",
                column: "AddFieldsId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_CollectionId",
                table: "Item",
                column: "CollectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "AdditionalFields");

            migrationBuilder.DropTable(
                name: "Collection");
        }
    }
}

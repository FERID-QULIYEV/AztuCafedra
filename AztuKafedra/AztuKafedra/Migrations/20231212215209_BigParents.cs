using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AztuKafedra.Migrations
{
    public partial class BigParents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BigParentsCategoryId",
                table: "Parentcategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BigParentsCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BigParentsCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parentcategory_BigParentsCategoryId",
                table: "Parentcategory",
                column: "BigParentsCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parentcategory_BigParentsCategory_BigParentsCategoryId",
                table: "Parentcategory",
                column: "BigParentsCategoryId",
                principalTable: "BigParentsCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parentcategory_BigParentsCategory_BigParentsCategoryId",
                table: "Parentcategory");

            migrationBuilder.DropTable(
                name: "BigParentsCategory");

            migrationBuilder.DropIndex(
                name: "IX_Parentcategory_BigParentsCategoryId",
                table: "Parentcategory");

            migrationBuilder.DropColumn(
                name: "BigParentsCategoryId",
                table: "Parentcategory");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AztuKafedra.Migrations
{
    public partial class aqqsl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ChildCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ChildCategory");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AztuKafedra.Migrations
{
    public partial class Aztu1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildCategory_Parentcategory_ParentCategoryId",
                table: "ChildCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Parentcategory_BigParentsCategory_BigParentsCategoryId",
                table: "Parentcategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Pasitions_ChildCategory_ChildCategoryId",
                table: "Pasitions");

            migrationBuilder.DropForeignKey(
                name: "FK_User_ChildCategory_ChildCategoryId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parentcategory",
                table: "Parentcategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChildCategory",
                table: "ChildCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BigParentsCategory",
                table: "BigParentsCategory");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Parentcategory",
                newName: "Parentcategories");

            migrationBuilder.RenameTable(
                name: "ChildCategory",
                newName: "ChildCategories");

            migrationBuilder.RenameTable(
                name: "BigParentsCategory",
                newName: "BigParentsCategories");

            migrationBuilder.RenameIndex(
                name: "IX_User_ChildCategoryId",
                table: "Users",
                newName: "IX_Users_ChildCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Parentcategory_BigParentsCategoryId",
                table: "Parentcategories",
                newName: "IX_Parentcategories_BigParentsCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ChildCategory_ParentCategoryId",
                table: "ChildCategories",
                newName: "IX_ChildCategories_ParentCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parentcategories",
                table: "Parentcategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChildCategories",
                table: "ChildCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BigParentsCategories",
                table: "BigParentsCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildCategories_Parentcategories_ParentCategoryId",
                table: "ChildCategories",
                column: "ParentCategoryId",
                principalTable: "Parentcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parentcategories_BigParentsCategories_BigParentsCategoryId",
                table: "Parentcategories",
                column: "BigParentsCategoryId",
                principalTable: "BigParentsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pasitions_ChildCategories_ChildCategoryId",
                table: "Pasitions",
                column: "ChildCategoryId",
                principalTable: "ChildCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ChildCategories_ChildCategoryId",
                table: "Users",
                column: "ChildCategoryId",
                principalTable: "ChildCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildCategories_Parentcategories_ParentCategoryId",
                table: "ChildCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Parentcategories_BigParentsCategories_BigParentsCategoryId",
                table: "Parentcategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Pasitions_ChildCategories_ChildCategoryId",
                table: "Pasitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_ChildCategories_ChildCategoryId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parentcategories",
                table: "Parentcategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChildCategories",
                table: "ChildCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BigParentsCategories",
                table: "BigParentsCategories");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Parentcategories",
                newName: "Parentcategory");

            migrationBuilder.RenameTable(
                name: "ChildCategories",
                newName: "ChildCategory");

            migrationBuilder.RenameTable(
                name: "BigParentsCategories",
                newName: "BigParentsCategory");

            migrationBuilder.RenameIndex(
                name: "IX_Users_ChildCategoryId",
                table: "User",
                newName: "IX_User_ChildCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Parentcategories_BigParentsCategoryId",
                table: "Parentcategory",
                newName: "IX_Parentcategory_BigParentsCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ChildCategories_ParentCategoryId",
                table: "ChildCategory",
                newName: "IX_ChildCategory_ParentCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parentcategory",
                table: "Parentcategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChildCategory",
                table: "ChildCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BigParentsCategory",
                table: "BigParentsCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildCategory_Parentcategory_ParentCategoryId",
                table: "ChildCategory",
                column: "ParentCategoryId",
                principalTable: "Parentcategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parentcategory_BigParentsCategory_BigParentsCategoryId",
                table: "Parentcategory",
                column: "BigParentsCategoryId",
                principalTable: "BigParentsCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pasitions_ChildCategory_ChildCategoryId",
                table: "Pasitions",
                column: "ChildCategoryId",
                principalTable: "ChildCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_ChildCategory_ChildCategoryId",
                table: "User",
                column: "ChildCategoryId",
                principalTable: "ChildCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

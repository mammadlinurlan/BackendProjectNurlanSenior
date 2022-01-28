using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendProjectNurlanSenior.Migrations
{
    public partial class mgmg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CCategoryId",
                table: "Courses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CCategoryId",
                table: "Courses",
                column: "CCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CCategories_CCategoryId",
                table: "Courses",
                column: "CCategoryId",
                principalTable: "CCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CCategories_CCategoryId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CCategoryId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CCategoryId",
                table: "Courses");
        }
    }
}

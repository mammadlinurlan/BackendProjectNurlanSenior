using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendProjectNurlanSenior.Migrations
{
    public partial class updateCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CCategories_CCategoryId",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Courses",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<int>(
                name: "CCategoryId",
                table: "Courses",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CCategories_CCategoryId",
                table: "Courses",
                column: "CCategoryId",
                principalTable: "CCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CCategories_CCategoryId",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Courses",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CCategoryId",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CCategories_CCategoryId",
                table: "Courses",
                column: "CCategoryId",
                principalTable: "CCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

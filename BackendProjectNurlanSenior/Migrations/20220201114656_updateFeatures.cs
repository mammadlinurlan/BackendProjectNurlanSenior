using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendProjectNurlanSenior.Migrations
{
    public partial class updateFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CCategories_CCategoryId",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "LeaveReply",
                table: "Courses",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<int>(
                name: "CCategoryId",
                table: "Courses",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<string>(
                name: "LeaveReply",
                table: "Courses",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CCategoryId",
                table: "Courses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CCategories_CCategoryId",
                table: "Courses",
                column: "CCategoryId",
                principalTable: "CCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendProjectNurlanSenior.Migrations
{
    public partial class socialAddedToTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Speciality",
                table: "Teachers",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fblink",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instalink",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PinterestLink",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Vimeolink",
                table: "Teachers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fblink",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Instalink",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "PinterestLink",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Vimeolink",
                table: "Teachers");

            migrationBuilder.AlterColumn<string>(
                name: "Speciality",
                table: "Teachers",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 70);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendProjectNurlanSenior.Migrations
{
    public partial class createdSettingsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FbLink",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstaLink",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterLink",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VimeoLink",
                table: "Settings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FbLink",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "InstaLink",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "TwitterLink",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "VimeoLink",
                table: "Settings");
        }
    }
}

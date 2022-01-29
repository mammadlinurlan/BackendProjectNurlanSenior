using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendProjectNurlanSenior.Migrations
{
    public partial class createdSettingsTablesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubsDesc",
                table: "Settings",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FooterPhone",
                table: "Settings",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "Settings",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Settings",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FooterPhone",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Mail",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Settings");

            migrationBuilder.AlterColumn<string>(
                name: "SubsDesc",
                table: "Settings",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendProjectNurlanSenior.Migrations
{
    public partial class createdSettingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(maxLength: 150, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    LogoBig = table.Column<string>(maxLength: 150, nullable: true),
                    LogoLittle = table.Column<string>(maxLength: 150, nullable: true),
                    SearchIcon = table.Column<string>(nullable: true),
                    SubsTitle = table.Column<string>(maxLength: 150, nullable: true),
                    SubsDesc = table.Column<string>(maxLength: 150, nullable: true),
                    FooterDesc = table.Column<string>(maxLength: 250, nullable: true),
                    FbIcon = table.Column<string>(nullable: true),
                    InstaIcon = table.Column<string>(nullable: true),
                    TwitterIcon = table.Column<string>(nullable: true),
                    VimeoIcon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}

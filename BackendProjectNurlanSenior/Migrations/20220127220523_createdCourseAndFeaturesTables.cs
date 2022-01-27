using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendProjectNurlanSenior.Migrations
{
    public partial class createdCourseAndFeaturesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Image = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: false),
                    About = table.Column<string>(maxLength: 250, nullable: false),
                    Certification = table.Column<string>(maxLength: 250, nullable: false),
                    HowToApply = table.Column<string>(maxLength: 250, nullable: false),
                    LeaveReply = table.Column<string>(maxLength: 250, nullable: false),
                    LinkLogo = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<string>(maxLength: 50, nullable: false),
                    ClassDuration = table.Column<string>(maxLength: 50, nullable: false),
                    SkillLevel = table.Column<string>(maxLength: 50, nullable: false),
                    Language = table.Column<string>(maxLength: 50, nullable: false),
                    Assesments = table.Column<string>(maxLength: 50, nullable: false),
                    StudentCount = table.Column<int>(nullable: false),
                    Fee = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Features_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Features_CourseId",
                table: "Features",
                column: "CourseId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}

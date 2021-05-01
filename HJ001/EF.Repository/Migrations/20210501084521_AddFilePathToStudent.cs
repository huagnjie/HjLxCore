using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentRepository.Migrations
{
    public partial class AddFilePathToStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Student",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Student");
        }
    }
}

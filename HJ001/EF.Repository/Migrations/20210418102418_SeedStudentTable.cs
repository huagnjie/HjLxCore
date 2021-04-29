using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentRepository.Migrations
{
    public partial class SeedStudentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "Id", "ClassName", "Email", "Name" },
                values: new object[] { 1, 1, "123", "黄杰" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

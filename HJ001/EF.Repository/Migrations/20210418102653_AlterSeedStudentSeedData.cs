using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentRepository.Migrations
{
    public partial class AlterSeedStudentSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "1341951524@163.com");

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "Id", "ClassName", "Email", "Name" },
                values: new object[] { 2, 2, "1341951524@qq.com", "黄小杰" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "123");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Student.Migrations
{
    public partial class addingStudentPassworAndRegno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentPassword",
                table: "Student",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentRegNo",
                table: "Student",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentPassword",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "StudentRegNo",
                table: "Student");
        }
    }
}

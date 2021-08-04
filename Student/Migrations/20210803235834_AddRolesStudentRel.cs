using Microsoft.EntityFrameworkCore.Migrations;

namespace Student.Migrations
{
    public partial class AddRolesStudentRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Teacher_TeacherId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_TeacherId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Course");

            migrationBuilder.CreateTable(
                name: "CourseTeacher",
                columns: table => new
                {
                    CoursesCourseId = table.Column<int>(type: "integer", nullable: false),
                    TeachersTeacherId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTeacher", x => new { x.CoursesCourseId, x.TeachersTeacherId });
                    table.ForeignKey(
                        name: "FK_CourseTeacher_Course_CoursesCourseId",
                        column: x => x.CoursesCourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTeacher_Teacher_TeachersTeacherId",
                        column: x => x.TeachersTeacherId,
                        principalTable: "Teacher",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseTeacher_TeachersTeacherId",
                table: "CourseTeacher",
                column: "TeachersTeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseTeacher");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Course",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_TeacherId",
                table: "Course",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Teacher_TeacherId",
                table: "Course",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

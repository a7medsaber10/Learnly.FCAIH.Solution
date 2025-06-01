using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learnly.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrolledCourse_Enrollment_EnrollmentId",
                table: "EnrolledCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollment",
                table: "Enrollment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrolledCourse",
                table: "EnrolledCourse");

            migrationBuilder.RenameTable(
                name: "Enrollment",
                newName: "Enrollments");

            migrationBuilder.RenameTable(
                name: "EnrolledCourse",
                newName: "EnrolledCourses");

            migrationBuilder.RenameIndex(
                name: "IX_EnrolledCourse_EnrollmentId",
                table: "EnrolledCourses",
                newName: "IX_EnrolledCourses_EnrollmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrolledCourses",
                table: "EnrolledCourses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolledCourses_Enrollments_EnrollmentId",
                table: "EnrolledCourses",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrolledCourses_Enrollments_EnrollmentId",
                table: "EnrolledCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrolledCourses",
                table: "EnrolledCourses");

            migrationBuilder.RenameTable(
                name: "Enrollments",
                newName: "Enrollment");

            migrationBuilder.RenameTable(
                name: "EnrolledCourses",
                newName: "EnrolledCourse");

            migrationBuilder.RenameIndex(
                name: "IX_EnrolledCourses_EnrollmentId",
                table: "EnrolledCourse",
                newName: "IX_EnrolledCourse_EnrollmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollment",
                table: "Enrollment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrolledCourse",
                table: "EnrolledCourse",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolledCourse_Enrollment_EnrollmentId",
                table: "EnrolledCourse",
                column: "EnrollmentId",
                principalTable: "Enrollment",
                principalColumn: "Id");
        }
    }
}

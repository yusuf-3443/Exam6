using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedBacks",
                table: "FeedBacks");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Assignments");

            migrationBuilder.RenameTable(
                name: "FeedBacks",
                newName: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "Courses",
                newName: "Credits");

            migrationBuilder.AddColumn<string>(
                name: "Context",
                table: "Submissions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollmentDate",
                table: "Students",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentURL",
                table: "Materials",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Materials",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Materials",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FeedbackDate",
                table: "Feedbacks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Feedbacks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Courses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Instructor",
                table: "Courses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Courses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Assignments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Assignments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Assignments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_AssignmentId",
                table: "Submissions",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_StudentId",
                table: "Submissions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_CourseId",
                table: "Materials",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_AssignmentId",
                table: "Feedbacks",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_StudentId",
                table: "Feedbacks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_CourseId",
                table: "Assignments",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Assignments_AssignmentId",
                table: "Feedbacks",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Students_StudentId",
                table: "Feedbacks",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Courses_CourseId",
                table: "Materials",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Assignments_AssignmentId",
                table: "Submissions",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Students_StudentId",
                table: "Submissions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Assignments_AssignmentId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Students_StudentId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Courses_CourseId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Assignments_AssignmentId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Students_StudentId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_AssignmentId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_StudentId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Materials_CourseId",
                table: "Materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_AssignmentId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_StudentId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_CourseId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "Context",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "EnrollmentDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ContentURL",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "FeedbackDate",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Instructor",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Assignments");

            migrationBuilder.RenameTable(
                name: "Feedbacks",
                newName: "FeedBacks");

            migrationBuilder.RenameColumn(
                name: "Credits",
                table: "Courses",
                newName: "MyProperty");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Submissions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Assignments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedBacks",
                table: "FeedBacks",
                column: "Id");
        }
    }
}

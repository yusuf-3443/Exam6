using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Assignments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Assignments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_StudentId",
                table: "Assignments",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Students_StudentId",
                table: "Assignments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Students_StudentId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_StudentId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Assignments");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Assignments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}

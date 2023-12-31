using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduArk.Infrastructure.Tenant.Migrations
{
    /// <inheritdoc />
    public partial class Edu_Ark_Development_78_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTargetSetting_Student_AcademicLevelId",
                table: "SubjectTargetSetting");

            migrationBuilder.AddColumn<string>(
                name: "FileUploadUrl",
                table: "Lesson",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTargetSetting_Student_StudentId",
                table: "SubjectTargetSetting",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTargetSetting_Student_StudentId",
                table: "SubjectTargetSetting");

            migrationBuilder.DropColumn(
                name: "FileUploadUrl",
                table: "Lesson");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTargetSetting_Student_AcademicLevelId",
                table: "SubjectTargetSetting",
                column: "AcademicLevelId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

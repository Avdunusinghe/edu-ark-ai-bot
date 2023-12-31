using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduArk.Infrastructure.Tenant.Migrations
{
    /// <inheritdoc />
    public partial class Edu_Ark_Development_78_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_ExamType_ExamTypeId",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamMark_AcademicYear_AcademicYearId",
                table: "ExamMark");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamMark_Semester_SemesterId",
                table: "ExamMark");

            migrationBuilder.DropIndex(
                name: "IX_ExamMark_AcademicYearId",
                table: "ExamMark");

            migrationBuilder.DropIndex(
                name: "IX_ExamMark_SemesterId",
                table: "ExamMark");

            migrationBuilder.DropColumn(
                name: "AcademicYearId",
                table: "ExamMark");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "ExamMark");

            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "Exam",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Exam_AcademicYearId",
                table: "Exam",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_SemesterId",
                table: "Exam",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_AcademicYear_AcademicYearId",
                table: "Exam",
                column: "AcademicYearId",
                principalTable: "AcademicYear",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_ExamType_ExamTypeId",
                table: "Exam",
                column: "ExamTypeId",
                principalTable: "ExamType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Semester_SemesterId",
                table: "Exam",
                column: "SemesterId",
                principalTable: "Semester",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_AcademicYear_AcademicYearId",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_Exam_ExamType_ExamTypeId",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Semester_SemesterId",
                table: "Exam");

            migrationBuilder.DropIndex(
                name: "IX_Exam_AcademicYearId",
                table: "Exam");

            migrationBuilder.DropIndex(
                name: "IX_Exam_SemesterId",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "Exam");

            migrationBuilder.AddColumn<int>(
                name: "AcademicYearId",
                table: "ExamMark",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "ExamMark",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExamMark_AcademicYearId",
                table: "ExamMark",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamMark_SemesterId",
                table: "ExamMark",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_ExamType_ExamTypeId",
                table: "Exam",
                column: "ExamTypeId",
                principalTable: "ExamType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamMark_AcademicYear_AcademicYearId",
                table: "ExamMark",
                column: "AcademicYearId",
                principalTable: "AcademicYear",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamMark_Semester_SemesterId",
                table: "ExamMark",
                column: "SemesterId",
                principalTable: "Semester",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

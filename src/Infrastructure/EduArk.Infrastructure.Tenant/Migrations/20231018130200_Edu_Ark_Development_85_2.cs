using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduArk.Infrastructure.Tenant.Migrations
{
    /// <inheritdoc />
    public partial class Edu_Ark_Development_85_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImportantFactorsAcademicPerformance",
                table: "Student");

            migrationBuilder.AlterColumn<int>(
                name: "StudyHours",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ConfidentAcademicPerformance",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ClassAttendance",
                table: "Student",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PersonalMotivation",
                table: "Student",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PriorKnowledgeOfTheSubject",
                table: "Student",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StudyEnvironment",
                table: "Student",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TeacherInstructorQuality",
                table: "Student",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TimeManagementSkills",
                table: "Student",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassAttendance",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "PersonalMotivation",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "PriorKnowledgeOfTheSubject",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "StudyEnvironment",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "TeacherInstructorQuality",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "TimeManagementSkills",
                table: "Student");

            migrationBuilder.AlterColumn<int>(
                name: "StudyHours",
                table: "Student",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ConfidentAcademicPerformance",
                table: "Student",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImportantFactorsAcademicPerformance",
                table: "Student",
                type: "int",
                nullable: true,
                defaultValue: 0);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduArk.Infrastructure.Tenant.Migrations
{
    /// <inheritdoc />
    public partial class Edu_Ark_Development_85_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConfidentAcademicPerformance",
                table: "Student",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImportantFactorsAcademicPerformance",
                table: "Student",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudyHours",
                table: "Student",
                type: "int",
                nullable: true,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfidentAcademicPerformance",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "ImportantFactorsAcademicPerformance",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "StudyHours",
                table: "Student");
        }
    }
}

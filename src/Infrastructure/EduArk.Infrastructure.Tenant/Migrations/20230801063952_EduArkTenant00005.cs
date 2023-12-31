using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduArk.Infrastructure.Tenant.Migrations
{
    /// <inheritdoc />
    public partial class EduArkTenant00005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Semester",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTargetSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    AcademicYearId = table.Column<int>(type: "int", nullable: false),
                    AcademicLevelId = table.Column<int>(type: "int", nullable: false),
                    SemesterId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    PredictedMark = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TeacherTargetScore = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTargetSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectTargetSetting_AcademicLevel_AcademicLevelId",
                        column: x => x.AcademicLevelId,
                        principalTable: "AcademicLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectTargetSetting_AcademicYear_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectTargetSetting_Semester_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectTargetSetting_Student_AcademicLevelId",
                        column: x => x.AcademicLevelId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectTargetSetting_Subject_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTargetSetting_AcademicLevelId",
                table: "SubjectTargetSetting",
                column: "AcademicLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTargetSetting_AcademicYearId",
                table: "SubjectTargetSetting",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTargetSetting_SemesterId",
                table: "SubjectTargetSetting",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTargetSetting_StudentId",
                table: "SubjectTargetSetting",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectTargetSetting");

            migrationBuilder.DropTable(
                name: "Semester");
        }
    }
}

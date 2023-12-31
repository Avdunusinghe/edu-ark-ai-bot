using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduArk.Infrastructure.Tenant.Migrations
{
    /// <inheritdoc />
    public partial class EduArkTenant00002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LearningPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolGrade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentMark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AverageMark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LearningPattern = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearningPlan_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LearningPlan_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonGrade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonType = table.Column<int>(type: "int", nullable: true),
                    LearningPlanId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lesson_LearningPlan_LearningPlanId",
                        column: x => x.LearningPlanId,
                        principalTable: "LearningPlan",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lesson_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lesson_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonTypeAudio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonType = table.Column<int>(type: "int", nullable: false),
                    AudioFileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTypeAudio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonTypeAudio_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonTypeAudio_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonTypeAudio_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonTypeText",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonType = table.Column<int>(type: "int", nullable: false),
                    TextContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTypeText", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonTypeText_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonTypeText_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonTypeText_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonTypeVideo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonType = table.Column<int>(type: "int", nullable: false),
                    VideoFileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTypeVideo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonTypeVideo_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonTypeVideo_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonTypeVideo_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LearningPlan_CreatedByUserId",
                table: "LearningPlan",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningPlan_UpdatedByUserId",
                table: "LearningPlan",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_CreatedByUserId",
                table: "Lesson",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_LearningPlanId",
                table: "Lesson",
                column: "LearningPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_UpdatedByUserId",
                table: "Lesson",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTypeAudio_CreatedByUserId",
                table: "LessonTypeAudio",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTypeAudio_LessonId",
                table: "LessonTypeAudio",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTypeAudio_UpdatedByUserId",
                table: "LessonTypeAudio",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTypeText_CreatedByUserId",
                table: "LessonTypeText",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTypeText_LessonId",
                table: "LessonTypeText",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTypeText_UpdatedByUserId",
                table: "LessonTypeText",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTypeVideo_CreatedByUserId",
                table: "LessonTypeVideo",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTypeVideo_LessonId",
                table: "LessonTypeVideo",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTypeVideo_UpdatedByUserId",
                table: "LessonTypeVideo",
                column: "UpdatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonTypeAudio");

            migrationBuilder.DropTable(
                name: "LessonTypeText");

            migrationBuilder.DropTable(
                name: "LessonTypeVideo");

            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropTable(
                name: "LearningPlan");
        }
    }
}

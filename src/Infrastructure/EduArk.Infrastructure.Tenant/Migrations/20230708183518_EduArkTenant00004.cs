using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduArk.Infrastructure.Tenant.Migrations
{
    /// <inheritdoc />
    public partial class EduArkTenant00004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelHeadId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicLevel_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AcademicLevel_User_LevelHeadId",
                        column: x => x.LevelHeadId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AcademicLevel_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AcademicYear",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsCurrentYear = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYear", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicYear_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AcademicYear_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassName",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassName", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassName_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassName_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExamType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AdmissionNo = table.Column<int>(type: "int", nullable: false),
                    EmegencyContactNo1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmegencyContactNo2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_User_Id",
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectStream",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectStream", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: false),
                    LearningExperience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topic_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Topic_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    ClassNameId = table.Column<int>(type: "int", nullable: false),
                    AcademicLevelId = table.Column<int>(type: "int", nullable: false),
                    AcademicYearId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassCategory = table.Column<int>(type: "int", nullable: false),
                    LanguageStream = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => new { x.ClassNameId, x.AcademicLevelId, x.AcademicYearId });
                    table.ForeignKey(
                        name: "FK_Class_AcademicLevel_AcademicLevelId",
                        column: x => x.AcademicLevelId,
                        principalTable: "AcademicLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Class_AcademicYear_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Class_ClassName_ClassNameId",
                        column: x => x.ClassNameId,
                        principalTable: "ClassName",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Class_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Class_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false),
                    AcademicYearId = table.Column<int>(type: "int", nullable: false),
                    ExamTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exam_ExamType_ExamTypeId",
                        column: x => x.ExamTypeId,
                        principalTable: "ExamType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exam_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exam_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectCategory = table.Column<int>(type: "int", nullable: false),
                    IsParentBasketSubject = table.Column<bool>(type: "bit", nullable: false),
                    IsBuscketSubject = table.Column<bool>(type: "bit", nullable: false),
                    ParentBasketSubjectId = table.Column<int>(type: "int", nullable: true),
                    SubjectStreamId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                    table.UniqueConstraint("AK_Subject_Name", x => x.Name);
                    table.UniqueConstraint("AK_Subject_SubjectCode", x => x.SubjectCode);
                    table.ForeignKey(
                        name: "FK_Subject_SubjectStream_SubjectStreamId",
                        column: x => x.SubjectStreamId,
                        principalTable: "SubjectStream",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subject_Subject_ParentBasketSubjectId",
                        column: x => x.ParentBasketSubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subject_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subject_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assessment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssessmentType = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assessment_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assessment_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assessment_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassTeacher",
                columns: table => new
                {
                    ClassNameId = table.Column<int>(type: "int", nullable: false),
                    AcademicLevelId = table.Column<int>(type: "int", nullable: false),
                    AcademicYearId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassTeacher", x => new { x.ClassNameId, x.AcademicLevelId, x.AcademicYearId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_ClassTeacher_Class_ClassNameId_AcademicLevelId_AcademicYearId",
                        columns: x => new { x.ClassNameId, x.AcademicLevelId, x.AcademicYearId },
                        principalTable: "Class",
                        principalColumns: new[] { "ClassNameId", "AcademicLevelId", "AcademicYearId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassTeacher_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassTeacher_User_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassTeacher_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentClass",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ClassNameId = table.Column<int>(type: "int", nullable: false),
                    AcademicYearId = table.Column<int>(type: "int", nullable: false),
                    AcademicLevelId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentClass", x => new { x.StudentId, x.ClassNameId, x.AcademicLevelId, x.AcademicYearId });
                    table.ForeignKey(
                        name: "FK_StudentClass_Class_ClassNameId_AcademicLevelId_AcademicYearId",
                        columns: x => new { x.ClassNameId, x.AcademicLevelId, x.AcademicYearId },
                        principalTable: "Class",
                        principalColumns: new[] { "ClassNameId", "AcademicLevelId", "AcademicYearId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentClass_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectAcademicLevel",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    AcademicLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectAcademicLevel", x => new { x.SubjectId, x.AcademicLevelId });
                    table.ForeignKey(
                        name: "FK_SubjectAcademicLevel_AcademicLevel_AcademicLevelId",
                        column: x => x.AcademicLevelId,
                        principalTable: "AcademicLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectAcademicLevel_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTeacher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcademicLevelId = table.Column<int>(type: "int", nullable: false),
                    AcademicYearId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTeacher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_AcademicLevel_AcademicLevelId",
                        column: x => x.AcademicLevelId,
                        principalTable: "AcademicLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_AcademicYear_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_User_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EssayQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentId = table.Column<int>(type: "int", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marks = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EssayQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EssayQuestion_Assessment_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EssayQuestion_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EssayQuestion_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MCQQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentId = table.Column<int>(type: "int", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marks = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MCQQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MCQQuestions_Assessment_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MCQQuestions_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MCQQuestions_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StructuredQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marks = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    AssessmentId1 = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructuredQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StructuredQuestion_Assessment_AssessmentId1",
                        column: x => x.AssessmentId1,
                        principalTable: "Assessment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StructuredQuestion_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StructuredQuestion_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentClassSubject",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ClassNameId = table.Column<int>(type: "int", nullable: false),
                    AcademicYearId = table.Column<int>(type: "int", nullable: false),
                    AcademicLevelId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentClassSubject", x => new { x.StudentId, x.ClassNameId, x.AcademicLevelId, x.AcademicYearId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_StudentClassSubject_StudentClass_StudentId_ClassNameId_AcademicLevelId_AcademicYearId",
                        columns: x => new { x.StudentId, x.ClassNameId, x.AcademicLevelId, x.AcademicYearId },
                        principalTable: "StudentClass",
                        principalColumns: new[] { "StudentId", "ClassNameId", "AcademicLevelId", "AcademicYearId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentClassSubject_SubjectAcademicLevel_SubjectId_AcademicLevelId",
                        columns: x => new { x.SubjectId, x.AcademicLevelId },
                        principalTable: "SubjectAcademicLevel",
                        principalColumns: new[] { "SubjectId", "AcademicLevelId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassSubjectTeacher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassNameId = table.Column<int>(type: "int", nullable: false),
                    AcademicLevelId = table.Column<int>(type: "int", nullable: false),
                    AcademicYearId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    SubjectTeacherId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSubjectTeacher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassSubjectTeacher_Class_ClassNameId_AcademicLevelId_AcademicYearId",
                        columns: x => new { x.ClassNameId, x.AcademicLevelId, x.AcademicYearId },
                        principalTable: "Class",
                        principalColumns: new[] { "ClassNameId", "AcademicLevelId", "AcademicYearId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassSubjectTeacher_SubjectTeacher_SubjectTeacherId",
                        column: x => x.SubjectTeacherId,
                        principalTable: "SubjectTeacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassSubjectTeacher_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassSubjectTeacher_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassSubjectTeacher_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HeadOfDepartment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    AcademicLevelId = table.Column<int>(type: "int", nullable: false),
                    AcademicYearId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    HeadOfDepartmentId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeadOfDepartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeadOfDepartment_AcademicLevel_AcademicLevelId",
                        column: x => x.AcademicLevelId,
                        principalTable: "AcademicLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HeadOfDepartment_AcademicYear_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HeadOfDepartment_HeadOfDepartment_HeadOfDepartmentId",
                        column: x => x.HeadOfDepartmentId,
                        principalTable: "HeadOfDepartment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HeadOfDepartment_SubjectTeacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "SubjectTeacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HeadOfDepartment_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HeadOfDepartment_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HeadOfDepartment_User_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicLevel_CreatedByUserId",
                table: "AcademicLevel",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicLevel_LevelHeadId",
                table: "AcademicLevel",
                column: "LevelHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicLevel_UpdatedByUserId",
                table: "AcademicLevel",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYear_CreatedByUserId",
                table: "AcademicYear",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYear_UpdatedByUserId",
                table: "AcademicYear",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_CreatedByUserId",
                table: "Assessment",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_TopicId",
                table: "Assessment",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_UpdatedByUserId",
                table: "Assessment",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_AcademicLevelId",
                table: "Class",
                column: "AcademicLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_AcademicYearId",
                table: "Class",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_CreatedByUserId",
                table: "Class",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_UpdatedByUserId",
                table: "Class",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassName_CreatedByUserId",
                table: "ClassName",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassName_UpdatedByUserId",
                table: "ClassName",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjectTeacher_ClassNameId_AcademicLevelId_AcademicYearId",
                table: "ClassSubjectTeacher",
                columns: new[] { "ClassNameId", "AcademicLevelId", "AcademicYearId" });

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjectTeacher_CreatedByUserId",
                table: "ClassSubjectTeacher",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjectTeacher_SubjectId",
                table: "ClassSubjectTeacher",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjectTeacher_SubjectTeacherId",
                table: "ClassSubjectTeacher",
                column: "SubjectTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjectTeacher_UpdatedByUserId",
                table: "ClassSubjectTeacher",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTeacher_CreatedByUserId",
                table: "ClassTeacher",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTeacher_TeacherId",
                table: "ClassTeacher",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTeacher_UpdatedByUserId",
                table: "ClassTeacher",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EssayQuestion_AssessmentId",
                table: "EssayQuestion",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EssayQuestion_CreatedByUserId",
                table: "EssayQuestion",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EssayQuestion_UpdatedByUserId",
                table: "EssayQuestion",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_CreatedByUserId",
                table: "Exam",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_ExamTypeId",
                table: "Exam",
                column: "ExamTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_UpdatedByUserId",
                table: "Exam",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HeadOfDepartment_AcademicLevelId",
                table: "HeadOfDepartment",
                column: "AcademicLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_HeadOfDepartment_AcademicYearId",
                table: "HeadOfDepartment",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_HeadOfDepartment_CreatedByUserId",
                table: "HeadOfDepartment",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HeadOfDepartment_HeadOfDepartmentId",
                table: "HeadOfDepartment",
                column: "HeadOfDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_HeadOfDepartment_SubjectId",
                table: "HeadOfDepartment",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_HeadOfDepartment_TeacherId",
                table: "HeadOfDepartment",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_HeadOfDepartment_UpdatedByUserId",
                table: "HeadOfDepartment",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MCQQuestions_AssessmentId",
                table: "MCQQuestions",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MCQQuestions_CreatedByUserId",
                table: "MCQQuestions",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MCQQuestions_UpdatedByUserId",
                table: "MCQQuestions",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StructuredQuestion_AssessmentId1",
                table: "StructuredQuestion",
                column: "AssessmentId1");

            migrationBuilder.CreateIndex(
                name: "IX_StructuredQuestion_CreatedByUserId",
                table: "StructuredQuestion",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StructuredQuestion_UpdatedByUserId",
                table: "StructuredQuestion",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_AdmissionNo",
                table: "Student",
                column: "AdmissionNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_CreatedByUserId",
                table: "Student",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_UpdatedByUserId",
                table: "Student",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClass_ClassNameId_AcademicLevelId_AcademicYearId",
                table: "StudentClass",
                columns: new[] { "ClassNameId", "AcademicLevelId", "AcademicYearId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentClassSubject_SubjectId_AcademicLevelId",
                table: "StudentClassSubject",
                columns: new[] { "SubjectId", "AcademicLevelId" });

            migrationBuilder.CreateIndex(
                name: "IX_Subject_CreatedByUserId",
                table: "Subject",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_ParentBasketSubjectId",
                table: "Subject",
                column: "ParentBasketSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_SubjectStreamId",
                table: "Subject",
                column: "SubjectStreamId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_UpdatedByUserId",
                table: "Subject",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAcademicLevel_AcademicLevelId",
                table: "SubjectAcademicLevel",
                column: "AcademicLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_AcademicLevelId",
                table: "SubjectTeacher",
                column: "AcademicLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_AcademicYearId",
                table: "SubjectTeacher",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_CreatedByUserId",
                table: "SubjectTeacher",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_SubjectId",
                table: "SubjectTeacher",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_TeacherId",
                table: "SubjectTeacher",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_UpdatedByUserId",
                table: "SubjectTeacher",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_CreatedByUserId",
                table: "Topic",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_UpdatedByUserId",
                table: "Topic",
                column: "UpdatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassSubjectTeacher");

            migrationBuilder.DropTable(
                name: "ClassTeacher");

            migrationBuilder.DropTable(
                name: "EssayQuestion");

            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.DropTable(
                name: "HeadOfDepartment");

            migrationBuilder.DropTable(
                name: "MCQQuestions");

            migrationBuilder.DropTable(
                name: "StructuredQuestion");

            migrationBuilder.DropTable(
                name: "StudentClassSubject");

            migrationBuilder.DropTable(
                name: "ExamType");

            migrationBuilder.DropTable(
                name: "SubjectTeacher");

            migrationBuilder.DropTable(
                name: "Assessment");

            migrationBuilder.DropTable(
                name: "StudentClass");

            migrationBuilder.DropTable(
                name: "SubjectAcademicLevel");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "AcademicLevel");

            migrationBuilder.DropTable(
                name: "AcademicYear");

            migrationBuilder.DropTable(
                name: "ClassName");

            migrationBuilder.DropTable(
                name: "SubjectStream");
        }
    }
}

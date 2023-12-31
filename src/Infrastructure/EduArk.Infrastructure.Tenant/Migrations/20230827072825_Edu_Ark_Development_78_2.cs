using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduArk.Infrastructure.Tenant.Migrations
{
    /// <inheritdoc />
    public partial class Edu_Ark_Development_78_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTargetSetting_Subject_StudentId",
                table: "SubjectTargetSetting");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTargetSetting_SubjectId",
                table: "SubjectTargetSetting",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTargetSetting_Subject_SubjectId",
                table: "SubjectTargetSetting",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTargetSetting_Subject_SubjectId",
                table: "SubjectTargetSetting");

            migrationBuilder.DropIndex(
                name: "IX_SubjectTargetSetting_SubjectId",
                table: "SubjectTargetSetting");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTargetSetting_Subject_StudentId",
                table: "SubjectTargetSetting",
                column: "StudentId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

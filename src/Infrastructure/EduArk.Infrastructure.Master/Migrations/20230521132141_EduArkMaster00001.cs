using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduArk.Infrastructure.Master.Migrations
{
    /// <inheritdoc />
    public partial class EduArkMaster00001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSetting",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSetting", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "MasterUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SMTPServer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMTPUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMTPPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMTPFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMTPPort = table.Column<int>(type: "int", nullable: true),
                    IsSMTPUseSSL = table.Column<bool>(type: "bit", nullable: true),
                    IsGovernmentSchool = table.Column<bool>(type: "bit", nullable: false),
                    SpecialNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("31a83201-e4ca-4cf3-a20f-81a5dac21d9b")),
                    APIKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("ed7e3751-9819-439e-8515-7d2258ad9273")),
                    SecretKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("695ba875-da00-49a7-919d-fb66754571da")),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSetting");

            migrationBuilder.DropTable(
                name: "MasterUser");

            migrationBuilder.DropTable(
                name: "Tenant");
        }
    }
}

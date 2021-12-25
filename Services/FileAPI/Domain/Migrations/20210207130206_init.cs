using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileAPI.Domain.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    Extention = table.Column<string>(type: "text", nullable: true),
                    Size = table.Column<double>(type: "double precision", nullable: false),
                    FileCategory = table.Column<int>(type: "integer", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UploaderUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfilePictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileInformaionId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileInformationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfilePictures_FileInformation_FileInformationId",
                        column: x => x.FileInformationId,
                        principalTable: "FileInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePictures_FileInformationId",
                table: "ProfilePictures",
                column: "FileInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfilePictures");

            migrationBuilder.DropTable(
                name: "FileInformation");
        }
    }
}

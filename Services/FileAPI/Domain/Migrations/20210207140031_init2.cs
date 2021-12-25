using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileAPI.Domain.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfilePictures_FileInformation_FileInformationId",
                table: "ProfilePictures");

            migrationBuilder.DropColumn(
                name: "FileInformaionId",
                table: "ProfilePictures");

            migrationBuilder.AlterColumn<Guid>(
                name: "FileInformationId",
                table: "ProfilePictures",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfilePictures_FileInformation_FileInformationId",
                table: "ProfilePictures",
                column: "FileInformationId",
                principalTable: "FileInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfilePictures_FileInformation_FileInformationId",
                table: "ProfilePictures");

            migrationBuilder.AlterColumn<Guid>(
                name: "FileInformationId",
                table: "ProfilePictures",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "FileInformaionId",
                table: "ProfilePictures",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_ProfilePictures_FileInformation_FileInformationId",
                table: "ProfilePictures",
                column: "FileInformationId",
                principalTable: "FileInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

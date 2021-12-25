using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityApi.Domain.Migrations
{
    public partial class remove_RolesFromUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_IdentityUserId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_IdentityUserId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PhoneNumberVerifyCodes");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "PhoneNumberVerifyCodes",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "PhoneNumberVerifyCodes");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "PhoneNumberVerifyCodes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdentityUserId",
                table: "AspNetRoles",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_IdentityUserId",
                table: "AspNetRoles",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_IdentityUserId",
                table: "AspNetRoles",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

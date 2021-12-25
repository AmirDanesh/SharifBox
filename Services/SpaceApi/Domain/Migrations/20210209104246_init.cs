using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceApi.Domain.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    UserFullName = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<byte>(type: "smallint", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    SvgId = table.Column<string>(type: "text", nullable: true),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spaces_Spaces_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Spaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RentAbleConferenceRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Area = table.Column<double>(type: "double precision", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    NumOfChairs = table.Column<int>(type: "integer", nullable: false),
                    NumOfVideoProjection = table.Column<int>(type: "integer", nullable: false),
                    NumOfMicrophone = table.Column<int>(type: "integer", nullable: false),
                    SpaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentAbleConferenceRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentAbleConferenceRooms_Spaces_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "Spaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentAbleRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Area = table.Column<double>(type: "double precision", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    NumOfChairs = table.Column<int>(type: "integer", nullable: false),
                    SpaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentAbleRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentAbleRooms_Spaces_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "Spaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentAblesChairs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SpaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentAblesChairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentAblesChairs_Spaces_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "Spaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    SvgId = table.Column<string>(type: "text", nullable: true),
                    SpaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsFinalized = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Spaces_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "Spaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentAbleConferenceRooms_SpaceId",
                table: "RentAbleConferenceRooms",
                column: "SpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_RentAbleRooms_SpaceId",
                table: "RentAbleRooms",
                column: "SpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_RentAblesChairs_SpaceId",
                table: "RentAblesChairs",
                column: "SpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PaymentId",
                table: "Reservations",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SpaceId",
                table: "Reservations",
                column: "SpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_ParentId",
                table: "Spaces",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentAbleConferenceRooms");

            migrationBuilder.DropTable(
                name: "RentAbleRooms");

            migrationBuilder.DropTable(
                name: "RentAblesChairs");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Spaces");
        }
    }
}

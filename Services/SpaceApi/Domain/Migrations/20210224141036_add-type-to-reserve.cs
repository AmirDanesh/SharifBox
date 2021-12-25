using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceApi.Domain.Migrations
{
    public partial class addtypetoreserve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RentAblesChairs_SpaceId",
                table: "RentAblesChairs");

            migrationBuilder.DropIndex(
                name: "IX_RentAbleRooms_SpaceId",
                table: "RentAbleRooms");

            migrationBuilder.DropIndex(
                name: "IX_RentAbleConferenceRooms_SpaceId",
                table: "RentAbleConferenceRooms");

            migrationBuilder.AddColumn<byte>(
                name: "Type",
                table: "Reservations",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_RentAblesChairs_SpaceId",
                table: "RentAblesChairs",
                column: "SpaceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentAbleRooms_SpaceId",
                table: "RentAbleRooms",
                column: "SpaceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentAbleConferenceRooms_SpaceId",
                table: "RentAbleConferenceRooms",
                column: "SpaceId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RentAblesChairs_SpaceId",
                table: "RentAblesChairs");

            migrationBuilder.DropIndex(
                name: "IX_RentAbleRooms_SpaceId",
                table: "RentAbleRooms");

            migrationBuilder.DropIndex(
                name: "IX_RentAbleConferenceRooms_SpaceId",
                table: "RentAbleConferenceRooms");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Reservations");

            migrationBuilder.CreateIndex(
                name: "IX_RentAblesChairs_SpaceId",
                table: "RentAblesChairs",
                column: "SpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_RentAbleRooms_SpaceId",
                table: "RentAbleRooms",
                column: "SpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_RentAbleConferenceRooms_SpaceId",
                table: "RentAbleConferenceRooms",
                column: "SpaceId");
        }
    }
}

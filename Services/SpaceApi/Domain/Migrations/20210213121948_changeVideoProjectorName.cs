using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceApi.Domain.Migrations
{
    public partial class changeVideoProjectorName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumOfVideoProjection",
                table: "RentAbleConferenceRooms",
                newName: "NumOfVideoProjector");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumOfVideoProjector",
                table: "RentAbleConferenceRooms",
                newName: "NumOfVideoProjection");
        }
    }
}

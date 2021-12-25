using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceApi.Domain.Migrations
{
    public partial class add_Amount_ReserveTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PayAmount",
                table: "Reservations",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayAmount",
                table: "Reservations");
        }
    }
}

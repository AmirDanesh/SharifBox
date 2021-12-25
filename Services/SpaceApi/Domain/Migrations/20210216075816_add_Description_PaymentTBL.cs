using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceApi.Domain.Migrations
{
    public partial class add_Description_PaymentTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Payment",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Payment");
        }
    }
}

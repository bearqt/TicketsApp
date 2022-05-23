using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketsApp.Migrations
{
    public partial class AddSerialNumberConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SerialNumber",
                table: "Segments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Segments_TicketNumber_SerialNumber",
                table: "Segments",
                columns: new[] { "TicketNumber", "SerialNumber" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Segments_TicketNumber_SerialNumber",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Segments");
        }
    }
}

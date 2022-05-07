using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TicketsApp.Migrations
{
    public partial class DateNameFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Segments",
                columns: table => new
                {
                    SegmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OperationType = table.Column<string>(type: "text", nullable: true),
                    OperationTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    OperationPlace = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Patronymic = table.Column<string>(type: "text", nullable: true),
                    DocType = table.Column<int>(type: "integer", nullable: false),
                    DocNumber = table.Column<long>(type: "bigint", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    PassengerType = table.Column<string>(type: "text", nullable: true),
                    TicketNumber = table.Column<long>(type: "bigint", nullable: false),
                    TicketType = table.Column<int>(type: "integer", nullable: false),
                    AirlineCode = table.Column<string>(type: "text", nullable: true),
                    FlightNum = table.Column<int>(type: "integer", nullable: false),
                    DepartPlace = table.Column<string>(type: "text", nullable: true),
                    DepartDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ArrivePlace = table.Column<string>(type: "text", nullable: true),
                    ArriveDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    PnrId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segments", x => x.SegmentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Segments");
        }
    }
}

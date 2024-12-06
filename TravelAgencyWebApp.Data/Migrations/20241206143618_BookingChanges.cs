using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class BookingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 31, 16, 36, 16, 880, DateTimeKind.Local).AddTicks(5035), new DateTime(2025, 1, 10, 16, 36, 16, 880, DateTimeKind.Local).AddTicks(5091) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 26, 16, 36, 16, 880, DateTimeKind.Local).AddTicks(5096), new DateTime(2024, 12, 31, 16, 36, 16, 880, DateTimeKind.Local).AddTicks(5098) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 11, 16, 36, 16, 880, DateTimeKind.Local).AddTicks(5102), new DateTime(2024, 12, 21, 16, 36, 16, 880, DateTimeKind.Local).AddTicks(5104) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 16, 16, 36, 16, 880, DateTimeKind.Local).AddTicks(5107), new DateTime(2024, 12, 23, 16, 36, 16, 880, DateTimeKind.Local).AddTicks(5109) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 20, 16, 36, 16, 880, DateTimeKind.Local).AddTicks(5112), new DateTime(2024, 12, 22, 16, 36, 16, 880, DateTimeKind.Local).AddTicks(5114) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 1, 9, 16, 36, 16, 880, DateTimeKind.Local).AddTicks(5118), new DateTime(2025, 1, 12, 16, 36, 16, 880, DateTimeKind.Local).AddTicks(5120) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 2, 10, 16, 36, 16, 880, DateTimeKind.Local).AddTicks(5123), new DateTime(2025, 2, 18, 16, 36, 16, 880, DateTimeKind.Local).AddTicks(5125) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 2, 10, 16, 36, 16, 880, DateTimeKind.Local).AddTicks(5129), new DateTime(2025, 2, 18, 16, 36, 16, 880, DateTimeKind.Local).AddTicks(5130) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 31, 16, 1, 56, 924, DateTimeKind.Local).AddTicks(2707), new DateTime(2025, 1, 10, 16, 1, 56, 924, DateTimeKind.Local).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 26, 16, 1, 56, 924, DateTimeKind.Local).AddTicks(2755), new DateTime(2024, 12, 31, 16, 1, 56, 924, DateTimeKind.Local).AddTicks(2758) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 11, 16, 1, 56, 924, DateTimeKind.Local).AddTicks(2762), new DateTime(2024, 12, 21, 16, 1, 56, 924, DateTimeKind.Local).AddTicks(2764) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 16, 16, 1, 56, 924, DateTimeKind.Local).AddTicks(2767), new DateTime(2024, 12, 23, 16, 1, 56, 924, DateTimeKind.Local).AddTicks(2769) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 20, 16, 1, 56, 924, DateTimeKind.Local).AddTicks(2773), new DateTime(2024, 12, 22, 16, 1, 56, 924, DateTimeKind.Local).AddTicks(2775) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 1, 9, 16, 1, 56, 924, DateTimeKind.Local).AddTicks(2778), new DateTime(2025, 1, 12, 16, 1, 56, 924, DateTimeKind.Local).AddTicks(2781) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 2, 10, 16, 1, 56, 924, DateTimeKind.Local).AddTicks(2784), new DateTime(2025, 2, 18, 16, 1, 56, 924, DateTimeKind.Local).AddTicks(2786) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 2, 10, 16, 1, 56, 924, DateTimeKind.Local).AddTicks(2790), new DateTime(2025, 2, 18, 16, 1, 56, 924, DateTimeKind.Local).AddTicks(2792) });
        }
    }
}

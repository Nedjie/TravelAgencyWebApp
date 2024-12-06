using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangingBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: true,
                comment: "User identifier",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "User identifier");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "User identifier",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "User identifier");

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 31, 15, 46, 15, 577, DateTimeKind.Local).AddTicks(3627), new DateTime(2025, 1, 10, 15, 46, 15, 577, DateTimeKind.Local).AddTicks(3678) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 26, 15, 46, 15, 577, DateTimeKind.Local).AddTicks(3684), new DateTime(2024, 12, 31, 15, 46, 15, 577, DateTimeKind.Local).AddTicks(3686) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 11, 15, 46, 15, 577, DateTimeKind.Local).AddTicks(3690), new DateTime(2024, 12, 21, 15, 46, 15, 577, DateTimeKind.Local).AddTicks(3692) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 16, 15, 46, 15, 577, DateTimeKind.Local).AddTicks(3695), new DateTime(2024, 12, 23, 15, 46, 15, 577, DateTimeKind.Local).AddTicks(3697) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 20, 15, 46, 15, 577, DateTimeKind.Local).AddTicks(3701), new DateTime(2024, 12, 22, 15, 46, 15, 577, DateTimeKind.Local).AddTicks(3703) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 1, 9, 15, 46, 15, 577, DateTimeKind.Local).AddTicks(3706), new DateTime(2025, 1, 12, 15, 46, 15, 577, DateTimeKind.Local).AddTicks(3708) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 2, 10, 15, 46, 15, 577, DateTimeKind.Local).AddTicks(3711), new DateTime(2025, 2, 18, 15, 46, 15, 577, DateTimeKind.Local).AddTicks(3713) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 2, 10, 15, 46, 15, 577, DateTimeKind.Local).AddTicks(3717), new DateTime(2025, 2, 18, 15, 46, 15, 577, DateTimeKind.Local).AddTicks(3719) });
        }
    }
}

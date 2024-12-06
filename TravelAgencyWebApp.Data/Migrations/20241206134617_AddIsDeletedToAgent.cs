using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToAgent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Agents",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Agents");

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 30, 21, 28, 37, 736, DateTimeKind.Local).AddTicks(6897), new DateTime(2025, 1, 9, 21, 28, 37, 736, DateTimeKind.Local).AddTicks(6941) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 25, 21, 28, 37, 736, DateTimeKind.Local).AddTicks(6946), new DateTime(2024, 12, 30, 21, 28, 37, 736, DateTimeKind.Local).AddTicks(6949) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 10, 21, 28, 37, 736, DateTimeKind.Local).AddTicks(6952), new DateTime(2024, 12, 20, 21, 28, 37, 736, DateTimeKind.Local).AddTicks(6954) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 15, 21, 28, 37, 736, DateTimeKind.Local).AddTicks(6957), new DateTime(2024, 12, 22, 21, 28, 37, 736, DateTimeKind.Local).AddTicks(6959) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 19, 21, 28, 37, 736, DateTimeKind.Local).AddTicks(6962), new DateTime(2024, 12, 21, 21, 28, 37, 736, DateTimeKind.Local).AddTicks(6964) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 1, 8, 21, 28, 37, 736, DateTimeKind.Local).AddTicks(6968), new DateTime(2025, 1, 11, 21, 28, 37, 736, DateTimeKind.Local).AddTicks(6969) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 2, 9, 21, 28, 37, 736, DateTimeKind.Local).AddTicks(6973), new DateTime(2025, 2, 17, 21, 28, 37, 736, DateTimeKind.Local).AddTicks(6975) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 2, 9, 21, 28, 37, 736, DateTimeKind.Local).AddTicks(6978), new DateTime(2025, 2, 17, 21, 28, 37, 736, DateTimeKind.Local).AddTicks(6980) });
        }
    }
}

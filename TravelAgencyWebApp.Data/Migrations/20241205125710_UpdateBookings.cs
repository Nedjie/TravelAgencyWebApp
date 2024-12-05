using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Agents_AgentId",
                table: "Bookings");

            migrationBuilder.AlterColumn<Guid>(
                name: "AgentId",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Agent identifier",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckInDate", "CheckOutDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 12, 30, 14, 57, 9, 62, DateTimeKind.Local).AddTicks(9572), new DateTime(2025, 1, 9, 14, 57, 9, 62, DateTimeKind.Local).AddTicks(9632), "/Content/images/dominicana.jpg" });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CheckInDate", "CheckOutDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 12, 25, 14, 57, 9, 62, DateTimeKind.Local).AddTicks(9655), new DateTime(2024, 12, 30, 14, 57, 9, 62, DateTimeKind.Local).AddTicks(9672), "/Content/images/dubai.jpg" });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 10, 14, 57, 9, 62, DateTimeKind.Local).AddTicks(9691), new DateTime(2024, 12, 20, 14, 57, 9, 62, DateTimeKind.Local).AddTicks(9707) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 15, 14, 57, 9, 62, DateTimeKind.Local).AddTicks(9727), new DateTime(2024, 12, 22, 14, 57, 9, 62, DateTimeKind.Local).AddTicks(9743) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 19, 14, 57, 9, 62, DateTimeKind.Local).AddTicks(9762), new DateTime(2024, 12, 21, 14, 57, 9, 62, DateTimeKind.Local).AddTicks(9778) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 1, 8, 14, 57, 9, 62, DateTimeKind.Local).AddTicks(9797), new DateTime(2025, 1, 11, 14, 57, 9, 62, DateTimeKind.Local).AddTicks(9814) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 2, 9, 14, 57, 9, 62, DateTimeKind.Local).AddTicks(9833), new DateTime(2025, 2, 17, 14, 57, 9, 62, DateTimeKind.Local).AddTicks(9849) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 2, 9, 14, 57, 9, 62, DateTimeKind.Local).AddTicks(9868), new DateTime(2025, 2, 17, 14, 57, 9, 62, DateTimeKind.Local).AddTicks(9884) });

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Agents_AgentId",
                table: "Bookings",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Agents_AgentId",
                table: "Bookings");

            migrationBuilder.AlterColumn<Guid>(
                name: "AgentId",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Agent identifier");

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckInDate", "CheckOutDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 12, 28, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5915), new DateTime(2025, 1, 7, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5959), "/Content.images/dominicana.jpg" });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CheckInDate", "CheckOutDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 12, 23, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5964), new DateTime(2024, 12, 28, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5967), "/Content.images/dubai.jpg" });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 8, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5971), new DateTime(2024, 12, 18, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5973) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 13, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5977), new DateTime(2024, 12, 20, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5979) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 17, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5983), new DateTime(2024, 12, 19, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5985) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 1, 6, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5989), new DateTime(2025, 1, 9, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5992) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 2, 7, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5995), new DateTime(2025, 2, 15, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5998) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 2, 7, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(6001), new DateTime(2025, 2, 15, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(6004) });

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Agents_AgentId",
                table: "Bookings",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id");
        }
    }
}

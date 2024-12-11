using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 1, 5, 20, 14, 34, 250, DateTimeKind.Local).AddTicks(4967), new DateTime(2025, 1, 15, 20, 14, 34, 257, DateTimeKind.Local).AddTicks(732) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 31, 20, 14, 34, 257, DateTimeKind.Local).AddTicks(3331), new DateTime(2025, 1, 5, 20, 14, 34, 257, DateTimeKind.Local).AddTicks(3347) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 16, 20, 14, 34, 257, DateTimeKind.Local).AddTicks(3353), new DateTime(2024, 12, 26, 20, 14, 34, 257, DateTimeKind.Local).AddTicks(3357) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 21, 20, 14, 34, 257, DateTimeKind.Local).AddTicks(3362), new DateTime(2024, 12, 28, 20, 14, 34, 257, DateTimeKind.Local).AddTicks(3366) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 12, 25, 20, 14, 34, 257, DateTimeKind.Local).AddTicks(3371), new DateTime(2024, 12, 27, 20, 14, 34, 257, DateTimeKind.Local).AddTicks(3375) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CheckInDate", "CheckOutDate", "Description" },
                values: new object[] { new DateTime(2025, 1, 14, 20, 14, 34, 257, DateTimeKind.Local).AddTicks(3379), new DateTime(2025, 1, 17, 20, 14, 34, 257, DateTimeKind.Local).AddTicks(3383), "ТранспортБагаж до 20 кг и 1 малък ръчен багаж с размери 40 х 30 х 20 см;3 нощувки със закуски;Обиколен тур на Рим с екскурзовод на български език;Медицинска застраховка Помощ при пътуване от Евронинс с покритие 10000 евроПредставител на туроператора на български език." });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 2, 15, 20, 14, 34, 257, DateTimeKind.Local).AddTicks(3388), new DateTime(2025, 2, 23, 20, 14, 34, 257, DateTimeKind.Local).AddTicks(3391) });

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2025, 2, 15, 20, 14, 34, 257, DateTimeKind.Local).AddTicks(3396), new DateTime(2025, 2, 23, 20, 14, 34, 257, DateTimeKind.Local).AddTicks(3400) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "CheckInDate", "CheckOutDate", "Description" },
                values: new object[] { new DateTime(2025, 1, 9, 16, 36, 16, 880, DateTimeKind.Local).AddTicks(5118), new DateTime(2025, 1, 12, 16, 36, 16, 880, DateTimeKind.Local).AddTicks(5120), "Самолетен билет Варна – Перуджа – Варна с авиокомпания European Air Charter;Летищни такси;Чекиран багаж до 20 кг и 1 малък ръчен багаж с размери 40 х 30 х 20 см;Трансфер летище – хотел – летище с автобус;3 нощувки със закуски;Обиколен тур на Рим с екскурзовод на български език;Медицинска застраховка Помощ при пътуване от Евронинс с покритие 10000 евроПредставител на туроператора на български език." });

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
    }
}

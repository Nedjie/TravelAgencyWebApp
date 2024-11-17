using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelAgencyWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingTravelingWays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TravelingWays",
                columns: new[] { "Id", "Cost", "Description", "Method" },
                values: new object[,]
                {
                    { 1, 0m, "Travel by airplane.", "Air" },
                    { 2, 0m, "Travel by train.", "Train" },
                    { 3, 0m, "Travel by bus.", "Bus" },
                    { 4, 0m, "Travel by car.", "Car" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TravelingWays",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TravelingWays",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TravelingWays",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TravelingWays",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}

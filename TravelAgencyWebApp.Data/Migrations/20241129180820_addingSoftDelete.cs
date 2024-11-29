using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Offers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Is this offer is deleted");

            migrationBuilder.UpdateData(
                table: "TravelingWays",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Method" },
                values: new object[] { "Пътуване със самолет", "Самолет" });

            migrationBuilder.UpdateData(
                table: "TravelingWays",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Method" },
                values: new object[] { "Пътуване със влак", "Влак" });

            migrationBuilder.UpdateData(
                table: "TravelingWays",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Method" },
                values: new object[] { "Пътуване с автобус", "Автобус" });

            migrationBuilder.UpdateData(
                table: "TravelingWays",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Method" },
                values: new object[] { "Пътуване с кола", "Кола" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Offers");

            migrationBuilder.UpdateData(
                table: "TravelingWays",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Method" },
                values: new object[] { "Travel by airplane.", "Air" });

            migrationBuilder.UpdateData(
                table: "TravelingWays",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Method" },
                values: new object[] { "Travel by train.", "Train" });

            migrationBuilder.UpdateData(
                table: "TravelingWays",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Method" },
                values: new object[] { "Travel by bus.", "Bus" });

            migrationBuilder.UpdateData(
                table: "TravelingWays",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Method" },
                values: new object[] { "Travel by car.", "Car" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAgent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AgentId",
                table: "Offers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AgentId",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Agent identifier"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Agent Full Name"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Agent email address")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_AgentId",
                table: "Offers",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AgentId",
                table: "Bookings",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Agents_AgentId",
                table: "Bookings",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Agents_AgentId",
                table: "Offers",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Agents_AgentId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Agents_AgentId",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropIndex(
                name: "IX_Offers_AgentId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_AgentId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Bookings");
        }
    }
}

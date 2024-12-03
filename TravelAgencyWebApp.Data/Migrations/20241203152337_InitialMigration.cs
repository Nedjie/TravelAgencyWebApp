using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelAgencyWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TravelingWays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Traveling way identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Method = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Traveling way method"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Traveling way description"),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Traveling way cost")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelingWays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Offer identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Offer title"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Offer price"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Offer description"),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Check in date of booking"),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Check out date of booking"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Offer image"),
                    TravelingWayId = table.Column<int>(type: "int", nullable: false, comment: "Traveling way identifier"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is this offer is deleted"),
                    AgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Offers_TravelingWays_TravelingWayId",
                        column: x => x.TravelingWayId,
                        principalTable: "TravelingWays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Booking identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "User identifier"),
                    OfferId = table.Column<int>(type: "int", nullable: false, comment: "Offer identifier"),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Check in date of booking"),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Check out date of booking"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TravelingWays",
                columns: new[] { "Id", "Cost", "Description", "Method" },
                values: new object[,]
                {
                    { 1, 0m, "Пътуване със самолет", "Самолет" },
                    { 2, 0m, "Пътуване със круизен кораб", "Круиз" },
                    { 3, 0m, "Пътуване с автобус", "Автобус" },
                    { 4, 0m, "Пътуване с кола", "Кола" }
                });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "AgentId", "CheckInDate", "CheckOutDate", "Description", "ImageUrl", "IsDeleted", "Price", "Title", "TravelingWayId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 12, 28, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5915), new DateTime(2025, 1, 7, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5959), "Самолетен билет Мадрид - Пунта Кана - Мадрид;7 нощувки на база All Inclusive в хотел по избор в Плая Баваро;Трансфери летище Пунта Кана – хотел – летище Пунта Кана;Представител на български език от фирма - партньор на място.", "/Content.images/dominicana.jpg", false, 2240.00m, "Почивка в Доминикана", 1 },
                    { 2, null, new DateTime(2024, 12, 23, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5964), new DateTime(2024, 12, 28, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5967), "Дубай -  смайващ лукс, високотехнологични съоръжения и целогодишно слънце, в съчетание с уникален допир до арабската култура. Известен в миналото като „град на търговците\", Дубай от векове посреща морски пътешественици, търговци и туристи по своите крайбрежия, превръщайки се в една от най-популярните дестинации за релаксираща почивка, авантюристична разходка в пустинята или бурен нощен живот. Подарете си релакс съчетан с лукс!", "/Content.images/dubai.jpg", false, 1622.17m, "Почивка в Дубай", 1 },
                    { 3, null, new DateTime(2024, 12, 8, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5971), new DateTime(2024, 12, 18, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5973), "Самолетен билет София - Истанбул - Пукет - Истанбул - София с включени летищни такси;Чекиран багаж до 23 кг.и ръчен багаж до 8 кг.;7 нощувки в избрания хотел на съответната база изхранване;Трансфер летище - хотел - летище;Медицинска застраховка с покритие 10 000 евро;", "/Content/images/tailand.jpg", false, 2523.00m, "Почивка на о-в Пукет, Тайланд", 1 },
                    { 4, null, new DateTime(2024, 12, 13, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5977), new DateTime(2024, 12, 20, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5979), "Kогато камбани зазвънят из целия град , уличките се изпълнят с тържествения марш на духови оркестри , а от балконите залетят червени делви -Корфу ще грабне душата ви от пръв поглед на най-християнския празник !", "/Content/images/korfu.jpg", false, 570.00m, "Великден на Остров Корфу", 3 },
                    { 5, null, new DateTime(2024, 12, 17, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5983), new DateTime(2024, 12, 19, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5985), "2 нощувки със закуски в хотел 3*** в Будапеща.Водач от фирмата по време на пътуванетоАвтобусен транспорт от София с лицензиран автобус за международни превозиМедицинска застраховка за лица до 65г.на застрахователна компания Уника с лимит на отговорност 2000 евро", "/Content/images/budapest.jpg", false, 365.00m, "Екскурзия до Будапеща и Виена - Аристократизъм и Барок", 3 },
                    { 6, null, new DateTime(2025, 1, 6, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5989), new DateTime(2025, 1, 9, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5992), "Самолетен билет Варна – Перуджа – Варна с авиокомпания European Air Charter;Летищни такси;Чекиран багаж до 20 кг и 1 малък ръчен багаж с размери 40 х 30 х 20 см;Трансфер летище – хотел – летище с автобус;3 нощувки със закуски;Обиколен тур на Рим с екскурзовод на български език;Медицинска застраховка Помощ при пътуване от Евронинс с покритие 10000 евроПредставител на туроператора на български език.", "/Content/images/rome.jpg", false, 799.00m, "Рим - Вечният град - 3 нощувки - чартърен полет от Варна", 3 },
                    { 7, null, new DateTime(2025, 2, 7, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5995), new DateTime(2025, 2, 15, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(5998), "Самолетен билет с авиокомпания \"България Еър\" и „ИТА”;Летищни такси;1 бр.ръчен багаж до 10 кг;1 бр.чекиран багаж до 23 кг;Трансфер летище Фиумичино – хотел в Рим;Трансфер хотел в Рим – пристанище Чивитавекия;Трансфер пристанище Чивитавекия – летищe Фиумичино;1 нощувка със закуска в тризвезден хотел в Рим;7 нощувки на база обогатен пълен пансион с круизен кораб MSC Seaview - богат асортимент от храна за закуска,обяд, следобедна закуска и вечеря + вода от диспенсър и чай в зоната на бюфет ресторанта;Безплатно ползване на басейните и фитнес центъра на кораба;Множество забавления на борда на кораба;Програма с атрактивни игри;Пристанищни такси;Водач – придружител от туроператора.", "/Content/images/msc.jpg", false, 2826.00m, "Круиз Средиземноморска приказка - MSC Seaview - 2025", 2 },
                    { 8, null, new DateTime(2025, 2, 7, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(6001), new DateTime(2025, 2, 15, 17, 23, 35, 586, DateTimeKind.Local).AddTicks(6004), "Основен ресторант, отопляем открит басейн 210 кв. м., тенис на корт, шахмат, 6 бара (лоби бар, Риксос бар, Тропик бар, Калина бар, бар на плажа, бар при басейна), СПА център, магазини, мини маркет, аптека,детегледачка(заплаща се)Безплатни услуги: турска баня,сауна,дартс,фитнес център, минибар,осветление на тенис корта ", "/Content/images/rixos.jpg", false, 1400.00m, "Лара, Турция собствен транспорт - RIXOS DOWNTOWN 5*", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AgentId",
                table: "Bookings",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_OfferId",
                table: "Bookings",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_AgentId",
                table: "Offers",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_TravelingWayId",
                table: "Offers",
                column: "TravelingWayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "TravelingWays");
        }
    }
}

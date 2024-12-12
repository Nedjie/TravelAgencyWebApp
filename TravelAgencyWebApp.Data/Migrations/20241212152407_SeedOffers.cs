using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelAgencyWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "AgentId", "CheckInDate", "CheckOutDate", "Description", "ImageUrl", "IsDeleted", "Price", "Title", "TravelingWayId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Самолетен билет Мадрид - Пунта Кана - Мадрид;7 нощувки на база All Inclusive в хотел по избор в Плая Баваро;Трансфери летище Пунта Кана – хотел – летище Пунта Кана;Представител на български език от фирма - партньор на място.", "/Content/images/dominicana.jpg", false, 2240.00m, "Почивка в Доминикана", 1 },
                    { 2, null, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дубай -  смайващ лукс, високотехнологични съоръжения и целогодишно слънце, в съчетание с уникален допир до арабската култура. Известен в миналото като „град на търговците\", Дубай от векове посреща морски пътешественици, търговци и туристи по своите крайбрежия, превръщайки се в една от най-популярните дестинации за релаксираща почивка, авантюристична разходка в пустинята или бурен нощен живот. Подарете си релакс съчетан с лукс!", "/Content/images/dubai.jpg", false, 1622.17m, "Почивка в Дубай", 1 },
                    { 3, null, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Самолетен билет София - Истанбул - Пукет - Истанбул - София с включени летищни такси;Чекиран багаж до 23 кг.и ръчен багаж до 8 кг.;7 нощувки в избрания хотел на съответната база изхранване;Трансфер летище - хотел - летище;Медицинска застраховка с покритие 10 000 евро;", "/Content/images/tailand.jpg", false, 2523.00m, "Почивка на о-в Пукет, Тайланд", 1 },
                    { 4, null, new DateTime(2025, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kогато камбани зазвънят из целия град , уличките се изпълнят с тържествения марш на духови оркестри , а от балконите залетят червени делви -Корфу ще грабне душата ви от пръв поглед на най-християнския празник !", "/Content/images/korfu.jpg", false, 570.00m, "Великден на Остров Корфу", 3 },
                    { 5, null, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "2 нощувки със закуски в хотел 3*** в Будапеща.Водач от фирмата по време на пътуванетоАвтобусен транспорт от София с лицензиран автобус за международни превозиМедицинска застраховка за лица до 65г.на застрахователна компания Уника с лимит на отговорност 2000 евро", "/Content/images/budapest.jpg", false, 365.00m, "Екскурзия до Будапеща и Виена - Аристократизъм и Барок", 3 },
                    { 6, null, new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "ТранспортБагаж до 20 кг и 1 малък ръчен багаж с размери 40 х 30 х 20 см;3 нощувки със закуски;Обиколен тур на Рим с екскурзовод на български език;Медицинска застраховка Помощ при пътуване от Евронинс с покритие 10000 евроПредставител на туроператора на български език.", "/Content/images/rome.jpg", false, 799.00m, "Рим - Вечният град - 3 нощувки - чартърен полет от Варна", 3 },
                    { 7, null, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Самолетен билет с авиокомпания \"България Еър\" и „ИТА”;Летищни такси;1 бр.ръчен багаж до 10 кг;1 бр.чекиран багаж до 23 кг;Трансфер летище Фиумичино – хотел в Рим;Трансфер хотел в Рим – пристанище Чивитавекия;Трансфер пристанище Чивитавекия – летищe Фиумичино;1 нощувка със закуска в тризвезден хотел в Рим;7 нощувки на база обогатен пълен пансион с круизен кораб MSC Seaview - богат асортимент от храна за закуска,обяд, следобедна закуска и вечеря + вода от диспенсър и чай в зоната на бюфет ресторанта;Безплатно ползване на басейните и фитнес центъра на кораба;Множество забавления на борда на кораба;Програма с атрактивни игри;Пристанищни такси;Водач – придружител от туроператора.", "/Content/images/msc.jpg", false, 2826.00m, "Круиз Средиземноморска приказка - MSC Seaview - 2025", 2 },
                    { 8, null, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Основен ресторант, отопляем открит басейн 210 кв. м., тенис на корт, шахмат, 6 бара (лоби бар, Риксос бар, Тропик бар, Калина бар, бар на плажа, бар при басейна), СПА център, магазини, мини маркет, аптека,детегледачка(заплаща се)Безплатни услуги: турска баня,сауна,дартс,фитнес център, минибар,осветление на тенис корта ", "/Content/images/rixos.jpg", false, 1400.00m, "Лара, Турция собствен транспорт - RIXOS DOWNTOWN 5*", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using TravelAgencyWebApp.Data.Models;

namespace TravelAgencyWebApp.Data.Seeding
{
    public static class SeedDataOffers
    {
        public static void DataOffers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Offer>().HasData(
               
                 new Offer
                 {
                     Id = 1,
                     Title = "Почивка в Доминикана",
                     Description = "Самолетен билет Мадрид - Пунта Кана - Мадрид;" +
                     "7 нощувки на база All Inclusive в хотел по избор в Плая Баваро;" +
                     "Трансфери летище Пунта Кана – хотел – летище Пунта Кана;" +
                     "Представител на български език от фирма - партньор на място.",
                     Price = 2240.00m,
                     ImageUrl = "/Content/images/dominicana.jpg",
                     CheckInDate = DateTime.Now.AddDays(25),
                     CheckOutDate = DateTime.Now.AddDays(35),
                     TravelingWayId = 1
                 },
                 new Offer
                 {
                     Id = 2,
                     Title = "Почивка в Дубай",
                     Description = "Дубай -  смайващ лукс, високотехнологични съоръжения и целогодишно слънце," +
                    " в съчетание с уникален допир до арабската култура. Известен в миналото като " +
                    "„град на търговците\", Дубай от векове посреща морски пътешественици, търговци " +
                    "и туристи по своите крайбрежия, превръщайки се в една от най-популярните дестинации" +
                    " за релаксираща почивка, авантюристична разходка в пустинята или бурен нощен живот. " +
                    "Подарете си релакс съчетан с лукс!",
                     Price = 1622.17m,
                     ImageUrl = "/Content/images/dubai.jpg",
                     CheckInDate = DateTime.Now.AddDays(20),
                     CheckOutDate = DateTime.Now.AddDays(25),
                     TravelingWayId = 1
                 },
                  new Offer
                  {
                      Id = 3,
                      Title = "Почивка на о-в Пукет, Тайланд",
                      Description = "Самолетен билет София - Истанбул - Пукет - Истанбул - София" +
                      " с включени летищни такси;"+
                      "Чекиран багаж до 23 кг.и ръчен багаж до 8 кг.;"+
                      "7 нощувки в избрания хотел на съответната база изхранване;"+
                      "Трансфер летище - хотел - летище;"+
                      "Медицинска застраховка с покритие 10 000 евро;",
                      Price = 2523.00m,
                      ImageUrl = "/Content/images/tailand.jpg",
                      CheckInDate = DateTime.Now.AddDays(5),
                      CheckOutDate = DateTime.Now.AddDays(15),
                      TravelingWayId = 1
                  },
                  new Offer
                  {
                      Id = 4,
                      Title = "Великден на Остров Корфу",
                      Description = "Kогато камбани зазвънят из целия град ," +
                    " уличките се изпълнят с тържествения марш на духови оркестри ," +
                    " а от балконите залетят червени делви -Корфу ще грабне душата ви от " +
                    "пръв поглед на най-християнския празник !",
                      Price = 570.00m,
                      ImageUrl = "/Content/images/korfu.jpg",
                      CheckInDate = DateTime.Now.AddDays(10),
                      CheckOutDate = DateTime.Now.AddDays(17),
                      TravelingWayId = 3
                  },
                  new Offer
                  {
                      Id = 5,
                      Title = "Екскурзия до Будапеща и Виена - Аристократизъм и Барок",
                      Description = "2 нощувки със закуски в хотел 3*** в Будапеща."+
                      "Водач от фирмата по време на пътуването"+
                      "Автобусен транспорт от София с лицензиран автобус за международни превози"+
                       "Медицинска застраховка за лица до 65г.на застрахователна компания Уника с лимит на отговорност 2000 евро",
                      Price = 365.00m,
                      ImageUrl = "/Content/images/budapest.jpg",
                      CheckInDate = DateTime.Now.AddDays(14),
                      CheckOutDate = DateTime.Now.AddDays(16),
                      TravelingWayId = 3
                  },
                   new Offer
                   {
                       Id = 6,
                       Title = "Рим - Вечният град - 3 нощувки - чартърен полет от Варна",
                       Description = "Транспорт"+                    
                       "Багаж до 20 кг и 1 малък ръчен багаж с размери 40 х 30 х 20 см;"+
                       "3 нощувки със закуски;"+
                       "Обиколен тур на Рим с екскурзовод на български език;"+
                       "Медицинска застраховка Помощ при пътуване от Евронинс с покритие 10000 евро" +                       
                       "Представител на туроператора на български език.",
                       Price = 799.00m,
                       ImageUrl = "/Content/images/rome.jpg",
                       CheckInDate = DateTime.Now.AddDays(34),
                       CheckOutDate = DateTime.Now.AddDays(37),
                       TravelingWayId = 3
                   },
                    new Offer
                    {
                        Id = 7,
                        Title = "Круиз Средиземноморска приказка - MSC Seaview - 2025",
                        Description = "Самолетен билет с авиокомпания \"България Еър\" и „ИТА”;"+ 
                        "Летищни такси;"+
                        "1 бр.ръчен багаж до 10 кг;"+
                        "1 бр.чекиран багаж до 23 кг;" +
                        "Трансфер летище Фиумичино – хотел в Рим;" +
                        "Трансфер хотел в Рим – пристанище Чивитавекия;" +
                        "Трансфер пристанище Чивитавекия – летищe Фиумичино;" +
                        "1 нощувка със закуска в тризвезден хотел в Рим;" +
                        "7 нощувки на база обогатен пълен пансион с круизен кораб MSC Seaview - богат асортимент от храна за закуска," +
                        "обяд, следобедна закуска и вечеря + вода от диспенсър и чай в зоната на бюфет ресторанта;" +
                        "Безплатно ползване на басейните и фитнес центъра на кораба;" +
                        "Множество забавления на борда на кораба;" +
                        "Програма с атрактивни игри;" +
                        "Пристанищни такси;" +
                        "Водач – придружител от туроператора.",
                        Price = 2826.00m,
                        ImageUrl = "/Content/images/msc.jpg",
                        CheckInDate = DateTime.Now.AddDays(66),
                        CheckOutDate = DateTime.Now.AddDays(74),
                        TravelingWayId = 2
                    },
                    new Offer
                    {
                        Id = 8,
                        Title = "Лара, Турция собствен транспорт - RIXOS DOWNTOWN 5*",
                        Description = "Основен ресторант, отопляем открит басейн 210 кв. м.," +
                        " тенис на корт, шахмат, 6 бара (лоби бар, Риксос бар, Тропик бар, Калина" +
                        " бар, бар на плажа, бар при басейна), СПА център, магазини, мини маркет, аптека,детегледачка(заплаща се)" +
                        "Безплатни услуги: турска баня,сауна,дартс,фитнес център, минибар,осветление на тенис корта ",
                        Price = 1400.00m,
                        ImageUrl = "/Content/images/rixos.jpg",
                        CheckInDate = DateTime.Now.AddDays(66),
                        CheckOutDate = DateTime.Now.AddDays(74),
                        TravelingWayId = 4
                    }
            );
        }
    }
}



using BarberMe.Models.Classes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberMe.Models.Database
{
    public class SeedData
    {
        private const string artur = "ArtyryrF@gmail.com";
        private const string user1Password = "Password123!";
        private const string testUser = "Test@gmail.com";
        private const string testPassword = "Password123!";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {

            UserManager<BarbershopUser> userManager = app.ApplicationServices
               .GetRequiredService<UserManager<BarbershopUser>>();

            ApplicationDbContext context = app.ApplicationServices
               .GetRequiredService<ApplicationDbContext>();

            BarbershopUser user = await userManager.FindByEmailAsync(artur);
            if (user == null)
            {
                user = new BarbershopUser
                {
                    Email = artur,
                    UserName = artur,
                };
                await userManager.CreateAsync(user, user1Password);
            }

            BarbershopUser test = await userManager.FindByEmailAsync(testUser);

            if (test == null)
            {
                test = new BarbershopUser
                {
                    Email = testUser,
                    UserName = testUser,
                };
                await userManager.CreateAsync(test, testPassword);
            }

            if (!context.ServiceTypes.Any())
            {
                context.ServiceTypes.AddRange(
                    new ServiceType { ServiceTypeName = "Мужская стрижка", Description = "" },
                    new ServiceType { ServiceTypeName = "Стрижка машинкой", Description = "" },
                    new ServiceType { ServiceTypeName = "Стрижка бороды", Description = "" },
                    new ServiceType { ServiceTypeName = "Традиционное влажное бритье", Description = "" },
                    new ServiceType { ServiceTypeName = "Бритье опасной бритвой", Description = "" },
                    new ServiceType { ServiceTypeName = "Мужская стрижка и стрижка бороды", Description = "" },
                    new ServiceType { ServiceTypeName = "Укладка волос", Description = "" },
                    new ServiceType { ServiceTypeName = "Детская стрижка", Description = "" },
                    new ServiceType { ServiceTypeName = "Мужская и детская стрижики", Description = "" });
                context.SaveChanges();
            }

            if (!context.Barbershops.Any())
            {
                BarbershopUser user1 = await userManager.FindByEmailAsync(artur);
                BarbershopUser user2 = await userManager.FindByEmailAsync(testUser);

                context.Barbershops.AddRange(
                    new Barbershop(user1.Id, "MannBarbershop@gmail.com", "Mann Barbershop", "ул. Гиршмана 17б", "+38 050 847 3000", "Mann Barbershop – " +
                    "это не просто мужская парикмахерская. Мы создали действительно качественное, " +
                    "крутое и брутальное место. Наши барберы — истинные профессионалы, потому что постоянно " +
                    "повышают свою квалификацию по технике мужских стрижек у лучших мастеров. " +
                    "В нашем барбершопе Вы сможете сделать не только стрижку головы, мы так же стильно оформим " +
                    "Вашу бороду и усы. Барбершоп сделан в стиле loft, который буквально пропитан мужественной атмосферой."
                    , "https://www.instagram.com/explore/locations/1013775316/mann-barbershop/?hl=ru",
                    "https://ru-ru.facebook.com/mannbarber.kh/",
                    "<iframe src=\"https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2564.574341307052!2d36.23783211601041!3d50.00058937941578!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x4127a0e7cfe3aac3%3A0xec9c8f2334a699c!2sMANN+Barbershop%26Bar!5e0!3m2!1sru!2sua!4v1558524333865!5m2!1sru!2sua\" width=\"600\" height=\"450\" frameborder=\"0\" style=\"border:0\" allowfullscreen></iframe>", 
                    "1.jpg"),

                    new Barbershop(user2.Id, "ChopChop@gmail.com", "Chop-Chop", "ул. Маршала Бажанова, д. 4", "+380 95 276 44 22", "Chop - Chop — это больше, " +
                    "чем мужская парикмахерская и уж точно не салон красоты.Здесь работают мастера своего дела, знающие, как из мельчайших" +
                    " деталей собирается образ настоящей мужественности.Пользуясь лучшей мужской косметикой, которую мы закупаем в самых " +
                    "разных уголках света, мы чтем трехсотлетние традиции классической стрижки и ухода за бородой, ну и конечно же, королевского бритья."
                    , "https://www.instagram.com/explore/locations/215009539/chop-chop-kharkov/?hl=en",
                    "https://www.facebook.com/chopchopkharkov",
                    "<iframe src=\"https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2564.7521282601383!2d36.238019316010515!3d49.99725647941538!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x4127a0e8c949ae4f%3A0xd650176dcae8193c!2z0JHQsNGA0LHQtdGA0YjQvtC_IENob3AtQ2hvcA!5e0!3m2!1sru!2sua!4v1558524717972!5m2!1sru!2sua\" width=\"600\" height=\"450\" frameborder=\"0\" style=\"border:0\" allowfullscreen></iframe>", 
                    "2.jpg"));
                context.SaveChanges();
            }

            if (!context.Services.Any())
            {
                context.Services.AddRange(
                    new Service
                    {
                        BarbershopId = 1,
                        ServiceName = "МУЖСКАЯ СТРИЖКА",
                        ServiceDescription = "",
                        ServicePrice = 350,
                        ServiceDuration = 60,
                        ServiceTypeId = 1
                    },
                    new Service
                    {
                        BarbershopId = 1,
                        ServiceName = "СТРИЖКА МАШИНКОЙ",
                        ServiceDescription = "",
                        ServicePrice = 200,
                        ServiceDuration = 30,
                        ServiceTypeId = 2
                    },
                    new Service
                    {
                        BarbershopId = 1,
                        ServiceName = "СТРИЖКА БОРОДЫ",
                        ServiceDescription = "",
                        ServicePrice = 250,
                        ServiceDuration = 60,
                        ServiceTypeId = 3
                    },
                    new Service
                    {
                        BarbershopId = 1,
                        ServiceName = "ТРАДИЦИОННОЕ ВЛАЖНОЕ БРИТЬЕ",
                        ServiceDescription = "",
                        ServicePrice = 250,
                        ServiceDuration = 60,
                        ServiceTypeId = 4
                    },
                    new Service
                    {
                        BarbershopId = 1,
                        ServiceName = "БРИТЬЕ ОПАСНОЙ БРИТВОЙ",
                        ServiceDescription = "",
                        ServicePrice = 350,
                        ServiceDuration = 90,
                        ServiceTypeId = 5
                    },
                    new Service
                    {
                        BarbershopId = 1,
                        ServiceName = "МУЖСКАЯ СТРИЖКА И СТРИЖКА БОРОДЫ",
                        ServiceDescription = "",
                        ServicePrice = 500,
                        ServiceDuration = 120,
                        ServiceTypeId = 6
                    },
                    new Service
                    {
                        BarbershopId = 1,
                        ServiceName = "УКЛАДКА ВОЛОС",
                        ServiceDescription = "",
                        ServicePrice = 200,
                        ServiceDuration = 30,
                        ServiceTypeId = 7
                    },
                    new Service
                    {
                        BarbershopId = 1,
                        ServiceName = "ДЕТСКАЯ СТРИЖКА",
                        ServiceDescription = "",
                        ServicePrice = 300,
                        ServiceDuration = 60,
                        ServiceTypeId = 8
                    },
                    new Service
                    {
                        BarbershopId = 1,
                        ServiceName = "МУЖСКАЯ СТРИЖКА И ДЕТСКАЯ",
                        ServiceDescription = "",
                        ServicePrice = 550,
                        ServiceDuration = 120,
                        ServiceTypeId = 9
                    },

                    new Service
                    {
                        BarbershopId = 2,
                        ServiceName = "МУЖСКАЯ СТРИЖКА",
                        ServiceDescription = "",
                        ServicePrice = 350,
                        ServiceDuration = 60,
                        ServiceTypeId = 1
                    },
                    new Service
                    {
                        BarbershopId = 2,
                        ServiceName = "СТРИЖКА МАШИНКОЙ",
                        ServiceDescription = "",
                        ServicePrice = 200,
                        ServiceDuration = 30,
                        ServiceTypeId = 2
                    },
                    new Service
                    {
                        BarbershopId = 2,
                        ServiceName = "СТРИЖКА БОРОДЫ",
                        ServiceDescription = "",
                        ServicePrice = 250,
                        ServiceDuration = 60,
                        ServiceTypeId = 3
                    },
                    new Service
                    {
                        BarbershopId = 2,
                        ServiceName = "ТРАДИЦИОННОЕ ВЛАЖНОЕ БРИТЬЕ",
                        ServiceDescription = "",
                        ServicePrice = 250,
                        ServiceDuration = 60,
                        ServiceTypeId = 4
                    },
                    new Service
                    {
                        BarbershopId = 2,
                        ServiceName = "БРИТЬЕ ОПАСНОЙ БРИТВОЙ",
                        ServiceDescription = "",
                        ServicePrice = 350,
                        ServiceDuration = 90,
                        ServiceTypeId = 5
                    },
                    new Service
                    {
                        BarbershopId = 2,
                        ServiceName = "МУЖСКАЯ СТРИЖКА И СТРИЖКА БОРОДЫ",
                        ServiceDescription = "",
                        ServicePrice = 500,
                        ServiceDuration = 120,
                        ServiceTypeId = 6
                    },
                    new Service
                    {
                        BarbershopId = 2,
                        ServiceName = "УКЛАДКА ВОЛОС",
                        ServiceDescription = "",
                        ServicePrice = 200,
                        ServiceDuration = 30,
                        ServiceTypeId = 7
                    },
                    new Service
                    {
                        BarbershopId = 2,
                        ServiceName = "ДЕТСКАЯ СТРИЖКА",
                        ServiceDescription = "",
                        ServicePrice = 300,
                        ServiceDuration = 60,
                        ServiceTypeId = 8
                    },
                    new Service
                    {
                        BarbershopId = 2,
                        ServiceName = "МУЖСКАЯ СТРИЖКА И ДЕТСКАЯ",
                        ServiceDescription = "",
                        ServicePrice = 550,
                        ServiceDuration = 120,
                        ServiceTypeId = 9
                    });
                context.SaveChanges();
            }
            if (!context.Barbers.Any())
            {
                context.Barbers.AddRange(
                   new Barber
                   {
                       BarbershopId = 1,
                       FirstName = "Никита",
                       LastName = "Смирнов",
                       Email = "Smirnov@gmail.com",
                       Telephone = "050-323-99-99",
                       Facebook = "",
                       Instagram = "",
                       PhotoLink = "1.jpeg"
                   },

                   new Barber
                   {
                       BarbershopId = 1,
                       FirstName = "Александр",
                       LastName = "Солотников",
                       Email = "Solotnikov@gmail.com",
                       Telephone = "050-323-99-10",
                       Facebook = "https://www.facebook.com/Саладовников-Александр-371043806987463/",
                       Instagram = "https://instagram.com/snake_san_barber?igshid=kjrazsllvtff",
                       PhotoLink = "2.jpg"
                   },
                    new Barber
                    {
                        BarbershopId = 2,
                        FirstName = "Дмитрий",
                        LastName = "Власенко",
                        Email = "Vlasenko@gmail.com",
                        Telephone = "050-323-99-10",
                        Facebook = "",
                        Instagram = "",
                        PhotoLink = "3.jpeg"
                    },
                   new Barber
                   {
                       BarbershopId = 2,
                       FirstName = "Антон",
                       LastName = "Нежуренко",
                       Email = "Negurenko@gmail.com",
                       Telephone = "050-323-10-10",
                       Facebook = "",
                       Instagram = "",
                       PhotoLink = "1.jpeg"
                   });
                context.SaveChanges();
            }
        }
    }
}

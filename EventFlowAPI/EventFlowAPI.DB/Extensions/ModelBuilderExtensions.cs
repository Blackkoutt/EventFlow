using EventFlowAPI.DB.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.IO;
using System.Reflection.Emit;

namespace EventFlowAPI.DB.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdditionalServices>().HasData(
                new AdditionalServices
                {
                    Id = 1,
                    Name = "DJ",
                    Price = 400.00
                },

                new AdditionalServices
                {
                    Id = 2,
                    Name = "Obsługa oświetlenia",
                    Price = 340.00
                },

                new AdditionalServices
                {
                    Id = 3,
                    Name = "Obsługa nagłośnienia",
                    Price = 250.00
                },
                new AdditionalServices
                {
                    Id = 4,
                    Name = "Fotograf",
                    Price = 200.00
                },
                 new AdditionalServices
                 {
                     Id = 5,
                     Name = "Promocja wydarzenia",
                     Price = 140.00
                 }
            );
            modelBuilder.Entity<EventCategory>().HasData(
                new EventCategory
                {
                    Id = 1,
                    Name = "Koncert"
                },

                new EventCategory
                {
                    Id = 2,
                    Name = "Film"
                },

                new EventCategory
                {
                    Id = 3,
                    Name = "Spektakl"
                },
                new EventCategory
                {
                    Id = 4,
                    Name = "Wystawa"
                }
            );
            modelBuilder.Entity<EventDetails>().HasData(
                new EventDetails
                {
                    Id = 1,
                    Description = "Krótki opis wydarzenia 1"
                },

                new EventDetails
                {
                    Id = 2,
                    Description = "Krótki opis wydarzenia 2"
                },

                new EventDetails
                {
                    Id = 3,
                    Description = "Krótki opis wydarzenia 3"
                },
                new EventDetails
                {
                    Id = 4,
                    Description = "Krótki opis wydarzenia 4"
                }
            );
            modelBuilder.Entity<EventPassType>().HasData(
                new EventPassType
                {
                    Id = 1,
                    Name = "Karnet miesięczny",
                    Price = 89.99
                },

                new EventPassType
                {
                    Id = 2,
                    Name = "Karnet kwartalny",
                    Price = 235.99
                },

                new EventPassType
                {
                    Id = 3,
                    Name = "Karnet półroczny",
                    Price = 499.99
                },
                new EventPassType
                {
                    Id = 4,
                    Name = "Karnet roczny",
                    Price = 999.99
                }
            );
            modelBuilder.Entity<EventPassType>().HasData(
               new EventPassType
               {
                   Id = 1,
                   Name = "Karnet miesięczny",
                   Price = 89.99
               },

               new EventPassType
               {
                   Id = 2,
                   Name = "Karnet kwartalny",
                   Price = 235.99
               },

               new EventPassType
               {
                   Id = 3,
                   Name = "Karnet półroczny",
                   Price = 499.99
               },
               new EventPassType
               {
                   Id = 4,
                   Name = "Karnet roczny",
                   Price = 999.99
               }
           );
            modelBuilder.Entity<Festival>().HasData(
              new Festival
              {
                  Id = 1,
                  Name = "Festiwal muzyki współczesnej",
                  StartDate = DateTime.Now.AddMonths(1),
                  EndDate = DateTime.Now.AddMonths(1).AddDays(4)
              },

              new Festival
              {
                  Id = 2,
                  Name = "Festiwal filmowy",
                  StartDate = DateTime.Now.AddMonths(3),
                  EndDate = DateTime.Now.AddMonths(3).AddDays(2)
              },

              new Festival
              {
                  Id = 3,
                  Name = "Festiwal sztuki abstrakcyjnej",
                  StartDate = DateTime.Now.AddMonths(5),
                  EndDate = DateTime.Now.AddMonths(5).AddDays(1)
              }
          );
            modelBuilder.Entity<FestivalDetails>().HasData(
                new FestivalDetails
                {
                    Id = 1,
                    Description = "Opis festiwalu muzyki współczesnej",
                },

                new FestivalDetails
                {
                    Id = 2,
                    Description = "Opis festiwalu filmowego",
                },

                new FestivalDetails
                {
                    Id = 3,
                    Description = "Opis festiwalu sztuki abstrakcyjnej",
                }
            );
            modelBuilder.Entity<HallType>().HasData(
                new HallType
                {
                    Id = 1,
                    Name = "Sala kinowa", 
                    Description = "Nowa sala kinowa wyposażona w nowoczesne nagłośnienie i ekran",
                },

                new HallType
                {
                    Id = 2,
                    Name = "Sala koncertowa",
                    Description = "Nowa sala koncertowa wyposażona w najwyższej klasy nagłośnienie",
                },

                new HallType
                {
                    Id = 3,
                    Name = "Sala teatralna",
                    Description = "Opis sali teatralnej",
                }
            );
            modelBuilder.Entity<Hall>().HasData(
                new Hall
                {
                    HallNr = 1,
                    RentalPricePerHour = 120.99,
                    floor = 2,
                    Area = 50.45,
                    NumberOfSeatsRows = 10,
                    NumberOfSeatsColumns = 10,
                    MaxNumberOfSeats = 100,
                    HallTypeId = 1
                },

                new Hall
                {
                    HallNr = 2,
                    RentalPricePerHour = 89.99,
                    floor = 1,
                    Area = 68.85,
                    NumberOfSeatsRows = 8,
                    NumberOfSeatsColumns = 12,
                    MaxNumberOfSeats = 96,
                    HallTypeId = 2
                },

                new Hall
                {
                    HallNr = 3,
                    RentalPricePerHour = 179.99,
                    floor = 2,
                    Area = 75.30,
                    NumberOfSeatsRows = 7,
                    NumberOfSeatsColumns = 14,
                    MaxNumberOfSeats = 98,
                    HallTypeId = 3
                }
            );
            modelBuilder.Entity<Equipment>().HasData(
               new Equipment
               {
                   Id = 1,
                   Name = "Projektor multimedialny",
                   Description = "Nowoczesny projektor"
               },

               new Equipment
               {
                   Id = 2,
                   Name = "Oświetlenie",
                   Description = "Wysokiej klasy oświetlenie"
               },

               new Equipment
               {
                   Id = 3,
                   Name = "Nagłośnienie kinowe",
                   Description = "Głośniki przeznaczone do odtwrzania filmów"
               },
               new Equipment
               {
                   Id = 4,
                   Name = "Nagłośnienie koncertowe",
                   Description = "Głośniki przeznaczone do koncertów"
               }
           );
            modelBuilder.Entity<HallType_Equipment>().HasData(
                new HallType_Equipment
                {
                    HallTypeId = 1,
                    EquipmentId = 1
                },
                new HallType_Equipment
                {
                    HallTypeId = 1,
                    EquipmentId = 3
                },
                new HallType_Equipment
                {
                    HallTypeId = 2,
                    EquipmentId = 2
                },
                new HallType_Equipment
                {
                    HallTypeId = 2,
                    EquipmentId = 4
                },
                new HallType_Equipment
                {
                    HallTypeId = 3,
                    EquipmentId = 2
                }
            );
            modelBuilder.Entity<MediaPatron>().HasData(
               new MediaPatron
               {
                   Id = 1,
                   Name = "Gazeta Nowoczesna"
               },
               new MediaPatron
               {
                   Id = 2,
                   Name = "Nowy świat TV"
               },
               new MediaPatron
               {
                   Id = 3,
                   Name = "Tygodnik Nowiny"
               }
           );
            modelBuilder.Entity<Organizer>().HasData(
               new Organizer
               {
                   Id = 1,
                   Name = "EventFlow"
               },
               new Organizer
               {
                   Id = 2,
                   Name = "Snowflake"
               },
               new Organizer
               {
                   Id = 3,
                   Name = "Aura"
               }
           );
            modelBuilder.Entity<Sponsor>().HasData(
              new Sponsor
              {
                  Id = 1,
                  Name = "Basel"
              },
              new Sponsor
              {
                  Id = 2,
                  Name = "Vision"
              },
              new Sponsor
              {
                  Id = 3,
                  Name = "Waveless"
              }
          );
            modelBuilder.Entity<SeatType>().HasData(
              new SeatType
              {
                  Id = 1,
                  Name = "Miejsce VIP",
                  Description = "Opis miejsca VIP",
                  AddtionalPaymentPercentage = 25.00
              },
              new SeatType
              {
                  Id = 2,
                  Name = "Miejsce klasy premium",
                  Description = "Opis miejsca klasy permium",
                  AddtionalPaymentPercentage = 10.00
              },
              new SeatType
              {
                  Id = 3,
                  Name = "Miejsce zwykłe",
                  Description = "Opis miejsca zwykłego",
                  AddtionalPaymentPercentage = 0.00
              }
          );
            modelBuilder.Entity<TicketType>().HasData(
              new TicketType
              {
                  Id = 1,
                  Name = "Bilet normalny"
              },
              new TicketType
              {
                  Id = 2,
                  Name = "Bilet ulgowy"
              },
              new TicketType
              {
                  Id = 3,
                  Name = "Bilet rodzinny"
              }
          );
            modelBuilder.Entity<UserData>().HasData(
              new UserData
              {
                  Id = 1,
                  Street = "Wesoła",
                  HouseNumber = 12,
                  FlatNumber = 14,
                  City = "Warszawa",
                  ZipCode = "15-264",
                  PhoneNumber = "789456123"
              },
              new UserData
              {
                  Id = 2,
                  Street = "Wiejska",
                  HouseNumber = 10,
                  FlatNumber = 31,
                  City = "Poznań",
                  ZipCode = "01-342",
                  PhoneNumber = "123456789"
              },
              new UserData
              {
                  Id = 3,
                  Street = "Pogodna",
                  HouseNumber = 7,
                  FlatNumber = 21,
                  City = "Białystok",
                  ZipCode = "14-453",
                  PhoneNumber = "147852369"
              }
          );
            modelBuilder.Entity<User>().HasData(
             new User
             {
                 Id = 1,
                 Name = "Piotr",
                 Surname = "Nowicki",
                 Email = "p.nowicki@gmail.com",
                 DateOfBirth = new DateTime(2000, 4, 3)
             },
             new User
             {
                 Id = 2,
                 Name = "Adam",
                 Surname = "Nowak",
                 Email = "a.nowak@gmail.com",
                 DateOfBirth = new DateTime(1985, 2, 1)
             },
             new User
             {
                 Id = 3,
                 Name = "Anna",
                 Surname = "Kowalska",
                 Email = "a.kowalska@gmail.com",
                 DateOfBirth = new DateTime(1979, 12, 11)
             }
         );
        }
    }
}

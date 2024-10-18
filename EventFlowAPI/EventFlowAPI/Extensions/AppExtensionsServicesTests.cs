﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces.Configuration.HallConfiguration;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces.Configuration.PassConfiguration;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces.Configuration.TicketConfiguration;
using EventFlowAPI.Logic.Services.OtherServices.Services;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Extensions
{
    public static class AppExtensionsServicesTests
    {
        public async static void AddServicesTests(this WebApplication app)
        {
           // await app.AddHallRentPDFPreviewer();
            await app.CreateHallRentSeatsJPGTest();
            await app.CreateHallViewPDFTest();
        }


        public async static Task CreateHallViewPDFTest(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var assetService = scope.ServiceProvider.GetRequiredService<IAssetService>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var pdfbuilder = new PdfBuilderService(assetService, unitOfWork);
                var hall = GetHall();
                var hallRent = GetHallRent();
                await pdfbuilder.CreateHallViewPdf([],hall, hallRent, null);
            }
        }

        public async static Task CreateHallRentSeatsJPGTest(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var festivalTicketConfig = scope.ServiceProvider.GetRequiredService<IFestivalTicketConfiguration>();
                var eventTicketConfig = scope.ServiceProvider.GetRequiredService<IEventTicketConfiguration>();
                var hallSeatsConfig = scope.ServiceProvider.GetRequiredService<IHallSeatsConfiguration>();
                var eventPassConfig = scope.ServiceProvider.GetRequiredService<IEventPassConfiguration>();
                var qrGenerator = scope.ServiceProvider.GetRequiredService<IQRCodeGeneratorService>();
                var assetService = scope.ServiceProvider.GetRequiredService<IAssetService>();

                var jpgbuilder = new JPGCreatorService(festivalTicketConfig, eventTicketConfig, hallSeatsConfig, eventPassConfig, qrGenerator, assetService);
                var hall = GetHall();
                await jpgbuilder.CreateHallJPG(hall);
            }
        }

        public async static Task AddHallRentPDFPreviewer(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var assetService = scope.ServiceProvider.GetRequiredService<IAssetService>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var pdfbuilder = new PdfBuilderService(assetService, unitOfWork);
                var hallRent = GetHallRent();
                await pdfbuilder.CreateHallRentPdf(hallRent);
            }
        }
        private static Hall GetHall()
        {
            return new Hall
            {
                Id = 5,
                DefaultId = 1,
                HallNr = 2,
                RentalPricePerHour = 400m,
                Floor = 1,
                Type = new HallType
                {
                    Id = 1,
                    Name = "Sala koncertowa",
                    Description = "Hall type Descirption",
                    Equipments = new List<Equipment>
                    {
                        new Equipment
                        {
                            Id = 1,
                            Name = "Projektor"
                        },
                        new Equipment
                        {
                            Id = 2,
                            Name = "Garderoba"
                        },
                    }
                },
                HallDetails = new HallDetails
                {
                    TotalLength = 20,
                    TotalWidth = 40,
                    TotalArea = 800,
                    StageLength = 4,
                    StageWidth = 20,
                    NumberOfSeatsRows = 10,
                    MaxNumberOfSeatsRows = 18,
                    NumberOfSeatsColumns = 10,
                    MaxNumberOfSeatsColumns = 18,
                    NumberOfSeats = 100,
                    MaxNumberOfSeats = 225,
                },
                Seats = GetSeatList()
            };
        }
        private static List<Seat> GetSeatList()
        {
            return new List<Seat>()
            {
                new Seat
                {
                    SeatNr = 1,
                    GridRow = 1,
                    GridColumn = 1,
                    SeatType = new SeatType
                    {
                        Name = "VIP",
                        Description = "Opis miejsca VIP",
                        SeatColor = "#9803fc",
                        AddtionalPaymentPercentage = 25.00m
                    }
                },
                new Seat
                {
                    SeatNr = 2,
                    GridRow = 1,
                    GridColumn = 2,
                    SeatType = new SeatType
                    {
                        Name = "VIP",
                        Description = "Opis miejsca VIP",
                        SeatColor = "#9803fc",
                        AddtionalPaymentPercentage = 25.00m
                    }
                },
                new Seat
                {
                    SeatNr = 3,
                    GridRow = 1,
                    GridColumn = 3,
                    SeatType = new SeatType
                    {
                        Name = "VIP",
                        Description = "Opis miejsca VIP",
                        SeatColor = "#9803fc",
                        AddtionalPaymentPercentage = 25.00m
                    }
                },
                new Seat
                {
                    SeatNr = 4,
                    GridRow = 1,
                    GridColumn = 4,
                    SeatType = new SeatType
                    {
                        Name = "Premium",
                        SeatColor = "#ffa600",
                        Description = "Opis miejsca klasy premium",
                        AddtionalPaymentPercentage = 10.00m
                    }
                },
                new Seat
                {
                    SeatNr = 5,
                    GridRow = 1,
                    GridColumn = 8,
                    SeatType = new SeatType
                    {
                        Name = "Zwykłe",
                        SeatColor = "#039aff",
                        Description = "Opis miejsca zwykłego",
                        AddtionalPaymentPercentage = 0.00m
                    }
                },
                 new Seat
                {
                    SeatNr = 6,
                    GridRow = 3,
                    GridColumn = 9,
                    SeatType = new SeatType
                    {
                        Name = "Zwykłe",
                        SeatColor = "#039aff",
                        Description = "Opis miejsca zwykłego",
                        AddtionalPaymentPercentage = 0.00m
                    }
                },
                  new Seat
                {
                    SeatNr = 7,
                    GridRow = 5,
                    GridColumn = 1,
                    SeatType = new SeatType
                    {
                        Name = "Zwykłe",
                        SeatColor = "#039aff",
                        Description = "Opis miejsca zwykłego",
                        AddtionalPaymentPercentage = 0.00m
                    }
                },
                    new Seat
                {
                    SeatNr = 8,
                    GridRow = 5,
                    GridColumn = 2,
                    SeatType = new SeatType
                    {
                        Name = "Zwykłe",
                        SeatColor = "#039aff",
                        Description = "Opis miejsca zwykłego",
                        AddtionalPaymentPercentage = 0.00m
                    }
                },
                      new Seat
                {
                    SeatNr = 9,
                    GridRow = 5,
                    GridColumn = 3,
                    SeatType = new SeatType
                    {
                        Name = "Zwykłe",
                        SeatColor = "#039aff",
                        Description = "Opis miejsca zwykłego",
                        AddtionalPaymentPercentage = 0.00m
                    }
                },
                        new Seat
                {
                    SeatNr = 10,
                    GridRow = 5,
                    GridColumn = 4,
                    SeatType = new SeatType
                    {
                        Name = "Zwykłe",
                        SeatColor = "#039aff",
                        Description = "Opis miejsca zwykłego",
                        AddtionalPaymentPercentage = 0.00m
                    }
                },
                          new Seat
                {
                    SeatNr = 11,
                    GridRow = 7,
                    GridColumn = 5,
                    SeatType = new SeatType
                    {
                        Name = "Zwykłe",
                        SeatColor = "#039aff",
                        Description = "Opis miejsca zwykłego",
                        AddtionalPaymentPercentage = 0.00m
                    }
                },
                                    new Seat
                {
                    SeatNr = 12,
                    GridRow = 7,
                    GridColumn = 7,
                    SeatType = new SeatType
                    {
                        Name = "Zwykłe",
                        SeatColor = "#039aff",
                        Description = "Opis miejsca zwykłego",
                        AddtionalPaymentPercentage = 0.00m
                    }
                },


            };
        }

        private static HallRent GetHallRent()
        {
            var dateNow = DateTime.Now;

            return new HallRent
            {
                Id = 1,
                HallRentGuid = Guid.NewGuid(),
                StartDate = dateNow,
                EndDate = dateNow.AddHours(2),
                Duration = dateNow.AddHours(2) - dateNow,
                RentDate = dateNow,
                PaymentDate = dateNow,
                PaymentAmount = 1200m,
                PaymentType = new PaymentType
                {
                    Id = 1,
                    Name = "Płatność kartą"
                },
                User = new User
                {
                    Id = "1",
                    Name = "Mateusz",
                    Surname = "Strapczuk",
                    Email = "mateusz.strapczuk@gmail.com"
                },
                Hall = GetHall(),
                AdditionalServices = new List<AdditionalServices>()
                {
                    new AdditionalServices
                    {
                        Id = 1,
                        Name = "DJ",
                        Price = 200m,
                    },
                    new AdditionalServices
                    {
                        Id = 2,
                        Name = "Catering",
                        Price = 200m,
                    },
                }
            };
        }
    }
}

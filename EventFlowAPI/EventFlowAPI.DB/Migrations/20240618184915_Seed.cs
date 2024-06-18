using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventFlowAPI.DB.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AdditionalServices",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "DJ", 400m },
                    { 2, "Obsługa oświetlenia", 340m },
                    { 3, "Obsługa nagłośnienia", 250m },
                    { 4, "Fotograf", 200m },
                    { 5, "Promocja wydarzenia", 140m }
                });

            migrationBuilder.InsertData(
                table: "Equipment",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Nowoczesny projektor", "Projektor multimedialny" },
                    { 2, "Wysokiej klasy oświetlenie", "Oświetlenie" },
                    { 3, "Głośniki przeznaczone do odtwrzania filmów", "Nagłośnienie kinowe" },
                    { 4, "Głośniki przeznaczone do koncertów", "Nagłośnienie koncertowe" }
                });

            migrationBuilder.InsertData(
                table: "EventCategory",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Koncert" },
                    { 2, "Film" },
                    { 3, "Spektakl" },
                    { 4, "Wystawa" }
                });

            migrationBuilder.InsertData(
                table: "EventDetails",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Krótki opis wydarzenia Koncert: Mystic Waves" },
                    { 2, "Krótki opis wydarzenia Spektakl: Cień Przeszłość" },
                    { 3, "Krótki opis wydarzenia Film: Królestwo planety małp" },
                    { 4, "Krótki opis wydarzenia Wystawa: Nowe inspiracje" },
                    { 5, "Krótki opis wydarzenia Koncert: New Era" },
                    { 6, "Krótki opis wydarzenia Film: Gladiator" },
                    { 7, "Krótki opis wydarzenia Wystawa: Nowa sztuka" }
                });

            migrationBuilder.InsertData(
                table: "EventPassType",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Karnet miesięczny", 89.99m },
                    { 2, "Karnet kwartalny", 235.99m },
                    { 3, "Karnet półroczny", 499.99m },
                    { 4, "Karnet roczny", 999.99m }
                });

            migrationBuilder.InsertData(
                table: "FestivalDetails",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Opis festiwalu muzyki współczesnej" },
                    { 2, "Opis festiwalu filmowego" },
                    { 3, "Opis festiwalu sztuki abstrakcyjnej" }
                });

            migrationBuilder.InsertData(
                table: "HallType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Nowa sala kinowa wyposażona w nowoczesne nagłośnienie i ekran", "Sala filmowa" },
                    { 2, "Nowa sala koncertowa wyposażona w najwyższej klasy nagłośnienie", "Sala koncertowa" },
                    { 3, "Opis sali teatralnej", "Sala teatralna" },
                    { 4, "Opis sali wystawowa", "Sala wystawowa" }
                });

            migrationBuilder.InsertData(
                table: "MediaPatron",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Gazeta Nowoczesna" },
                    { 2, "Nowy świat TV" },
                    { 3, "Tygodnik Nowiny" }
                });

            migrationBuilder.InsertData(
                table: "Organizer",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "EventFlow" },
                    { 2, "Snowflake" },
                    { 3, "Aura" }
                });

            migrationBuilder.InsertData(
                table: "PaymentType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Karta kredytowa" },
                    { 2, "Przelew" },
                    { 3, "BLIK" },
                    { 4, "Zapłać później" }
                });

            migrationBuilder.InsertData(
                table: "SeatType",
                columns: new[] { "Id", "AddtionalPaymentPercentage", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 25m, "Opis miejsca VIP", "Miejsce VIP" },
                    { 2, 10m, "Opis miejsca klasy premium", "Miejsce klasy premium" },
                    { 3, 0m, "Opis miejsca zwykłego", "Miejsce zwykłe" }
                });

            migrationBuilder.InsertData(
                table: "Sponsor",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Basel" },
                    { 2, "Vision" },
                    { 3, "Waveless" }
                });

            migrationBuilder.InsertData(
                table: "TicketType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bilet normalny" },
                    { 2, "Bilet ulgowy" },
                    { 3, "Bilet rodzinny" }
                });

            migrationBuilder.InsertData(
                table: "UserData",
                columns: new[] { "Id", "City", "FlatNumber", "HouseNumber", "PhoneNumber", "Street", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Warszawa", 14m, 12m, "789456123", "Wesoła", "15-264" },
                    { 2, "Poznań", 31m, 10m, "123456789", "Wiejska", "01-342" },
                    { 3, "Białystok", 21m, 7m, "147852369", "Pogodna", "14-453" }
                });

            migrationBuilder.InsertData(
                table: "Festival",
                columns: new[] { "Id", "EndDate", "Name", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Festiwal muzyki współczesnej", new DateTime(2024, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2024, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Festiwal filmowy", new DateTime(2024, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2024, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Festiwal sztuki abstrakcyjnej", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Hall",
                columns: new[] { "HallNr", "Area", "HallTypeId", "MaxNumberOfSeats", "NumberOfSeatsColumns", "NumberOfSeatsRows", "RentalPricePerHour", "floor" },
                values: new object[,]
                {
                    { 1, 50.45m, 1, 100m, 10m, 10m, 120.99m, 2m },
                    { 2, 68.85m, 2, 96m, 12m, 8m, 89.99m, 1m },
                    { 3, 75.3m, 3, 98m, 14m, 7m, 179.99m, 2m },
                    { 4, 55.2m, 4, 120m, 15m, 8m, 199.99m, 1m }
                });

            migrationBuilder.InsertData(
                table: "HallType_Equipment",
                columns: new[] { "EquipmentId", "HallTypeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 1 },
                    { 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "DateOfBirth", "Email", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "p.nowicki@gmail.com", "Piotr", "Nowicki" },
                    { 2, new DateTime(1985, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "a.nowak@gmail.com", "Adam", "Nowak" },
                    { 3, new DateTime(1979, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "a.kowalska@gmail.com", "Anna", "Kowalska" }
                });

            migrationBuilder.InsertData(
                table: "Event",
                columns: new[] { "Id", "CategoryId", "EndDate", "HallNr", "Name", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 7, 19, 1, 0, 0, 0, DateTimeKind.Unspecified), 2, "Koncert: Mystic Waves", new DateTime(2024, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 3, new DateTime(2024, 7, 20, 3, 0, 0, 0, DateTimeKind.Unspecified), 3, "Cień Przeszłość", new DateTime(2024, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, new DateTime(2024, 7, 21, 2, 0, 0, 0, DateTimeKind.Unspecified), 1, "Królestwo planety małp", new DateTime(2024, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, new DateTime(2024, 7, 22, 3, 0, 0, 0, DateTimeKind.Unspecified), 4, "Nowe inspiracje", new DateTime(2024, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, new DateTime(2024, 8, 19, 1, 0, 0, 0, DateTimeKind.Unspecified), 2, "Koncert: New Era", new DateTime(2024, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 2, new DateTime(2024, 8, 21, 2, 0, 0, 0, DateTimeKind.Unspecified), 1, "Gladiator", new DateTime(2024, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 4, new DateTime(2024, 8, 22, 3, 0, 0, 0, DateTimeKind.Unspecified), 4, "Nowa sztuka", new DateTime(2024, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "EventPass",
                columns: new[] { "Id", "EndDate", "PassTypeId", "PaymentAmount", "PaymentDate", "PaymentTypeId", "RenewalDate", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 499.99m, new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2025, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 999.99m, new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, new DateTime(2024, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 235.99m, new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.InsertData(
                table: "Festival_MediaPatron",
                columns: new[] { "FestivalId", "MediaPatronId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 },
                    { 2, 3 },
                    { 3, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Festival_Organizer",
                columns: new[] { "FestivalId", "OrganizerId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 },
                    { 2, 3 },
                    { 3, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Festival_Sponsor",
                columns: new[] { "FestivalId", "SponsorId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 },
                    { 2, 3 },
                    { 3, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "HallRent",
                columns: new[] { "Id", "EndDate", "HallNr", "PaymentAmount", "PaymentDate", "PaymentTypeId", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), 1, 899.99m, new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2024, 7, 20, 4, 0, 0, 0, DateTimeKind.Unspecified), 3, 699.99m, new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 3, new DateTime(2024, 7, 21, 2, 0, 0, 0, DateTimeKind.Unspecified), 3, 399.99m, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, new DateTime(2024, 7, 22, 1, 0, 0, 0, DateTimeKind.Unspecified), 4, 150.99m, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "Seat",
                columns: new[] { "Id", "Column", "HallNr", "Row", "SeatNr", "SeatTypeId" },
                values: new object[,]
                {
                    { 1, 1m, 1, 1m, 1m, 1 },
                    { 2, 2m, 1, 1m, 2m, 1 },
                    { 3, 3m, 1, 1m, 3m, 1 },
                    { 4, 4m, 1, 1m, 4m, 1 },
                    { 5, 1m, 2, 1m, 1m, 2 },
                    { 6, 2m, 2, 1m, 2m, 2 },
                    { 7, 3m, 2, 1m, 3m, 2 },
                    { 8, 4m, 2, 1m, 4m, 2 },
                    { 9, 1m, 3, 1m, 1m, 3 },
                    { 10, 2m, 3, 1m, 2m, 3 },
                    { 11, 3m, 3, 1m, 3m, 3 },
                    { 12, 4m, 3, 1m, 4m, 3 },
                    { 13, 1m, 4, 1m, 1m, 3 }
                });

            migrationBuilder.InsertData(
                table: "EventTicket",
                columns: new[] { "Id", "EventId", "Price", "TicketTypeId" },
                values: new object[,]
                {
                    { 1, 1, 24.99m, 1 },
                    { 2, 2, 34.99m, 2 },
                    { 3, 3, 29.99m, 3 },
                    { 4, 4, 19.99m, 3 }
                });

            migrationBuilder.InsertData(
                table: "Festival_Event",
                columns: new[] { "EventId", "FestivalId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 5, 1 },
                    { 2, 2 },
                    { 6, 2 },
                    { 4, 3 },
                    { 7, 3 }
                });

            migrationBuilder.InsertData(
                table: "HallRent_AdditionalServices",
                columns: new[] { "AdditionalServiceId", "HallRentId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 },
                    { 2, 3 },
                    { 4, 4 },
                    { 5, 4 }
                });

            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "Id", "EventTicketId", "PaymentAmount", "PaymentTypeId", "ReservationDate", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 24.99m, 1, new DateTime(2024, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 2, 34.99m, 2, new DateTime(2024, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, 3, 29.99m, 3, new DateTime(2024, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, 4, 19.99m, 2, new DateTime(2024, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.InsertData(
                table: "Reservation_Seat",
                columns: new[] { "ReservationId", "SeatId" },
                values: new object[,]
                {
                    { 1, 4 },
                    { 2, 8 },
                    { 3, 10 },
                    { 4, 13 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventPass",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EventPass",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EventPass",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EventPassType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Festival_Event",
                keyColumns: new[] { "EventId", "FestivalId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Festival_Event",
                keyColumns: new[] { "EventId", "FestivalId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "Festival_Event",
                keyColumns: new[] { "EventId", "FestivalId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Festival_Event",
                keyColumns: new[] { "EventId", "FestivalId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "Festival_Event",
                keyColumns: new[] { "EventId", "FestivalId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "Festival_Event",
                keyColumns: new[] { "EventId", "FestivalId" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "Festival_MediaPatron",
                keyColumns: new[] { "FestivalId", "MediaPatronId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Festival_MediaPatron",
                keyColumns: new[] { "FestivalId", "MediaPatronId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Festival_MediaPatron",
                keyColumns: new[] { "FestivalId", "MediaPatronId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Festival_MediaPatron",
                keyColumns: new[] { "FestivalId", "MediaPatronId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Festival_MediaPatron",
                keyColumns: new[] { "FestivalId", "MediaPatronId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "Festival_MediaPatron",
                keyColumns: new[] { "FestivalId", "MediaPatronId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Festival_Organizer",
                keyColumns: new[] { "FestivalId", "OrganizerId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Festival_Organizer",
                keyColumns: new[] { "FestivalId", "OrganizerId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Festival_Organizer",
                keyColumns: new[] { "FestivalId", "OrganizerId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Festival_Organizer",
                keyColumns: new[] { "FestivalId", "OrganizerId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Festival_Organizer",
                keyColumns: new[] { "FestivalId", "OrganizerId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "Festival_Organizer",
                keyColumns: new[] { "FestivalId", "OrganizerId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Festival_Sponsor",
                keyColumns: new[] { "FestivalId", "SponsorId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Festival_Sponsor",
                keyColumns: new[] { "FestivalId", "SponsorId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Festival_Sponsor",
                keyColumns: new[] { "FestivalId", "SponsorId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Festival_Sponsor",
                keyColumns: new[] { "FestivalId", "SponsorId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Festival_Sponsor",
                keyColumns: new[] { "FestivalId", "SponsorId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "Festival_Sponsor",
                keyColumns: new[] { "FestivalId", "SponsorId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "HallRent",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HallRent_AdditionalServices",
                keyColumns: new[] { "AdditionalServiceId", "HallRentId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "HallRent_AdditionalServices",
                keyColumns: new[] { "AdditionalServiceId", "HallRentId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "HallRent_AdditionalServices",
                keyColumns: new[] { "AdditionalServiceId", "HallRentId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "HallRent_AdditionalServices",
                keyColumns: new[] { "AdditionalServiceId", "HallRentId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "HallRent_AdditionalServices",
                keyColumns: new[] { "AdditionalServiceId", "HallRentId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "HallRent_AdditionalServices",
                keyColumns: new[] { "AdditionalServiceId", "HallRentId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "HallType_Equipment",
                keyColumns: new[] { "EquipmentId", "HallTypeId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "HallType_Equipment",
                keyColumns: new[] { "EquipmentId", "HallTypeId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "HallType_Equipment",
                keyColumns: new[] { "EquipmentId", "HallTypeId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "HallType_Equipment",
                keyColumns: new[] { "EquipmentId", "HallTypeId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "HallType_Equipment",
                keyColumns: new[] { "EquipmentId", "HallTypeId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "PaymentType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservation_Seat",
                keyColumns: new[] { "ReservationId", "SeatId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "Reservation_Seat",
                keyColumns: new[] { "ReservationId", "SeatId" },
                keyValues: new object[] { 2, 8 });

            migrationBuilder.DeleteData(
                table: "Reservation_Seat",
                keyColumns: new[] { "ReservationId", "SeatId" },
                keyValues: new object[] { 3, 10 });

            migrationBuilder.DeleteData(
                table: "Reservation_Seat",
                keyColumns: new[] { "ReservationId", "SeatId" },
                keyValues: new object[] { 4, 13 });

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AdditionalServices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AdditionalServices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AdditionalServices",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AdditionalServices",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AdditionalServices",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EventPassType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EventPassType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EventPassType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Festival",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Festival",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Festival",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HallRent",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HallRent",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HallRent",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MediaPatron",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MediaPatron",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MediaPatron",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Organizer",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Organizer",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Organizer",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Sponsor",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sponsor",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sponsor",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EventDetails",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EventDetails",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EventDetails",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EventTicket",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EventTicket",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EventTicket",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EventTicket",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FestivalDetails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FestivalDetails",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FestivalDetails",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PaymentType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PaymentType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PaymentType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SeatType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SeatType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SeatType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TicketType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TicketType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TicketType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserData",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserData",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserData",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EventCategory",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EventCategory",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EventCategory",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EventCategory",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EventDetails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EventDetails",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EventDetails",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EventDetails",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hall",
                keyColumn: "HallNr",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hall",
                keyColumn: "HallNr",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hall",
                keyColumn: "HallNr",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hall",
                keyColumn: "HallNr",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "HallType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HallType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HallType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HallType",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}

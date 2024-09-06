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
                    { 1, "DJ", 400.00m },
                    { 2, "Obsługa oświetlenia", 340.00m },
                    { 3, "Obsługa nagłośnienia", 250.00m },
                    { 4, "Fotograf", 200.00m },
                    { 5, "Promocja wydarzenia", 140.00m }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin role", "Admin", "ADMIN" },
                    { "2", null, "User role", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "fb3ddf27-067c-46e1-b597-f0c15014b882", new DateTime(2000, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", true, false, null, "Admin", "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEILawDLW2MKaPbnP4+aAv3ClpmWulEdis1saOaiAI1tp5+eusxjyDHi+2nJP7t+mXQ==", null, false, "dfbadf0f-220c-459e-912b-d1eed97ba477", "Admin", false, "admin@gmail.com" },
                    { "2", 0, "21afc9a3-3116-40cd-9a41-6ef10a3ef8f2", new DateTime(1985, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "j.kowalski@gmail.com", true, false, null, "Jan", "J.KOWALSKI@GMAIL.COM", "J.KOWALSKI@GMAIL.COM", "AQAAAAIAAYagAAAAEGoCny2DEyg2BOLKTmf9TLiqJl3Dz1UMSjRWcHkMh4Oafs8K5RH2e5WBBbZeXA8zSQ==", null, false, "40e0a49d-7933-4670-b3c2-75b8d86308bb", "Kowalski", false, "j.kowalski@gmail.com" },
                    { "3", 0, "f588cf5f-8c24-4a06-bbcf-f6a5b12254fa", new DateTime(1979, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "a.kowalska@gmail.com", true, false, null, "Anna", "A.KOWALSKA@GMAIL.COM", "A.KOWALSKA@GMAIL.COM", "AQAAAAIAAYagAAAAEDZ7CKBJDSuAMxXD+MRKoQSumw3ms9+gngofZUgDnazhcHtP8Xx5HGzvUxz1Fap1bA==", null, false, "9359c437-c522-44cd-bd33-57d44cc243f6", "Kowalska", false, "a.kowalska@gmail.com" }
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
                columns: new[] { "Id", "LongDescription" },
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
                columns: new[] { "Id", "Name", "Price", "ValidityPeriodInMonths" },
                values: new object[,]
                {
                    { 1, "Karnet miesięczny", 89.99m, 1m },
                    { 2, "Karnet kwartalny", 235.99m, 3m },
                    { 3, "Karnet półroczny", 499.99m, 6m },
                    { 4, "Karnet roczny", 999.99m, 12m }
                });

            migrationBuilder.InsertData(
                table: "FestivalDetails",
                columns: new[] { "Id", "LongDescription" },
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
                    { 1, 25.00m, "Opis miejsca VIP", "Miejsce VIP" },
                    { 2, 10.00m, "Opis miejsca klasy premium", "Miejsce klasy premium" },
                    { 3, 0.00m, "Opis miejsca zwykłego", "Miejsce zwykłe" }
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
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "2" },
                    { "2", "3" }
                });

            migrationBuilder.InsertData(
                table: "EventPass",
                columns: new[] { "Id", "EndDate", "PassTypeId", "PaymentAmount", "PaymentDate", "PaymentTypeId", "RenewalDate", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 499.99m, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "1" },
                    { 2, new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 999.99m, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "2" },
                    { 3, new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 235.99m, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "3" }
                });

            migrationBuilder.InsertData(
                table: "Festival",
                columns: new[] { "Id", "Duration", "EndDate", "Name", "ShortDescription", "StartDate" },
                values: new object[,]
                {
                    { 1, new TimeSpan(-31, -1, 0, 0, 0), new DateTime(2024, 10, 6, 1, 0, 0, 0, DateTimeKind.Unspecified), "Festiwal muzyki współczesnej", "Festiwal muzyki współczesnej to nowy festiwal organizowany przez XYZ.", new DateTime(2024, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new TimeSpan(-31, -2, 0, 0, 0), new DateTime(2024, 10, 8, 2, 0, 0, 0, DateTimeKind.Unspecified), "Festiwal filmowy", "Festiwal filmowy to festiwal na którym można obejrzeć filmy.", new DateTime(2024, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new TimeSpan(-30, -3, 0, 0, 0), new DateTime(2024, 10, 9, 3, 0, 0, 0, DateTimeKind.Unspecified), "Festiwal sztuki abstrakcyjnej", "Festiwal sztuki abstrakcyjnej to festiwal na którym można zobaczyć sztukę.", new DateTime(2024, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Hall",
                columns: new[] { "Id", "Floor", "HallNr", "HallTypeId", "IsCopy", "IsVisible", "MaxNumberOfSeats", "MaxNumberOfSeatsColumns", "MaxNumberOfSeatsRows", "NumberOfSeats", "NumberOfSeatsColumns", "NumberOfSeatsRows", "RentalPricePerHour", "StageArea", "TotalArea", "TotalLength", "TotalWidth" },
                values: new object[,]
                {
                    { 1, 2m, 1, 1, false, true, 90m, 10m, 9m, 90m, 10m, 9m, 120.99m, 30m, 120m, 12m, 10m },
                    { 2, 1m, 2, 2, false, true, 150m, 10m, 15m, 150m, 10m, 15m, 89.99m, null, 150m, 15m, 10m },
                    { 3, 2m, 3, 3, false, true, 60m, 10m, 6m, 60m, 10m, 6m, 179.99m, 20m, 80m, 10m, 8m },
                    { 4, 1m, 4, 4, false, true, 100m, 10m, 10m, 100m, 10m, 10m, 199.99m, 40m, 140m, 14m, 10m }
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
                table: "UserData",
                columns: new[] { "Id", "City", "FlatNumber", "HouseNumber", "PhoneNumber", "Street", "ZipCode" },
                values: new object[,]
                {
                    { "1", "Warszawa", 14m, 12m, "789456123", "Wesoła", "15-264" },
                    { "2", "Poznań", 31m, 10m, "123456789", "Wiejska", "01-342" },
                    { "3", "Białystok", 21m, 7m, "147852369", "Pogodna", "14-453" }
                });

            migrationBuilder.InsertData(
                table: "Event",
                columns: new[] { "Id", "CategoryId", "DefaultHallId", "Duration", "EndDate", "HallId", "Name", "ShortDescription", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, 2, new TimeSpan(0, -1, 0, 0, 0), new DateTime(2024, 9, 6, 1, 0, 0, 0, DateTimeKind.Unspecified), 2, "Koncert: Mystic Waves", "Jedyna taka okazja na usłyszenie Mystic Waves na żywo.", new DateTime(2024, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 3, 3, new TimeSpan(0, -3, 0, 0, 0), new DateTime(2024, 9, 7, 3, 0, 0, 0, DateTimeKind.Unspecified), 3, "Cień Przeszłośći", "Cień Przeszłości to jedyny taki spektakl.", new DateTime(2024, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, 1, new TimeSpan(0, -2, 0, 0, 0), new DateTime(2024, 9, 8, 2, 0, 0, 0, DateTimeKind.Unspecified), 1, "Królestwo planety małp", "Nowy film Królestwo planety małp już w kinach!.", new DateTime(2024, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, 4, new TimeSpan(0, -3, 0, 0, 0), new DateTime(2024, 9, 9, 3, 0, 0, 0, DateTimeKind.Unspecified), 4, "Nowe inspiracje", "Nowe inspiracje to nowoczesna wystawa sztuki.", new DateTime(2024, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, 2, new TimeSpan(0, -1, 0, 0, 0), new DateTime(2024, 10, 6, 1, 0, 0, 0, DateTimeKind.Unspecified), 2, "Koncert: New Era", "Jedyna taka okazja na usłyszenie New Era na żywo.", new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 2, 1, new TimeSpan(0, -2, 0, 0, 0), new DateTime(2024, 10, 8, 2, 0, 0, 0, DateTimeKind.Unspecified), 1, "Gladiator", "Nowy film Gladiator już w kinach!.", new DateTime(2024, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 4, 4, new TimeSpan(0, -3, 0, 0, 0), new DateTime(2024, 10, 9, 3, 0, 0, 0, DateTimeKind.Unspecified), 4, "Nowa sztuka", "Nowe sztuka to nowoczesna wystawa sztuki.", new DateTime(2024, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
                columns: new[] { "Id", "DefaultHallId", "EndDate", "HallId", "PaymentAmount", "PaymentDate", "PaymentTypeId", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 6, 8, 0, 0, 0, DateTimeKind.Unspecified), 1, 899.99m, new DateTime(2024, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "1" },
                    { 2, 3, new DateTime(2024, 9, 7, 4, 0, 0, 0, DateTimeKind.Unspecified), 3, 699.99m, new DateTime(2024, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "3" },
                    { 3, 3, new DateTime(2024, 9, 8, 2, 0, 0, 0, DateTimeKind.Unspecified), 3, 399.99m, new DateTime(2024, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "3" },
                    { 4, 4, new DateTime(2024, 9, 9, 1, 0, 0, 0, DateTimeKind.Unspecified), 4, 150.99m, new DateTime(2024, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "2" }
                });

            migrationBuilder.InsertData(
                table: "Seat",
                columns: new[] { "Id", "Column", "GridColumn", "GridRow", "HallId", "Row", "SeatNr", "SeatTypeId" },
                values: new object[,]
                {
                    { 1, 1m, 1m, 1m, 1, 1m, 1m, 1 },
                    { 2, 2m, 2m, 1m, 1, 1m, 2m, 1 },
                    { 3, 3m, 3m, 1m, 1, 1m, 3m, 1 },
                    { 4, 4m, 4m, 1m, 1, 1m, 4m, 1 },
                    { 5, 1m, 1m, 1m, 2, 1m, 1m, 2 },
                    { 6, 2m, 2m, 1m, 2, 1m, 2m, 2 },
                    { 7, 3m, 3m, 1m, 2, 1m, 3m, 1 },
                    { 8, 4m, 4m, 1m, 2, 1m, 4m, 2 },
                    { 9, 1m, 1m, 1m, 3, 1m, 1m, 3 },
                    { 10, 2m, 2m, 1m, 3, 1m, 2m, 3 },
                    { 11, 3m, 3m, 1m, 3, 1m, 3m, 3 },
                    { 12, 4m, 4m, 1m, 3, 1m, 4m, 3 },
                    { 13, 1m, 1m, 1m, 4, 1m, 1m, 3 }
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
                columns: new[] { "AdditionalServicesId", "HallRentId" },
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
                table: "Ticket",
                columns: new[] { "Id", "EventId", "FestivalId", "Price", "TicketTypeId" },
                values: new object[,]
                {
                    { 1, 1, null, 24.99m, 1 },
                    { 2, 2, null, 34.99m, 2 },
                    { 3, 3, null, 29.99m, 3 },
                    { 4, 4, null, 19.99m, 3 },
                    { 5, 1, 1, 19.99m, 1 },
                    { 6, 5, 1, 19.99m, 1 },
                    { 7, 2, 2, 29.99m, 2 },
                    { 8, 6, 2, 29.99m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "Id", "EndOfReservationDate", "PaymentAmount", "PaymentDate", "PaymentTypeId", "ReservationDate", "StartOfReservationDate", "TicketId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 6, 1, 0, 0, 0, DateTimeKind.Unspecified), 24.99m, new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "1" },
                    { 2, new DateTime(2024, 9, 7, 3, 0, 0, 0, DateTimeKind.Unspecified), 34.99m, new DateTime(2024, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "2" },
                    { 3, new DateTime(2024, 9, 8, 2, 0, 0, 0, DateTimeKind.Unspecified), 29.99m, new DateTime(2024, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "3" },
                    { 4, new DateTime(2024, 9, 9, 3, 0, 0, 0, DateTimeKind.Unspecified), 19.99m, new DateTime(2024, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "3" },
                    { 5, new DateTime(2024, 9, 6, 1, 0, 0, 0, DateTimeKind.Unspecified), 19.99m, new DateTime(2024, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "2" },
                    { 6, new DateTime(2024, 10, 6, 1, 0, 0, 0, DateTimeKind.Unspecified), 19.99m, new DateTime(2024, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "2" },
                    { 7, new DateTime(2024, 9, 7, 3, 0, 0, 0, DateTimeKind.Unspecified), 29.99m, new DateTime(2024, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "2" },
                    { 8, new DateTime(2024, 10, 8, 2, 0, 0, 0, DateTimeKind.Unspecified), 29.99m, new DateTime(2024, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "2" }
                });

            migrationBuilder.InsertData(
                table: "Reservation_Seat",
                columns: new[] { "ReservationId", "SeatId" },
                values: new object[,]
                {
                    { 1, 5 },
                    { 2, 9 },
                    { 3, 1 },
                    { 4, 13 },
                    { 5, 8 },
                    { 6, 8 },
                    { 7, 12 },
                    { 8, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "3" });

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
                keyColumns: new[] { "AdditionalServicesId", "HallRentId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "HallRent_AdditionalServices",
                keyColumns: new[] { "AdditionalServicesId", "HallRentId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "HallRent_AdditionalServices",
                keyColumns: new[] { "AdditionalServicesId", "HallRentId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "HallRent_AdditionalServices",
                keyColumns: new[] { "AdditionalServicesId", "HallRentId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "HallRent_AdditionalServices",
                keyColumns: new[] { "AdditionalServicesId", "HallRentId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "HallRent_AdditionalServices",
                keyColumns: new[] { "AdditionalServicesId", "HallRentId" },
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
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "Reservation_Seat",
                keyColumns: new[] { "ReservationId", "SeatId" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "Reservation_Seat",
                keyColumns: new[] { "ReservationId", "SeatId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "Reservation_Seat",
                keyColumns: new[] { "ReservationId", "SeatId" },
                keyValues: new object[] { 4, 13 });

            migrationBuilder.DeleteData(
                table: "Reservation_Seat",
                keyColumns: new[] { "ReservationId", "SeatId" },
                keyValues: new object[] { 5, 8 });

            migrationBuilder.DeleteData(
                table: "Reservation_Seat",
                keyColumns: new[] { "ReservationId", "SeatId" },
                keyValues: new object[] { 6, 8 });

            migrationBuilder.DeleteData(
                table: "Reservation_Seat",
                keyColumns: new[] { "ReservationId", "SeatId" },
                keyValues: new object[] { 7, 12 });

            migrationBuilder.DeleteData(
                table: "Reservation_Seat",
                keyColumns: new[] { "ReservationId", "SeatId" },
                keyValues: new object[] { 8, 3 });

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 4);

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
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UserData",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "UserData",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "UserData",
                keyColumn: "Id",
                keyValue: "3");

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
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

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
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 1);

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
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Seat",
                keyColumn: "Id",
                keyValue: 12);

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
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "EventDetails",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EventDetails",
                keyColumn: "Id",
                keyValue: 7);

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
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 7);

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
                table: "Event",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Festival",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Festival",
                keyColumn: "Id",
                keyValue: 2);

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
                table: "EventDetails",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FestivalDetails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FestivalDetails",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hall",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hall",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hall",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hall",
                keyColumn: "Id",
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

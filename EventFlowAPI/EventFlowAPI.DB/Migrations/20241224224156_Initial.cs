﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventFlowAPI.DB.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Price = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSoftUpdated = table.Column<bool>(type: "bit", nullable: false),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    PhotoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LongDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventPassType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ValidityPeriodInMonths = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    Price = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false),
                    RenewalDiscountPercentage = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSoftUpdated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPassType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQ", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FestivalDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LongDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FestivalDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HallType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    HallTypeGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSoftUpdated = table.Column<bool>(type: "bit", nullable: false),
                    PhotoName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HallType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaPatron",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MediaPatronGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaPatron", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewsGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LongDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrganizerGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartnerGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentTypeGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeatType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSoftUpdated = table.Column<bool>(type: "bit", nullable: false),
                    AddtionalPaymentPercentage = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    SeatColor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sponsor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SponsorGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketJPG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketJPG", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketPDF",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPDF", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSoftUpdated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "UserData",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HouseNumber = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: true),
                    FlatNumber = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserData_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Festival",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AddDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FestivalGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Duration = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhotoName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Festival", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Festival_FestivalDetails_Id",
                        column: x => x.Id,
                        principalTable: "FestivalDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hall",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefaultId = table.Column<int>(type: "int", nullable: true),
                    HallNr = table.Column<int>(type: "int", nullable: false),
                    RentalPricePerHour = table.Column<decimal>(type: "NUMERIC(5,2)", nullable: false),
                    IsCopy = table.Column<bool>(type: "bit", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Floor = table.Column<decimal>(type: "NUMERIC(1,0)", nullable: false),
                    HallViewFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HallTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hall", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hall_HallType_HallTypeId",
                        column: x => x.HallTypeId,
                        principalTable: "HallType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HallType_Equipment",
                columns: table => new
                {
                    HallTypeId = table.Column<int>(type: "int", nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HallType_Equipment", x => new { x.EquipmentId, x.HallTypeId });
                    table.ForeignKey(
                        name: "FK_HallType_Equipment_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HallType_Equipment_HallType_HallTypeId",
                        column: x => x.HallTypeId,
                        principalTable: "HallType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventPass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventPassGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RenewalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreviousEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false),
                    TotalDiscountPercentage = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "NUMERIC(5,2)", nullable: false),
                    EventPassJPGName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventPassPDFName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassTypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventPass_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventPass_EventPassType_PassTypeId",
                        column: x => x.PassTypeId,
                        principalTable: "EventPassType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventPass_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Festival_MediaPatron",
                columns: table => new
                {
                    FestivalId = table.Column<int>(type: "int", nullable: false),
                    MediaPatronId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Festival_MediaPatron", x => new { x.FestivalId, x.MediaPatronId });
                    table.ForeignKey(
                        name: "FK_Festival_MediaPatron_Festival_FestivalId",
                        column: x => x.FestivalId,
                        principalTable: "Festival",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Festival_MediaPatron_MediaPatron_MediaPatronId",
                        column: x => x.MediaPatronId,
                        principalTable: "MediaPatron",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Festival_Organizer",
                columns: table => new
                {
                    FestivalId = table.Column<int>(type: "int", nullable: false),
                    OrganizerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Festival_Organizer", x => new { x.FestivalId, x.OrganizerId });
                    table.ForeignKey(
                        name: "FK_Festival_Organizer_Festival_FestivalId",
                        column: x => x.FestivalId,
                        principalTable: "Festival",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Festival_Organizer_Organizer_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "Organizer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Festival_Sponsor",
                columns: table => new
                {
                    FestivalId = table.Column<int>(type: "int", nullable: false),
                    SponsorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Festival_Sponsor", x => new { x.FestivalId, x.SponsorId });
                    table.ForeignKey(
                        name: "FK_Festival_Sponsor_Festival_FestivalId",
                        column: x => x.FestivalId,
                        principalTable: "Festival",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Festival_Sponsor_Sponsor_SponsorId",
                        column: x => x.SponsorId,
                        principalTable: "Sponsor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AddDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EventGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    HallId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_EventCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "EventCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_EventDetails_Id",
                        column: x => x.Id,
                        principalTable: "EventDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_Hall_HallId",
                        column: x => x.HallId,
                        principalTable: "Hall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HallDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TotalLength = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    TotalWidth = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    TotalArea = table.Column<decimal>(type: "NUMERIC(5,2)", nullable: false),
                    StageLength = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: true),
                    StageWidth = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: true),
                    NumberOfSeatsRows = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    MaxNumberOfSeatsRows = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    NumberOfSeatsColumns = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    MaxNumberOfSeatsColumns = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    NumberOfSeats = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: false),
                    MaxNumberOfSeats = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HallDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HallDetails_Hall_Id",
                        column: x => x.Id,
                        principalTable: "Hall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HallRent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HallRentGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<long>(type: "bigint", nullable: false),
                    RentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentAmount = table.Column<decimal>(type: "NUMERIC(7,2)", nullable: false),
                    HallRentPDFName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    HallId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HallRent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HallRent_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HallRent_Hall_HallId",
                        column: x => x.HallId,
                        principalTable: "Hall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HallRent_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatNr = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: false),
                    Row = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    GridRow = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    Column = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    GridColumn = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    SeatTypeId = table.Column<int>(type: "int", nullable: false),
                    HallId = table.Column<int>(type: "int", nullable: false),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seat_Hall_HallId",
                        column: x => x.HallId,
                        principalTable: "Hall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seat_SeatType_SeatTypeId",
                        column: x => x.SeatTypeId,
                        principalTable: "SeatType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Festival_Event",
                columns: table => new
                {
                    FestivalId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Festival_Event", x => new { x.FestivalId, x.EventId });
                    table.ForeignKey(
                        name: "FK_Festival_Event_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Festival_Event_Festival_FestivalId",
                        column: x => x.FestivalId,
                        principalTable: "Festival",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "NUMERIC(5,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TicketTypeId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    FestivalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Festival_FestivalId",
                        column: x => x.FestivalId,
                        principalTable: "Festival",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_TicketType_TicketTypeId",
                        column: x => x.TicketTypeId,
                        principalTable: "TicketType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HallRent_AdditionalServices",
                columns: table => new
                {
                    HallRentId = table.Column<int>(type: "int", nullable: false),
                    AdditionalServicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HallRent_AdditionalServices", x => new { x.HallRentId, x.AdditionalServicesId });
                    table.ForeignKey(
                        name: "FK_HallRent_AdditionalServices_AdditionalServices_AdditionalServicesId",
                        column: x => x.AdditionalServicesId,
                        principalTable: "AdditionalServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HallRent_AdditionalServices_HallRent_HallRentId",
                        column: x => x.HallRentId,
                        principalTable: "HallRent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsFestivalReservation = table.Column<bool>(type: "bit", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAddtionalPaymentPercentage = table.Column<decimal>(type: "NUMERIC(5,2)", nullable: false),
                    TotalAdditionalPaymentAmount = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "NUMERIC(7,2)", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "NUMERIC(7,2)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    EventPassId = table.Column<int>(type: "int", nullable: true),
                    TicketPDFId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservation_EventPass_EventPassId",
                        column: x => x.EventPassId,
                        principalTable: "EventPass",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservation_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservation_TicketPDF_TicketPDFId",
                        column: x => x.TicketPDFId,
                        principalTable: "TicketPDF",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservation_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation_Seat",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation_Seat", x => new { x.ReservationId, x.SeatId });
                    table.ForeignKey(
                        name: "FK_Reservation_Seat_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservation_Seat_Seat_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seat",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reservation_TicketJPG",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    TicketJPGId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation_TicketJPG", x => new { x.ReservationId, x.TicketJPGId });
                    table.ForeignKey(
                        name: "FK_Reservation_TicketJPG_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservation_TicketJPG_TicketJPG_TicketJPGId",
                        column: x => x.TicketJPGId,
                        principalTable: "TicketJPG",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AdditionalServices",
                columns: new[] { "Id", "DeleteDate", "Description", "IsDeleted", "IsSoftUpdated", "IsUpdated", "Name", "Price", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, false, false, false, "DJ", 400.00m, null },
                    { 2, null, null, false, false, false, "Obsługa oświetlenia", 340.00m, null },
                    { 3, null, null, false, false, false, "Obsługa nagłośnienia", 250.00m, null },
                    { 4, null, null, false, false, false, "Fotograf", 200.00m, null },
                    { 5, null, null, false, false, false, "Promocja wydarzenia", 140.00m, null }
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
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "IsVerified", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoName", "Provider", "RegisteredDate", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "e5007695-3033-40a8-a83e-8cc807021ac7", new DateTime(2000, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", true, true, false, null, "Admin", "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEL68tc0iHCRjCTvP2hnOVt4YxFuqzbEgY+NZ9Izn1y367KgC6qg4fgyB4qjuzEfY4A==", null, false, "admin.jpg", "APP", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "8d278a61-7ad9-41ce-9774-623dc9971ad6", "Admin", false, "admin@gmail.com" },
                    { "2", 0, "56af4507-647d-4053-87a9-59f50db1a326", new DateTime(1985, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mateusz.strapczuk2@gmail.com", true, true, false, null, "Mateusz2", "MATEUSZ.STRAPCZUK2@GMAIL.COM", "MATEUSZ.STRAPCZUK2@GMAIL.COM", "AQAAAAIAAYagAAAAEJhAsI0ZwBp340ec02AGAgs7UVjclALaj9+7uNajpvm9MO23jO3OEHnwNHSXrz+xDw==", null, false, "user2.jpg", "APP", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "67208ac8-2e12-4013-b2ee-c46ddbdc6eda", "Strapczuk2", false, "mateusz.strapczuk2@gmail.com" },
                    { "3", 0, "191ebd1d-da96-4b4f-a69f-e473b21d51c0", new DateTime(1979, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "mateusz.strapczuk3@gmail.com", true, true, false, null, "Mateusz3", "MATEUSZ.STRAPCZUK3@GMAIL.COM", "MATEUSZ.STRAPCZUK3@GMAIL.COM", "AQAAAAIAAYagAAAAEG5NvyB+rOy8A4Ke2azM5/TBlvbBVvRP4WJA4ICQOl1gVaFynSgLxe6WOJtVq57aZA==", null, false, "user3.jpg", "APP", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "eca1c680-8fdf-4c90-b740-95a9862e7872", "Strapczuk3", false, "mateusz.strapczuk3@gmail.com" },
                    { "4", 0, "e8d8446c-30a3-44bb-992f-895f4815f29e", new DateTime(1979, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "mateusz.strapczuk4@gmail.com", true, true, false, null, "Mateusz4", "MATEUSZ.STRAPCZUK4@GMAIL.COM", "MATEUSZ.STRAPCZUK4@GMAIL.COM", "AQAAAAIAAYagAAAAEB8DHvtkDXnhpPJWVU32z3otQa8EykO5zR9RMc5uJTqLOzvJPG3MEE81YCAzgbAvJw==", null, false, "user4.jpg", "APP", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "196b3136-7323-4a02-bb3b-ca294602e4b4", "Strapczuk4", false, "mateusz.strapczuk4@gmail.com" },
                    { "5", 0, "a8b29b82-6ae9-4d15-bf1d-89474a91fefb", new DateTime(1979, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "mateusz.strapczuk1@gmail.com", true, true, false, null, "Mateusz", "MATEUSZ.STRAPCZUK1@GMAIL.COM", "MATEUSZ.STRAPCZUK1@GMAIL.COM", "AQAAAAIAAYagAAAAENuA7HIQYiLSFJsUdTVMZH9KkB/NXGFTMEg0qviH3Q5LEUiThz7liILsR+jgmN/0rw==", null, false, "user5.jpg", "APP", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "f56bb341-a37a-4b48-be4d-119a6047208e", "Strapczuk", false, "mateusz.strapczuk1@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Equipment",
                columns: new[] { "Id", "Description", "IsUpdated", "Name", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Nowoczesny projektor", false, "Projektor multimedialny", null },
                    { 2, "Wysokiej klasy oświetlenie", false, "Oświetlenie", null },
                    { 3, "Głośniki przeznaczone do odtwrzania filmów", false, "Nagłośnienie kinowe", null },
                    { 4, "Głośniki przeznaczone do koncertów", false, "Nagłośnienie koncertowe", null }
                });

            migrationBuilder.InsertData(
                table: "EventCategory",
                columns: new[] { "Id", "Color", "DeleteDate", "Icon", "IsDeleted", "IsUpdated", "Name", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "#82CAFC", null, "fa-solid fa-music", false, false, "Koncert", null },
                    { 2, "#6BD49B", null, "fa-solid fa-film", false, false, "Film", null },
                    { 3, "#C33EB1", null, "fa-solid fa-masks-theater", false, false, "Spektakl", null },
                    { 4, "#FC5353", null, "fa-solid fa-landmark", false, false, "Wystawa", null }
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
                columns: new[] { "Id", "DeleteDate", "IsDeleted", "IsSoftUpdated", "IsUpdated", "Name", "Price", "RenewalDiscountPercentage", "UpdateDate", "ValidityPeriodInMonths" },
                values: new object[,]
                {
                    { 1, null, false, false, false, "Karnet miesięczny", 89.99m, 5m, null, 1m },
                    { 2, null, false, false, false, "Karnet kwartalny", 235.99m, 10m, null, 3m },
                    { 3, null, false, false, false, "Karnet półroczny", 499.99m, 15m, null, 6m },
                    { 4, null, false, false, false, "Karnet roczny", 999.99m, 20m, null, 12m }
                });

            migrationBuilder.InsertData(
                table: "FAQ",
                columns: new[] { "Id", "Answer", "Question" },
                values: new object[,]
                {
                    { 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed justo erat, tempor quis sagittis et, sodales id nibh. Phasellus diam enim, sodales pharetra neque eget, sollicitudin vulputate est. Morbi ac velit sed arcu malesuada feugiat. Suspendisse potenti. Donec commodo mauris nisi. Nulla ut mi eu lectus consequat porta at commodo.", "W jaki sposób mogę kupić bilet na wydarzenie ?" },
                    { 2, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed justo erat, tempor quis sagittis et, sodales id nibh. Phasellus diam enim, sodales pharetra neque eget, sollicitudin vulputate est. Morbi ac velit sed arcu malesuada feugiat. Suspendisse potenti. Donec commodo mauris nisi. Nulla ut mi eu lectus consequat porta at commodo.", "Jak mogę wynająć salę ?" },
                    { 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed justo erat, tempor quis sagittis et, sodales id nibh. Phasellus diam enim, sodales pharetra neque eget, sollicitudin vulputate est. Morbi ac velit sed arcu malesuada feugiat. Suspendisse potenti. Donec commodo mauris nisi. Nulla ut mi eu lectus consequat porta at commodo.", "Co ile czasu muszę przedłużać karnet ?" },
                    { 4, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed justo erat, tempor quis sagittis et, sodales id nibh. Phasellus diam enim, sodales pharetra neque eget, sollicitudin vulputate est. Morbi ac velit sed arcu malesuada feugiat. Suspendisse potenti. Donec commodo mauris nisi. Nulla ut mi eu lectus consequat porta at commodo.", "Jak zwrócić zakupiony bilet ?" }
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
                columns: new[] { "Id", "DeleteDate", "Description", "HallTypeGuid", "IsDeleted", "IsSoftUpdated", "IsUpdated", "Name", "PhotoName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, "Nowa sala kinowa wyposażona w nowoczesne nagłośnienie i ekran", new Guid("00000000-0000-0000-0000-000000000000"), false, false, false, "Sala filmowa", "", null },
                    { 2, null, "Nowa sala koncertowa wyposażona w najwyższej klasy nagłośnienie", new Guid("00000000-0000-0000-0000-000000000000"), false, false, false, "Sala koncertowa", "", null },
                    { 3, null, "Opis sali teatralnej", new Guid("00000000-0000-0000-0000-000000000000"), false, false, false, "Sala teatralna", "", null },
                    { 4, null, "Opis sali wystawowa", new Guid("00000000-0000-0000-0000-000000000000"), false, false, false, "Sala wystawowa", "", null },
                    { 5, null, "Sala ogólna", new Guid("00000000-0000-0000-0000-000000000000"), false, false, false, "Sala ogólna", "", null }
                });

            migrationBuilder.InsertData(
                table: "MediaPatron",
                columns: new[] { "Id", "DeleteDate", "IsDeleted", "IsUpdated", "MediaPatronGuid", "Name", "PhotoName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, false, false, new Guid("99489123-b8f8-4a5f-b403-9ba844d4da7b"), "Gazeta Nowoczesna", "", null },
                    { 2, null, false, false, new Guid("7979eb21-7961-4d55-a627-39a03e586581"), "Nowy świat TV", "", null },
                    { 3, null, false, false, new Guid("977f04f3-7139-43a4-888d-6fa1a5eddc7a"), "Tygodnik Nowiny", "", null }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "LongDescription", "NewsGuid", "PhotoName", "PublicationDate", "ShortDescription", "Title" },
                values: new object[,]
                {
                    { 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus velit ac, feugiat turpis. Vestibulum fermentum placerat mi nec scelerisque. Ut id nibh ornare, luctus velit ac, feugiat turpis. Vestibulum fermentum. Vestibulum fermentum placerat mi nec. Ut id nibh ornare, luctus velit ac, feugiat turpis.Vestibulum fermentum. Vestibulum fermentum.", new Guid("a9f8d848-a091-4dbf-b5dd-00a74f519f44"), "konkurs_artystyczny.png", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus velit ac, feugiat turpis. Vestibulum fermentum placerat mi nec scelerisque. Ut id nibh ornare, luctus velit ac, feugiat turpis. Vestibulum fermentum. Vestibulum fermentum placerat mi nec. Ut id nibh ornare, luctus velit ac, feugiat turpis.Vestibulum fermentum. Vestibulum fermentum.", "Finał konkursu artystycznego" },
                    { 2, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", new Guid("886df0a0-0cbe-41d9-b8b6-7171504dbef2"), "koncert_lunar_vibes.png", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", "Relacja z koncertu zespołu Lunar Vibes" },
                    { 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", new Guid("af95dc53-3cc1-4010-9174-92100f78fa53"), "modernizacja sali.png", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", "Modernizacja sali koncertowej" },
                    { 4, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", new Guid("21151156-37dc-4cae-aa83-6756b531a3d7"), "znizka.png", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", "Zniżka 20% na zakup karnetów" },
                    { 5, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", new Guid("d298f5eb-38a9-4628-ba48-c0cad76a2c76"), "noc_filmowa.png", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", "Noc Filmowa z Klasykami Kina" },
                    { 6, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", new Guid("8007bd09-9e0e-4a97-91de-8f0c5f86cf99"), "wernisaz.png", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", "Wernisaż: Nowe inspiracje" }
                });

            migrationBuilder.InsertData(
                table: "Organizer",
                columns: new[] { "Id", "DeleteDate", "IsDeleted", "IsUpdated", "Name", "OrganizerGuid", "PhotoName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, false, false, "EventFlow", new Guid("f93b8879-d1ce-4b96-b6db-03e2851c2140"), "", null },
                    { 2, null, false, false, "Snowflake", new Guid("0b566824-8a39-499e-b06b-6edabadafa83"), "", null },
                    { 3, null, false, false, "Aura", new Guid("06d7bcb3-2806-4f7e-8453-4d0d881e1fab"), "", null }
                });

            migrationBuilder.InsertData(
                table: "Partner",
                columns: new[] { "Id", "Name", "PartnerGuid", "PhotoName" },
                values: new object[,]
                {
                    { 1, "Basel", new Guid("4d1c3ab3-1404-44ce-a4f9-c58481cc20e2"), "basel.png" },
                    { 2, "Aura", new Guid("5637c837-cb37-4aa3-b0bd-226009aba939"), "aura.png" },
                    { 3, "Vision", new Guid("4d9d801e-5a02-4df1-a81d-60f4cec604a1"), "vision.png" },
                    { 4, "Snowflake", new Guid("c4ee5f31-5160-4929-a3ad-a839aa253f7d"), "snowflake.png" },
                    { 5, "Waveless", new Guid("381c1c43-963b-402f-9775-5006b12a3ded"), "waveless.png" }
                });

            migrationBuilder.InsertData(
                table: "PaymentType",
                columns: new[] { "Id", "DeleteDate", "IsDeleted", "IsUpdated", "Name", "PaymentTypeGuid", "PhotoName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, false, false, "PayU", new Guid("3f891ecf-fb99-4cbd-aa93-2b8629a3681d"), "", null },
                    { 2, null, false, false, "Karnet", new Guid("6b00c94d-5d7e-42cd-9d6a-16e8ea21bfdd"), "", null }
                });

            migrationBuilder.InsertData(
                table: "SeatType",
                columns: new[] { "Id", "AddtionalPaymentPercentage", "DeleteDate", "Description", "IsDeleted", "IsSoftUpdated", "IsUpdated", "Name", "SeatColor", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 25.00m, null, "Opis miejsca VIP", false, false, false, "Miejsce VIP", "#9803fc", null },
                    { 2, 10.00m, null, "Opis miejsca klasy premium", false, false, false, "Miejsce klasy premium", "#ffa600", null },
                    { 3, 0.00m, null, "Opis miejsca zwykłego", false, false, false, "Miejsce zwykłe", "#039aff", null }
                });

            migrationBuilder.InsertData(
                table: "Sponsor",
                columns: new[] { "Id", "DeleteDate", "IsDeleted", "IsUpdated", "Name", "PhotoName", "SponsorGuid", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, false, false, "Basel", "", new Guid("6371e0fe-ff18-46d5-b171-9404c72a24a9"), null },
                    { 2, null, false, false, "Vision", "", new Guid("0e3e8865-1061-4a2f-a4cf-96818a7bd19b"), null },
                    { 3, null, false, false, "Waveless", "", new Guid("36dcc521-bae7-494b-86d3-a43d94807adf"), null }
                });

            migrationBuilder.InsertData(
                table: "TicketJPG",
                columns: new[] { "Id", "FileName", "ReservationGuid" },
                values: new object[,]
                {
                    { 1, "eventflow_bilet_test_53538b58-f885-4f4a-b675-a4aa4063ccf3.jpg", new Guid("53538b58-f885-4f4a-b675-a4aa4063ccf3") },
                    { 2, "eventflow_bilet_test_ed8b9230-223b-4609-8d13-aa6017edad09.jpg", new Guid("ed8b9230-223b-4609-8d13-aa6017edad09") },
                    { 3, "eventflow_bilet_test_f9a076c4-3475-4a28-a60c-6e0e3c03731a.jpg", new Guid("f9a076c4-3475-4a28-a60c-6e0e3c03731a") },
                    { 4, "eventflow_bilet_test_de1d6773-f027-4888-996a-0296e5c52708.jpg", new Guid("de1d6773-f027-4888-996a-0296e5c52708") },
                    { 5, "eventflow_bilet_test_0b74ec7b-933b-4163-afa5-e0997681dccd_1.jpg", new Guid("0b74ec7b-933b-4163-afa5-e0997681dccd") },
                    { 6, "eventflow_bilet_test_0b74ec7b-933b-4163-afa5-e0997681dccd_2.jpg", new Guid("0b74ec7b-933b-4163-afa5-e0997681dccd") },
                    { 7, "eventflow_bilet_test_806cade1-2685-43dc-8cfc-682fc4229db6_1.jpg", new Guid("806cade1-2685-43dc-8cfc-682fc4229db6") },
                    { 8, "eventflow_bilet_test_806cade1-2685-43dc-8cfc-682fc4229db6_2.jpg", new Guid("806cade1-2685-43dc-8cfc-682fc4229db6") }
                });

            migrationBuilder.InsertData(
                table: "TicketPDF",
                columns: new[] { "Id", "FileName", "ReservationGuid" },
                values: new object[,]
                {
                    { 1, "eventflow_bilet_test_53538b58-f885-4f4a-b675-a4aa4063ccf3.pdf", new Guid("53538b58-f885-4f4a-b675-a4aa4063ccf3") },
                    { 2, "eventflow_bilet_test_ed8b9230-223b-4609-8d13-aa6017edad09.pdf", new Guid("ed8b9230-223b-4609-8d13-aa6017edad09") },
                    { 3, "eventflow_bilet_test_f9a076c4-3475-4a28-a60c-6e0e3c03731a.pdf", new Guid("f9a076c4-3475-4a28-a60c-6e0e3c03731a") },
                    { 4, "eventflow_bilet_test_de1d6773-f027-4888-996a-0296e5c52708.pdf", new Guid("de1d6773-f027-4888-996a-0296e5c52708") },
                    { 5, "eventflow_bilet_test_0b74ec7b-933b-4163-afa5-e0997681dccd.pdf", new Guid("0b74ec7b-933b-4163-afa5-e0997681dccd") },
                    { 6, "eventflow_bilet_test_806cade1-2685-43dc-8cfc-682fc4229db6.pdf", new Guid("806cade1-2685-43dc-8cfc-682fc4229db6") }
                });

            migrationBuilder.InsertData(
                table: "TicketType",
                columns: new[] { "Id", "DeleteDate", "IsDeleted", "IsSoftUpdated", "IsUpdated", "Name", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, false, false, false, "Bilet normalny", null },
                    { 2, null, false, false, false, "Bilet ulgowy", null },
                    { 3, null, false, false, false, "Bilet rodzinny", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "2" },
                    { "2", "3" },
                    { "2", "4" },
                    { "2", "5" }
                });

            migrationBuilder.InsertData(
                table: "EventPass",
                columns: new[] { "Id", "DeleteDate", "EndDate", "EventPassGuid", "EventPassJPGName", "EventPassPDFName", "IsDeleted", "IsUpdated", "PassTypeId", "PaymentAmount", "PaymentDate", "PaymentTypeId", "PreviousEndDate", "RenewalDate", "StartDate", "TotalDiscount", "TotalDiscountPercentage", "UpdateDate", "UserId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b00ca94a-e6b2-4d2e-b270-244b3e76048d"), "eventflow_karnet_b00ca94a-e6b2-4d2e-b270-244b3e76048d.jpg", "eventflow_karnet_b00ca94a-e6b2-4d2e-b270-244b3e76048d.pdf", false, false, 3, 499.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, null, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 0m, null, "2" },
                    { 2, null, new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("766245b4-8c08-49dd-9480-2606aaa590be"), "eventflow_karnet_766245b4-8c08-49dd-9480-2606aaa590be.jpg", "eventflow_karnet_766245b4-8c08-49dd-9480-2606aaa590be.pdf", false, false, 4, 999.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, null, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 0m, null, "3" },
                    { 3, null, new DateTime(2025, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33610a0d-a1b7-4700-bffe-9e334b977e6a"), "eventflow_karnet_33610a0d-a1b7-4700-bffe-9e334b977e6a.jpg", "eventflow_karnet_33610a0d-a1b7-4700-bffe-9e334b977e6a.pdf", false, false, 2, 235.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, null, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 0m, null, "4" }
                });

            migrationBuilder.InsertData(
                table: "Festival",
                columns: new[] { "Id", "AddDate", "DeleteDate", "Duration", "EndDate", "FestivalGuid", "IsDeleted", "IsUpdated", "Name", "PhotoName", "ShortDescription", "StartDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, -2768400L, new DateTime(2025, 1, 26, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6d3a8af5-30c8-4cc2-9e73-24f5c989d495"), false, false, "Festiwal muzyki hip-hop", "festival.png", "Festiwal muzyki hip-hop to nowy festiwal organizowany przez XYZ.", new DateTime(2024, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, -2772000L, new DateTime(2025, 1, 28, 2, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1734fad9-156b-4119-be9f-8ed06bedb8b0"), false, false, "Festiwal filmowy", "", "Festiwal filmowy to festiwal na którym można obejrzeć filmy.", new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, -2689200L, new DateTime(2025, 1, 29, 3, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6b291628-8222-4b3c-97e3-3466d5a37943"), false, false, "Festiwal sztuki abstrakcyjnej", "", "Festiwal sztuki abstrakcyjnej to festiwal na którym można zobaczyć sztukę.", new DateTime(2024, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Hall",
                columns: new[] { "Id", "DefaultId", "Floor", "HallNr", "HallTypeId", "HallViewFileName", "IsCopy", "IsUpdated", "IsVisible", "RentalPricePerHour", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, 2m, 1, 1, "sala_5_5.pdf", false, false, true, 120.99m, null },
                    { 2, 2, 1m, 2, 2, "sala_5_5.pdf", false, false, true, 89.99m, null },
                    { 3, 3, 2m, 3, 3, "sala_5_5.pdf", false, false, true, 179.99m, null },
                    { 4, 4, 1m, 4, 4, "sala_5_5.pdf", false, false, true, 199.99m, null },
                    { 5, 1, 2m, 1, 1, "sala_5_5.pdf", true, false, false, 120.99m, null },
                    { 6, 2, 1m, 2, 2, "sala_5_5.pdf", true, false, false, 89.99m, null },
                    { 7, 3, 2m, 3, 3, "sala_5_5.pdf", true, false, false, 179.99m, null },
                    { 8, 4, 1m, 4, 4, "sala_5_5.pdf", true, false, false, 199.99m, null },
                    { 9, 1, 2m, 1, 1, "sala_5_5.pdf", true, false, false, 120.99m, null },
                    { 10, 2, 1m, 2, 2, "sala_5_5.pdf", true, false, false, 89.99m, null },
                    { 11, 4, 1m, 4, 4, "sala_5_5.pdf", true, false, false, 199.99m, null },
                    { 12, 1, 2m, 1, 1, "sala_5_5.pdf", true, false, false, 120.99m, null },
                    { 13, 2, 1m, 2, 2, "sala_5_5.pdf", true, false, false, 89.99m, null },
                    { 14, 3, 2m, 3, 3, "sala_5_5.pdf", true, false, false, 179.99m, null },
                    { 15, 4, 1m, 4, 4, "sala_5_5.pdf", true, false, false, 199.99m, null }
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
                    { "3", "Białystok", 21m, 7m, "147852369", "Pogodna", "14-453" },
                    { "4", "Warszawa", 42m, 21m, "147852369", "Słoneczna", "14-453" }
                });

            migrationBuilder.InsertData(
                table: "Event",
                columns: new[] { "Id", "AddDate", "CategoryId", "DeleteDate", "Duration", "EndDate", "EventGuid", "HallId", "IsDeleted", "IsUpdated", "Name", "PhotoName", "ShortDescription", "StartDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, -3600L, new DateTime(2024, 12, 26, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("95ddf410-c9c7-48a3-a6f7-7e90c2721d8d"), 6, false, false, "Koncert: Mystic Waves", "koncert_mystic_waves.png", "Któtki opis koncertu Mystic Waves.", new DateTime(2024, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, -10800L, new DateTime(2024, 12, 27, 3, 0, 0, 0, DateTimeKind.Unspecified), new Guid("39f0c1dc-6825-450d-83c3-f64e411ee54a"), 7, false, false, "Cień Przeszłości", "cien_przeszlosci.png", "Krótki opis spektaklu pt. Cień Przeszłości.", new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, -7200L, new DateTime(2024, 12, 28, 2, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1bba1b54-76fb-4c31-bc03-d5107d292ec2"), 5, false, false, "Królestwo planety małp", "krolestwo_planety_malp.png", "Nowy film Królestwo planety małp już w kinach!.", new DateTime(2024, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, -10800L, new DateTime(2024, 12, 29, 3, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c5d14792-7422-435a-9a3b-df2157761167"), 8, false, false, "Nowe inspiracje", "nowe_inspiracje.png", "Nowe inspiracje to nowoczesna wystawa sztuki.", new DateTime(2024, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, -3600L, new DateTime(2025, 1, 26, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("af936f2c-7363-4165-acd2-5a96483bd445"), 10, false, false, "Koncert: New Era", "", "Jedyna taka okazja na usłyszenie New Era na żywo.", new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, -7200L, new DateTime(2025, 1, 28, 2, 0, 0, 0, DateTimeKind.Unspecified), new Guid("577b3ec9-6040-4a22-9f12-8b198da92646"), 9, false, false, "Gladiator", "", "Nowy film Gladiator już w kinach!.", new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, -10800L, new DateTime(2025, 1, 29, 3, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0135ff06-888b-4363-88fb-bb4fce13486a"), 11, false, false, "Nowa sztuka", "", "Nowe sztuka to nowoczesna wystawa sztuki.", new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
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
                table: "HallDetails",
                columns: new[] { "Id", "MaxNumberOfSeats", "MaxNumberOfSeatsColumns", "MaxNumberOfSeatsRows", "NumberOfSeats", "NumberOfSeatsColumns", "NumberOfSeatsRows", "StageLength", "StageWidth", "TotalArea", "TotalLength", "TotalWidth" },
                values: new object[,]
                {
                    { 1, 255m, 15m, 15m, 100m, 10m, 10m, 4m, 20m, 800m, 20m, 40m },
                    { 2, 150m, 10m, 15m, 150m, 10m, 15m, null, null, 150m, 15m, 10m },
                    { 3, 60m, 10m, 6m, 60m, 10m, 6m, 4m, 5m, 80m, 10m, 8m },
                    { 4, 100m, 10m, 10m, 100m, 10m, 10m, 5m, 8m, 140m, 14m, 10m },
                    { 5, 255m, 15m, 15m, 100m, 10m, 10m, 4m, 20m, 800m, 20m, 40m },
                    { 6, 150m, 10m, 15m, 150m, 10m, 15m, null, null, 150m, 15m, 10m },
                    { 7, 60m, 10m, 6m, 60m, 10m, 6m, 4m, 5m, 80m, 10m, 8m },
                    { 8, 100m, 10m, 10m, 100m, 10m, 10m, 5m, 8m, 140m, 14m, 10m },
                    { 9, 255m, 15m, 15m, 100m, 10m, 10m, 4m, 20m, 800m, 20m, 40m },
                    { 10, 150m, 10m, 15m, 150m, 10m, 15m, null, null, 150m, 15m, 10m },
                    { 11, 100m, 10m, 10m, 100m, 10m, 10m, 5m, 8m, 140m, 14m, 10m },
                    { 12, 255m, 15m, 15m, 100m, 10m, 10m, 4m, 20m, 800m, 20m, 40m },
                    { 13, 150m, 10m, 15m, 150m, 10m, 15m, null, null, 150m, 15m, 10m },
                    { 14, 60m, 10m, 6m, 60m, 10m, 6m, 4m, 5m, 80m, 10m, 8m },
                    { 15, 100m, 10m, 10m, 100m, 10m, 10m, 5m, 8m, 140m, 14m, 10m }
                });

            migrationBuilder.InsertData(
                table: "HallRent",
                columns: new[] { "Id", "DeleteDate", "Duration", "EndDate", "HallId", "HallRentGuid", "HallRentPDFName", "IsDeleted", "IsUpdated", "PaymentAmount", "PaymentDate", "PaymentTypeId", "RentDate", "StartDate", "UpdateDate", "UserId" },
                values: new object[,]
                {
                    { 1, null, 28800L, new DateTime(2024, 12, 26, 8, 0, 0, 0, DateTimeKind.Unspecified), 12, new Guid("8a7a0a87-baf7-4793-b4b4-81d5a3cf8704"), "wynajem_sali_f1f1e0e3-fd2b-4188-bcb0-ce1b924258d8.pdf", false, false, 899.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "4" },
                    { 2, null, 14400L, new DateTime(2024, 12, 27, 4, 0, 0, 0, DateTimeKind.Unspecified), 13, new Guid("38652902-a88e-4998-a4a7-cdf4b76b1532"), "wynajem_sali_f1f1e0e3-fd2b-4188-bcb0-ce1b924258d8.pdf", false, false, 699.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "3" },
                    { 3, null, 7200L, new DateTime(2024, 12, 28, 2, 0, 0, 0, DateTimeKind.Unspecified), 14, new Guid("8313eab6-74e2-448b-af9f-8d73df7db222"), "wynajem_sali_f1f1e0e3-fd2b-4188-bcb0-ce1b924258d8.pdf", false, false, 399.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "3" },
                    { 4, null, 3600L, new DateTime(2024, 12, 29, 1, 0, 0, 0, DateTimeKind.Unspecified), 15, new Guid("85631c1c-750b-48ae-9486-a6c43a1dd47b"), "wynajem_sali_f1f1e0e3-fd2b-4188-bcb0-ce1b924258d8.pdf", false, false, 150.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "2" }
                });

            migrationBuilder.InsertData(
                table: "Seat",
                columns: new[] { "Id", "Column", "GridColumn", "GridRow", "HallId", "IsUpdated", "Row", "SeatNr", "SeatTypeId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1m, 1m, 1m, 1, false, 1m, 1m, 1, null },
                    { 2, 2m, 2m, 1m, 1, false, 1m, 2m, 1, null },
                    { 3, 3m, 3m, 1m, 1, false, 1m, 3m, 1, null },
                    { 4, 4m, 4m, 1m, 1, false, 1m, 4m, 1, null },
                    { 5, 1m, 1m, 1m, 2, false, 1m, 1m, 2, null },
                    { 6, 2m, 2m, 1m, 2, false, 1m, 2m, 2, null },
                    { 7, 3m, 3m, 1m, 2, false, 1m, 3m, 1, null },
                    { 8, 4m, 4m, 1m, 2, false, 1m, 4m, 2, null },
                    { 9, 1m, 1m, 1m, 3, false, 1m, 1m, 3, null },
                    { 10, 2m, 2m, 1m, 3, false, 1m, 2m, 3, null },
                    { 11, 3m, 3m, 1m, 3, false, 1m, 3m, 3, null },
                    { 12, 4m, 4m, 1m, 3, false, 1m, 4m, 3, null },
                    { 13, 1m, 1m, 1m, 4, false, 1m, 1m, 3, null },
                    { 14, 1m, 1m, 1m, 5, false, 1m, 1m, 1, null },
                    { 15, 2m, 2m, 1m, 5, false, 1m, 2m, 1, null },
                    { 16, 3m, 3m, 1m, 5, false, 1m, 3m, 1, null },
                    { 17, 4m, 4m, 1m, 5, false, 1m, 4m, 1, null },
                    { 18, 1m, 1m, 1m, 9, false, 1m, 1m, 1, null },
                    { 19, 2m, 2m, 1m, 9, false, 1m, 2m, 1, null },
                    { 20, 3m, 3m, 1m, 9, false, 1m, 3m, 1, null },
                    { 21, 4m, 4m, 1m, 9, false, 1m, 4m, 1, null },
                    { 22, 1m, 1m, 1m, 12, false, 1m, 1m, 1, null },
                    { 23, 2m, 2m, 1m, 12, false, 1m, 2m, 1, null },
                    { 24, 3m, 3m, 1m, 12, false, 1m, 3m, 1, null },
                    { 25, 4m, 4m, 1m, 12, false, 1m, 4m, 1, null },
                    { 26, 1m, 1m, 1m, 6, false, 1m, 1m, 2, null },
                    { 27, 2m, 2m, 1m, 6, false, 1m, 2m, 2, null },
                    { 28, 3m, 3m, 1m, 6, false, 1m, 3m, 1, null },
                    { 29, 4m, 4m, 1m, 6, false, 1m, 4m, 2, null },
                    { 30, 1m, 1m, 1m, 10, false, 1m, 1m, 2, null },
                    { 31, 2m, 2m, 1m, 10, false, 1m, 2m, 2, null },
                    { 32, 3m, 3m, 1m, 10, false, 1m, 3m, 1, null },
                    { 33, 4m, 4m, 1m, 10, false, 1m, 4m, 2, null },
                    { 34, 1m, 1m, 1m, 13, false, 1m, 1m, 2, null },
                    { 35, 2m, 2m, 1m, 13, false, 1m, 2m, 2, null },
                    { 36, 3m, 3m, 1m, 13, false, 1m, 3m, 1, null },
                    { 37, 4m, 4m, 1m, 13, false, 1m, 4m, 2, null },
                    { 38, 1m, 1m, 1m, 7, false, 1m, 1m, 3, null },
                    { 39, 2m, 2m, 1m, 7, false, 1m, 2m, 3, null },
                    { 40, 3m, 3m, 1m, 7, false, 1m, 3m, 3, null },
                    { 41, 4m, 4m, 1m, 7, false, 1m, 4m, 3, null },
                    { 42, 1m, 1m, 1m, 14, false, 1m, 1m, 3, null },
                    { 43, 2m, 2m, 1m, 14, false, 1m, 2m, 3, null },
                    { 44, 3m, 3m, 1m, 14, false, 1m, 3m, 3, null },
                    { 45, 4m, 4m, 1m, 14, false, 1m, 4m, 3, null },
                    { 46, 1m, 1m, 1m, 8, false, 1m, 1m, 3, null },
                    { 47, 1m, 1m, 1m, 11, false, 1m, 1m, 3, null },
                    { 48, 1m, 1m, 1m, 15, false, 1m, 1m, 3, null }
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
                columns: new[] { "Id", "DeleteDate", "EventId", "FestivalId", "IsDeleted", "IsUpdated", "Price", "TicketTypeId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, 1, null, false, false, 24.99m, 1, null },
                    { 2, null, 2, null, false, false, 34.99m, 2, null },
                    { 3, null, 3, null, false, false, 29.99m, 3, null },
                    { 4, null, 4, null, false, false, 19.99m, 3, null },
                    { 5, null, 1, 1, false, false, 19.99m, 1, null },
                    { 6, null, 5, 1, false, false, 19.99m, 1, null },
                    { 7, null, 2, 2, false, false, 29.99m, 2, null },
                    { 8, null, 6, 2, false, false, 29.99m, 2, null }
                });

            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "Id", "DeleteDate", "EndDate", "EventPassId", "IsDeleted", "IsFestivalReservation", "IsUpdated", "PaymentAmount", "PaymentDate", "PaymentTypeId", "ReservationDate", "ReservationGuid", "StartDate", "TicketId", "TicketPDFId", "TotalAdditionalPaymentAmount", "TotalAddtionalPaymentPercentage", "TotalDiscount", "UpdateDate", "UserId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 12, 26, 1, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, false, 24.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("53538b58-f885-4f4a-b675-a4aa4063ccf3"), new DateTime(2024, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 2.5m, 10m, 0m, null, "4" },
                    { 2, null, new DateTime(2024, 12, 27, 3, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, false, 34.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ed8b9230-223b-4609-8d13-aa6017edad09"), new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 0m, 0m, 0m, null, "2" },
                    { 3, null, new DateTime(2024, 12, 28, 2, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, false, 29.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f9a076c4-3475-4a28-a60c-6e0e3c03731a"), new DateTime(2024, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 7.5m, 25m, 0m, null, "3" },
                    { 4, null, new DateTime(2024, 12, 29, 3, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, false, 19.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("de1d6773-f027-4888-996a-0296e5c52708"), new DateTime(2024, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, 0m, 0m, 0m, null, "3" },
                    { 5, null, new DateTime(2024, 12, 26, 1, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, false, 19.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0b74ec7b-933b-4163-afa5-e0997681dccd"), new DateTime(2024, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5, 2m, 10m, 0m, null, "2" },
                    { 6, null, new DateTime(2025, 1, 26, 1, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, false, 19.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0b74ec7b-933b-4163-afa5-e0997681dccd"), new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 5, 2m, 10m, 0m, null, "2" },
                    { 7, null, new DateTime(2024, 12, 27, 3, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, false, 29.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("806cade1-2685-43dc-8cfc-682fc4229db6"), new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 6, 0m, 0m, 0m, null, "2" },
                    { 8, null, new DateTime(2025, 1, 28, 2, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, false, 29.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("806cade1-2685-43dc-8cfc-682fc4229db6"), new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 6, 0m, 0m, 0m, null, "2" }
                });

            migrationBuilder.InsertData(
                table: "Reservation_Seat",
                columns: new[] { "ReservationId", "SeatId" },
                values: new object[,]
                {
                    { 1, 26 },
                    { 2, 38 },
                    { 3, 14 },
                    { 4, 46 },
                    { 5, 29 },
                    { 6, 33 },
                    { 7, 41 },
                    { 8, 20 }
                });

            migrationBuilder.InsertData(
                table: "Reservation_TicketJPG",
                columns: new[] { "ReservationId", "TicketJPGId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 5, 6 },
                    { 6, 5 },
                    { 6, 6 },
                    { 7, 7 },
                    { 7, 8 },
                    { 8, 7 },
                    { 8, 8 }
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
                name: "IX_Event_CategoryId",
                table: "Event",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_HallId",
                table: "Event",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_EventPass_PassTypeId",
                table: "EventPass",
                column: "PassTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EventPass_PaymentTypeId",
                table: "EventPass",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EventPass_UserId",
                table: "EventPass",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Festival_Event_EventId",
                table: "Festival_Event",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Festival_MediaPatron_MediaPatronId",
                table: "Festival_MediaPatron",
                column: "MediaPatronId");

            migrationBuilder.CreateIndex(
                name: "IX_Festival_Organizer_OrganizerId",
                table: "Festival_Organizer",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Festival_Sponsor_SponsorId",
                table: "Festival_Sponsor",
                column: "SponsorId");

            migrationBuilder.CreateIndex(
                name: "IX_Hall_HallTypeId",
                table: "Hall",
                column: "HallTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HallRent_HallId",
                table: "HallRent",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_HallRent_PaymentTypeId",
                table: "HallRent",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HallRent_UserId",
                table: "HallRent",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HallRent_AdditionalServices_AdditionalServicesId",
                table: "HallRent_AdditionalServices",
                column: "AdditionalServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_HallType_Equipment_HallTypeId",
                table: "HallType_Equipment",
                column: "HallTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_EventPassId",
                table: "Reservation",
                column: "EventPassId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_PaymentTypeId",
                table: "Reservation",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_TicketId",
                table: "Reservation",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_TicketPDFId",
                table: "Reservation",
                column: "TicketPDFId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_UserId",
                table: "Reservation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_Seat_SeatId",
                table: "Reservation_Seat",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_TicketJPG_TicketJPGId",
                table: "Reservation_TicketJPG",
                column: "TicketJPGId");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_HallId",
                table: "Seat",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_SeatTypeId",
                table: "Seat",
                column: "SeatTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_EventId",
                table: "Ticket",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_FestivalId",
                table: "Ticket",
                column: "FestivalId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketTypeId",
                table: "Ticket",
                column: "TicketTypeId");
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
                name: "FAQ");

            migrationBuilder.DropTable(
                name: "Festival_Event");

            migrationBuilder.DropTable(
                name: "Festival_MediaPatron");

            migrationBuilder.DropTable(
                name: "Festival_Organizer");

            migrationBuilder.DropTable(
                name: "Festival_Sponsor");

            migrationBuilder.DropTable(
                name: "HallDetails");

            migrationBuilder.DropTable(
                name: "HallRent_AdditionalServices");

            migrationBuilder.DropTable(
                name: "HallType_Equipment");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Partner");

            migrationBuilder.DropTable(
                name: "Reservation_Seat");

            migrationBuilder.DropTable(
                name: "Reservation_TicketJPG");

            migrationBuilder.DropTable(
                name: "UserData");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "MediaPatron");

            migrationBuilder.DropTable(
                name: "Organizer");

            migrationBuilder.DropTable(
                name: "Sponsor");

            migrationBuilder.DropTable(
                name: "AdditionalServices");

            migrationBuilder.DropTable(
                name: "HallRent");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "TicketJPG");

            migrationBuilder.DropTable(
                name: "SeatType");

            migrationBuilder.DropTable(
                name: "EventPass");

            migrationBuilder.DropTable(
                name: "TicketPDF");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EventPassType");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Festival");

            migrationBuilder.DropTable(
                name: "TicketType");

            migrationBuilder.DropTable(
                name: "EventCategory");

            migrationBuilder.DropTable(
                name: "EventDetails");

            migrationBuilder.DropTable(
                name: "Hall");

            migrationBuilder.DropTable(
                name: "FestivalDetails");

            migrationBuilder.DropTable(
                name: "HallType");
        }
    }
}

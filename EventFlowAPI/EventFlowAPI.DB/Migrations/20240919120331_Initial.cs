using System;
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
                    Price = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false)
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
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
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
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
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
                    Price = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPassType", x => x.Id);
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
                    Description = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true)
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
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaPatron", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
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
                    AddtionalPaymentPercentage = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
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
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HouseNumber = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: false),
                    FlatNumber = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
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
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false)
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
                    HallNr = table.Column<int>(type: "int", nullable: false),
                    RentalPricePerHour = table.Column<decimal>(type: "NUMERIC(5,2)", nullable: false),
                    IsCopy = table.Column<bool>(type: "bit", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    Floor = table.Column<decimal>(type: "NUMERIC(1,0)", nullable: false),
                    TotalLength = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    TotalWidth = table.Column<decimal>(type: "NUMERIC(4,2)", nullable: false),
                    TotalArea = table.Column<decimal>(type: "NUMERIC(5,2)", nullable: false),
                    StageArea = table.Column<decimal>(type: "NUMERIC(5,2)", nullable: true),
                    NumberOfSeatsRows = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    MaxNumberOfSeatsRows = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    NumberOfSeatsColumns = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    MaxNumberOfSeatsColumns = table.Column<decimal>(type: "NUMERIC(2,0)", nullable: false),
                    NumberOfSeats = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: false),
                    MaxNumberOfSeats = table.Column<decimal>(type: "NUMERIC(3,0)", nullable: false),
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
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RenewalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false),
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
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    HallId = table.Column<int>(type: "int", nullable: false),
                    DefaultHallId = table.Column<int>(type: "int", nullable: false)
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
                name: "HallRent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "NUMERIC(7,2)", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    HallId = table.Column<int>(type: "int", nullable: false),
                    DefaultHallId = table.Column<int>(type: "int", nullable: false),
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
                    HallId = table.Column<int>(type: "int", nullable: false)
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
                    StartOfReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndOfReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAddtionalPaymentPercentage = table.Column<decimal>(type: "NUMERIC(5,2)", nullable: false),
                    TotalAdditionalPaymentAmount = table.Column<decimal>(type: "NUMERIC(6,2)", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "NUMERIC(7,2)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_Reservation_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    { "1", 0, "9da1421f-5680-4d68-8d2d-9aa4b39fb7a5", new DateTime(2000, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", true, false, null, "Admin", "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEOyk1tRnFAlWqVwDl/u+WwVDli9dUQ5c7pByuVRcas3bAOVbxeuH0wkIQvrYv2DkKA==", null, false, "994d8399-ffc3-4fdf-9acf-c7561c72775e", "Admin", false, "admin@gmail.com" },
                    { "2", 0, "8514a7ff-b108-4e2a-881d-c2e8b25a2d4d", new DateTime(1985, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "j.kowalski@gmail.com", true, false, null, "Jan", "J.KOWALSKI@GMAIL.COM", "J.KOWALSKI@GMAIL.COM", "AQAAAAIAAYagAAAAEEynKiGeW3PEVCxPWVDJvc5PPPOn3cKkJimIUE28IGikduq2lR00EUAIq5gVBZvkXw==", null, false, "2f1a8da2-f8f9-42be-843e-0248e193d741", "Kowalski", false, "j.kowalski@gmail.com" },
                    { "3", 0, "a77e9294-4d85-4464-b32f-7e0247b300cf", new DateTime(1979, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "a.kowalska@gmail.com", true, false, null, "Anna", "A.KOWALSKA@GMAIL.COM", "A.KOWALSKA@GMAIL.COM", "AQAAAAIAAYagAAAAEBXygKUbUXbF29K3Q8tvwiXykpLe9PTxLw/ddLn4BYP026h/i6hPvGySMMvW7eZjVg==", null, false, "a5064724-920e-481b-8c59-08862c5a8536", "Kowalska", false, "a.kowalska@gmail.com" },
                    { "4", 0, "3e73df7d-fbd9-458f-ac1e-8fc7c89562c2", new DateTime(1979, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "mateusz.strapczuk1@gmail.com", true, false, null, "Mateusz", "MATEUSZ.STRAPCZUK1@GMAIL.COM", "MATEUSZ.STRAPCZUK1@GMAIL.COM", "AQAAAAIAAYagAAAAEIOUTYWj0dk3B+YCiQqyN6WEiM9mserOaCma7btDrQfZn2je7Mr/fd/aGJjejgRFDA==", null, false, "9118a688-1a42-4421-9def-f19ae0f530ef", "Strapczuk", false, "mateusz.strapczuk1@gmail.com" }
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
                    { "2", "3" },
                    { "2", "4" }
                });

            migrationBuilder.InsertData(
                table: "EventPass",
                columns: new[] { "Id", "EndDate", "PassTypeId", "PaymentAmount", "PaymentDate", "PaymentTypeId", "RenewalDate", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 499.99m, new DateTime(2024, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, new DateTime(2024, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "1" },
                    { 2, new DateTime(2025, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 999.99m, new DateTime(2024, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, new DateTime(2024, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "2" },
                    { 3, new DateTime(2024, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 235.99m, new DateTime(2024, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, new DateTime(2024, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "3" }
                });

            migrationBuilder.InsertData(
                table: "Festival",
                columns: new[] { "Id", "Duration", "EndDate", "Name", "ShortDescription", "StartDate" },
                values: new object[,]
                {
                    { 1, new TimeSpan(-32, -1, 0, 0, 0), new DateTime(2024, 11, 20, 1, 0, 0, 0, DateTimeKind.Unspecified), "Festiwal muzyki współczesnej", "Festiwal muzyki współczesnej to nowy festiwal organizowany przez XYZ.", new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new TimeSpan(-32, -2, 0, 0, 0), new DateTime(2024, 11, 22, 2, 0, 0, 0, DateTimeKind.Unspecified), "Festiwal filmowy", "Festiwal filmowy to festiwal na którym można obejrzeć filmy.", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new TimeSpan(-31, -3, 0, 0, 0), new DateTime(2024, 11, 23, 3, 0, 0, 0, DateTimeKind.Unspecified), "Festiwal sztuki abstrakcyjnej", "Festiwal sztuki abstrakcyjnej to festiwal na którym można zobaczyć sztukę.", new DateTime(2024, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
                    { "3", "Białystok", 21m, 7m, "147852369", "Pogodna", "14-453" },
                    { "4", "Warszawa", 42m, 21m, "147852369", "Słoneczna", "14-453" }
                });

            migrationBuilder.InsertData(
                table: "Event",
                columns: new[] { "Id", "CategoryId", "DefaultHallId", "Duration", "EndDate", "HallId", "Name", "ShortDescription", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, 2, new TimeSpan(0, -1, 0, 0, 0), new DateTime(2024, 10, 20, 1, 0, 0, 0, DateTimeKind.Unspecified), 2, "Koncert: Mystic Waves", "Jedyna taka okazja na usłyszenie Mystic Waves na żywo.", new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 3, 3, new TimeSpan(0, -3, 0, 0, 0), new DateTime(2024, 10, 21, 3, 0, 0, 0, DateTimeKind.Unspecified), 3, "Cień Przeszłośći", "Cień Przeszłości to jedyny taki spektakl.", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, 1, new TimeSpan(0, -2, 0, 0, 0), new DateTime(2024, 10, 22, 2, 0, 0, 0, DateTimeKind.Unspecified), 1, "Królestwo planety małp", "Nowy film Królestwo planety małp już w kinach!.", new DateTime(2024, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, 4, new TimeSpan(0, -3, 0, 0, 0), new DateTime(2024, 10, 23, 3, 0, 0, 0, DateTimeKind.Unspecified), 4, "Nowe inspiracje", "Nowe inspiracje to nowoczesna wystawa sztuki.", new DateTime(2024, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, 2, new TimeSpan(0, -1, 0, 0, 0), new DateTime(2024, 11, 20, 1, 0, 0, 0, DateTimeKind.Unspecified), 2, "Koncert: New Era", "Jedyna taka okazja na usłyszenie New Era na żywo.", new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 2, 1, new TimeSpan(0, -2, 0, 0, 0), new DateTime(2024, 11, 22, 2, 0, 0, 0, DateTimeKind.Unspecified), 1, "Gladiator", "Nowy film Gladiator już w kinach!.", new DateTime(2024, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 4, 4, new TimeSpan(0, -3, 0, 0, 0), new DateTime(2024, 11, 23, 3, 0, 0, 0, DateTimeKind.Unspecified), 4, "Nowa sztuka", "Nowe sztuka to nowoczesna wystawa sztuki.", new DateTime(2024, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
                    { 1, 1, new DateTime(2024, 10, 20, 8, 0, 0, 0, DateTimeKind.Unspecified), 1, 899.99m, new DateTime(2024, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "1" },
                    { 2, 3, new DateTime(2024, 10, 21, 4, 0, 0, 0, DateTimeKind.Unspecified), 3, 699.99m, new DateTime(2024, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "3" },
                    { 3, 3, new DateTime(2024, 10, 22, 2, 0, 0, 0, DateTimeKind.Unspecified), 3, 399.99m, new DateTime(2024, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "3" },
                    { 4, 4, new DateTime(2024, 10, 23, 1, 0, 0, 0, DateTimeKind.Unspecified), 4, 150.99m, new DateTime(2024, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "2" }
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
                columns: new[] { "Id", "EndOfReservationDate", "PaymentAmount", "PaymentDate", "PaymentTypeId", "ReservationDate", "ReservationGuid", "StartOfReservationDate", "TicketId", "TotalAdditionalPaymentAmount", "TotalAddtionalPaymentPercentage", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 20, 1, 0, 0, 0, DateTimeKind.Unspecified), 24.99m, new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("47ca2cf2-8546-4175-a706-bcd143e07788"), new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2.5m, 10m, "1" },
                    { 2, new DateTime(2024, 10, 21, 3, 0, 0, 0, DateTimeKind.Unspecified), 34.99m, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c49fa91a-602d-4ebb-9a2e-d7628211ef5c"), new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 0m, 0m, "2" },
                    { 3, new DateTime(2024, 10, 22, 2, 0, 0, 0, DateTimeKind.Unspecified), 29.99m, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e20f5c1b-0779-42ea-a841-c1d45f6361ea"), new DateTime(2024, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 7.5m, 25m, "3" },
                    { 4, new DateTime(2024, 10, 23, 3, 0, 0, 0, DateTimeKind.Unspecified), 19.99m, new DateTime(2024, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ef37f4ed-33a7-438e-88f0-7603f9406e1a"), new DateTime(2024, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 0m, 0m, "3" },
                    { 5, new DateTime(2024, 10, 20, 1, 0, 0, 0, DateTimeKind.Unspecified), 19.99m, new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1ab54166-cdbc-449e-b426-bc2d23057665"), new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 2m, 10m, "2" },
                    { 6, new DateTime(2024, 11, 20, 1, 0, 0, 0, DateTimeKind.Unspecified), 19.99m, new DateTime(2024, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1ab54166-cdbc-449e-b426-bc2d23057665"), new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 2m, 10m, "2" },
                    { 7, new DateTime(2024, 10, 21, 3, 0, 0, 0, DateTimeKind.Unspecified), 29.99m, new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9ee52f89-2f0a-46c7-bfda-313a948ccc7b"), new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 0m, 0m, "2" },
                    { 8, new DateTime(2024, 11, 22, 2, 0, 0, 0, DateTimeKind.Unspecified), 29.99m, new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9ee52f89-2f0a-46c7-bfda-313a948ccc7b"), new DateTime(2024, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 7.5m, 25m, "2" }
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
                name: "IX_Reservation_PaymentTypeId",
                table: "Reservation",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_TicketId",
                table: "Reservation",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_UserId",
                table: "Reservation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_Seat_SeatId",
                table: "Reservation_Seat",
                column: "SeatId");

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
                name: "EventPass");

            migrationBuilder.DropTable(
                name: "Festival_Event");

            migrationBuilder.DropTable(
                name: "Festival_MediaPatron");

            migrationBuilder.DropTable(
                name: "Festival_Organizer");

            migrationBuilder.DropTable(
                name: "Festival_Sponsor");

            migrationBuilder.DropTable(
                name: "HallRent_AdditionalServices");

            migrationBuilder.DropTable(
                name: "HallType_Equipment");

            migrationBuilder.DropTable(
                name: "Reservation_Seat");

            migrationBuilder.DropTable(
                name: "UserData");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EventPassType");

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
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "SeatType");

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

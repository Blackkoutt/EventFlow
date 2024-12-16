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
                    { "1", 0, "7481e8e1-b73b-451a-abdd-d2eeec8e2340", new DateTime(2000, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", true, true, false, null, "Admin", "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEHRlMPNZC0Urpxc8CCQBlX3QZ3IeSa8xwlT0gMaUVZtAQInwUDm8970jsRKnrsEJ2A==", null, false, "admin.jpg", "APP", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "c9e16abd-3f79-44ce-b687-797799f85f40", "Admin", false, "admin@gmail.com" },
                    { "2", 0, "5bc81c26-1e30-49c0-b8b7-f8a6f3d399b0", new DateTime(1985, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mateusz.strapczuk2@gmail.com", true, true, false, null, "Mateusz2", "MATEUSZ.STRAPCZUK2@GMAIL.COM", "MATEUSZ.STRAPCZUK2@GMAIL.COM", "AQAAAAIAAYagAAAAEBnbLHFsfu1dtAYSMB58XL1jYXFbaG27UCsMhaHoVGXy9rZ9BYcFhTPqcIGtlqyd6w==", null, false, "user2.jpg", "APP", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "0a910b57-ea11-4b3b-8b5d-d6d3d73b3cbb", "Strapczuk2", false, "mateusz.strapczuk2@gmail.com" },
                    { "3", 0, "1daddf3b-3e2a-4ed9-abb1-a268c5605818", new DateTime(1979, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "mateusz.strapczuk3@gmail.com", true, true, false, null, "Mateusz3", "MATEUSZ.STRAPCZUK3@GMAIL.COM", "MATEUSZ.STRAPCZUK3@GMAIL.COM", "AQAAAAIAAYagAAAAEDiJeiCY0Z1WDUxTCe7vekGCpDh5D8hAR/Su22Ya6u3CFdgzPL1JDUo5qQoP628fQw==", null, false, "user3.jpg", "APP", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "2622e586-7df9-4e73-8523-a349476defee", "Strapczuk3", false, "mateusz.strapczuk3@gmail.com" },
                    { "4", 0, "95f15038-5e38-4b90-9b36-d07e759abcc9", new DateTime(1979, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "mateusz.strapczuk4@gmail.com", true, true, false, null, "Mateusz4", "MATEUSZ.STRAPCZUK4@GMAIL.COM", "MATEUSZ.STRAPCZUK4@GMAIL.COM", "AQAAAAIAAYagAAAAEObq7draO/OkBTY+xhxZHZ9MAr9GcnIh8MiG30OJWgOuLsekwR2S9Sj7p0UIng+K6Q==", null, false, "user4.jpg", "APP", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "ad19cc68-d111-4c35-864e-555f206426c5", "Strapczuk4", false, "mateusz.strapczuk4@gmail.com" },
                    { "5", 0, "0a2c5edc-8cfb-4e39-8cad-d4b381b51d27", new DateTime(1979, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "mateusz.strapczuk1@gmail.com", true, true, false, null, "Mateusz", "MATEUSZ.STRAPCZUK1@GMAIL.COM", "MATEUSZ.STRAPCZUK1@GMAIL.COM", "AQAAAAIAAYagAAAAEN4fqMsf5CcTM5WYX/mSXTQqkEKVBdqFfHZ3KozUwJxDylqK0mwDGbvkoJuiNYtcWQ==", null, false, "user5.jpg", "APP", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "6aeb3af6-de82-4209-938c-8625a29dc6aa", "Strapczuk", false, "mateusz.strapczuk1@gmail.com" }
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
                    { 1, null, false, false, new Guid("0577dcc1-ea63-4d01-8602-a41fb623d99f"), "Gazeta Nowoczesna", "", null },
                    { 2, null, false, false, new Guid("486553fb-34f3-46b8-b042-b880509c9e2f"), "Nowy świat TV", "", null },
                    { 3, null, false, false, new Guid("fb5050c3-590d-41ee-90dd-d5eeda7f620b"), "Tygodnik Nowiny", "", null }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "LongDescription", "NewsGuid", "PhotoName", "PublicationDate", "ShortDescription", "Title" },
                values: new object[,]
                {
                    { 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus velit ac, feugiat turpis. Vestibulum fermentum placerat mi nec scelerisque. Ut id nibh ornare, luctus velit ac, feugiat turpis. Vestibulum fermentum. Vestibulum fermentum placerat mi nec. Ut id nibh ornare, luctus velit ac, feugiat turpis.Vestibulum fermentum. Vestibulum fermentum.", new Guid("5b16b5a2-094d-49fb-b4cb-96805522a4e8"), "konkurs_artystyczny.png", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus velit ac, feugiat turpis. Vestibulum fermentum placerat mi nec scelerisque. Ut id nibh ornare, luctus velit ac, feugiat turpis. Vestibulum fermentum. Vestibulum fermentum placerat mi nec. Ut id nibh ornare, luctus velit ac, feugiat turpis.Vestibulum fermentum. Vestibulum fermentum.", "Finał konkursu artystycznego" },
                    { 2, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", new Guid("faa360c6-b730-4990-ba65-3ff232340011"), "koncert_lunar_vibes.png", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", "Relacja z koncertu zespołu Lunar Vibes" },
                    { 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", new Guid("daef5bcb-3681-4539-a8de-f79929116ea7"), "modernizacja sali.png", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", "Modernizacja sali koncertowej" },
                    { 4, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", new Guid("29910764-8476-4840-8f1b-15f1a7e8fdcb"), "znizka.png", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", "Zniżka 20% na zakup karnetów" },
                    { 5, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", new Guid("91b08804-baff-4334-bb08-c89d811ffcaf"), "noc_filmowa.png", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", "Noc Filmowa z Klasykami Kina" },
                    { 6, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", new Guid("ef0d6702-b20f-4bfe-9c48-17930ae8a90f"), "wernisaz.png", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut id nibh ornare, luctus...", "Wernisaż: Nowe inspiracje" }
                });

            migrationBuilder.InsertData(
                table: "Organizer",
                columns: new[] { "Id", "DeleteDate", "IsDeleted", "IsUpdated", "Name", "OrganizerGuid", "PhotoName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, false, false, "EventFlow", new Guid("618d3517-e270-418a-a47f-de32f49e7070"), "", null },
                    { 2, null, false, false, "Snowflake", new Guid("d888fdb4-16ad-4910-b687-fc607a94d839"), "", null },
                    { 3, null, false, false, "Aura", new Guid("f5554240-67d9-46f5-9118-d456d32ec737"), "", null }
                });

            migrationBuilder.InsertData(
                table: "Partner",
                columns: new[] { "Id", "Name", "PartnerGuid", "PhotoName" },
                values: new object[,]
                {
                    { 1, "Basel", new Guid("27ceceac-1524-4fb4-8bb9-7c3121ea0b5c"), "basel.png" },
                    { 2, "Aura", new Guid("27511245-f9ae-4866-bde1-bfb0bf87e343"), "aura.png" },
                    { 3, "Vision", new Guid("900cf3cc-0642-4caa-aa37-fc74effc0974"), "vision.png" },
                    { 4, "Snowflake", new Guid("70356bf0-be95-46ab-abfc-20591b0a6a89"), "snowflake.png" },
                    { 5, "Waveless", new Guid("0b66f190-c5d3-41e8-8900-d72c403be7c3"), "waveless.png" }
                });

            migrationBuilder.InsertData(
                table: "PaymentType",
                columns: new[] { "Id", "DeleteDate", "IsDeleted", "IsUpdated", "Name", "PaymentTypeGuid", "PhotoName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, false, false, "Karta kredytowa", new Guid("d053456b-c4bb-4304-a20e-4ed34d17bebb"), "", null },
                    { 2, null, false, false, "Przelew", new Guid("d2fa1570-6c00-4ab6-824d-a4ea2e1452bf"), "", null },
                    { 3, null, false, false, "BLIK", new Guid("73ebbc46-7488-42c5-9b14-0f5e5e8c458f"), "", null },
                    { 4, null, false, false, "Zapłać później", new Guid("f3147d0e-5d50-4b14-b0ab-2ddba2fe1404"), "", null },
                    { 5, null, false, false, "Karnet", new Guid("be1f5ac1-7cb2-462b-83c1-9ad7eb78243e"), "", null }
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
                    { 1, null, false, false, "Basel", "", new Guid("76df7724-8575-41b0-8dd5-1fe059e40293"), null },
                    { 2, null, false, false, "Vision", "", new Guid("bcc8ca2b-aa1a-4f50-86f7-ec444ad5b8f5"), null },
                    { 3, null, false, false, "Waveless", "", new Guid("d1c2a423-8034-41d3-a289-d7fea168dae1"), null }
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
                    { 2, null, new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("766245b4-8c08-49dd-9480-2606aaa590be"), "eventflow_karnet_766245b4-8c08-49dd-9480-2606aaa590be.jpg", "eventflow_karnet_766245b4-8c08-49dd-9480-2606aaa590be.pdf", false, false, 4, 999.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, null, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 0m, null, "3" },
                    { 3, null, new DateTime(2025, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33610a0d-a1b7-4700-bffe-9e334b977e6a"), "eventflow_karnet_33610a0d-a1b7-4700-bffe-9e334b977e6a.jpg", "eventflow_karnet_33610a0d-a1b7-4700-bffe-9e334b977e6a.pdf", false, false, 2, 235.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, null, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 0m, null, "4" }
                });

            migrationBuilder.InsertData(
                table: "Festival",
                columns: new[] { "Id", "AddDate", "DeleteDate", "Duration", "EndDate", "FestivalGuid", "IsDeleted", "IsUpdated", "Name", "PhotoName", "ShortDescription", "StartDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, -2768400L, new DateTime(2025, 1, 26, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e5cdabb0-90a2-4b63-9564-e50993bc09ff"), false, false, "Festiwal muzyki hip-hop", "festival.png", "Festiwal muzyki hip-hop to nowy festiwal organizowany przez XYZ.", new DateTime(2024, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, -2772000L, new DateTime(2025, 1, 28, 2, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5e6df8ae-8175-4dd8-adbd-0bc2ae75c51c"), false, false, "Festiwal filmowy", "", "Festiwal filmowy to festiwal na którym można obejrzeć filmy.", new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, -2689200L, new DateTime(2025, 1, 29, 3, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11ff0760-5ca2-4ea0-90c6-b8c78286bd45"), false, false, "Festiwal sztuki abstrakcyjnej", "", "Festiwal sztuki abstrakcyjnej to festiwal na którym można zobaczyć sztukę.", new DateTime(2024, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Hall",
                columns: new[] { "Id", "DefaultId", "Floor", "HallNr", "HallTypeId", "HallViewFileName", "IsCopy", "IsUpdated", "IsVisible", "RentalPricePerHour", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, 2m, 1, 1, null, false, false, true, 120.99m, null },
                    { 2, 2, 1m, 2, 2, null, false, false, true, 89.99m, null },
                    { 3, 3, 2m, 3, 3, null, false, false, true, 179.99m, null },
                    { 4, 4, 1m, 4, 4, null, false, false, true, 199.99m, null },
                    { 5, 1, 2m, 1, 1, null, true, false, false, 120.99m, null },
                    { 6, 2, 1m, 2, 2, null, true, false, false, 89.99m, null },
                    { 7, 3, 2m, 3, 3, null, true, false, false, 179.99m, null },
                    { 8, 4, 1m, 4, 4, null, true, false, false, 199.99m, null },
                    { 9, 1, 2m, 1, 1, null, true, false, false, 120.99m, null },
                    { 10, 2, 1m, 2, 2, null, true, false, false, 89.99m, null },
                    { 11, 4, 1m, 4, 4, null, true, false, false, 199.99m, null },
                    { 12, 1, 2m, 1, 1, null, true, false, false, 120.99m, null },
                    { 13, 2, 1m, 2, 2, null, true, false, false, 89.99m, null },
                    { 14, 3, 2m, 3, 3, null, true, false, false, 179.99m, null },
                    { 15, 4, 1m, 4, 4, null, true, false, false, 199.99m, null }
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
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, -3600L, new DateTime(2024, 12, 26, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7faad234-b17a-45fb-a367-21c4522f09d4"), 6, false, false, "Koncert: Mystic Waves", "koncert_mystic_waves.png", "Któtki opis koncertu Mystic Waves.", new DateTime(2024, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, -10800L, new DateTime(2024, 12, 27, 3, 0, 0, 0, DateTimeKind.Unspecified), new Guid("18d46db4-c2c1-4b9d-b421-e81541aea561"), 7, false, false, "Cień Przeszłości", "cien_przeszlosci.png", "Krótki opis spektaklu pt. Cień Przeszłości.", new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, -7200L, new DateTime(2024, 12, 28, 2, 0, 0, 0, DateTimeKind.Unspecified), new Guid("eca284ef-bad8-4922-9b3f-3ddf428916bb"), 5, false, false, "Królestwo planety małp", "krolestwo_planety_malp.png", "Nowy film Królestwo planety małp już w kinach!.", new DateTime(2024, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, -10800L, new DateTime(2024, 12, 29, 3, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7f923e51-ef8b-4aeb-b511-005c8ed3541d"), 8, false, false, "Nowe inspiracje", "nowe_inspiracje.png", "Nowe inspiracje to nowoczesna wystawa sztuki.", new DateTime(2024, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, -3600L, new DateTime(2025, 1, 26, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1ed1520a-c309-42d8-9462-0be74291a1dc"), 10, false, false, "Koncert: New Era", "", "Jedyna taka okazja na usłyszenie New Era na żywo.", new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, -7200L, new DateTime(2025, 1, 28, 2, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4710847f-32db-4f0a-a702-11914c2c24f1"), 9, false, false, "Gladiator", "", "Nowy film Gladiator już w kinach!.", new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, -10800L, new DateTime(2025, 1, 29, 3, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e38ecec3-6411-4acb-a522-3b0ba63b1564"), 11, false, false, "Nowa sztuka", "", "Nowe sztuka to nowoczesna wystawa sztuki.", new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
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
                    { 1, null, 28800L, new DateTime(2024, 12, 26, 8, 0, 0, 0, DateTimeKind.Unspecified), 12, new Guid("0d7654ec-c1ea-4841-b4d8-9b3b934bbac0"), null, false, false, 899.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "4" },
                    { 2, null, 14400L, new DateTime(2024, 12, 27, 4, 0, 0, 0, DateTimeKind.Unspecified), 13, new Guid("7a3801cf-4009-4db3-b4ed-64428f336072"), null, false, false, 699.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "3" },
                    { 3, null, 7200L, new DateTime(2024, 12, 28, 2, 0, 0, 0, DateTimeKind.Unspecified), 14, new Guid("e2bd56f4-e7a0-42b4-819c-0ed15c6deaad"), null, false, false, 399.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "3" },
                    { 4, null, 3600L, new DateTime(2024, 12, 29, 1, 0, 0, 0, DateTimeKind.Unspecified), 15, new Guid("62f38bed-d95c-4512-9619-4cd67f52e786"), null, false, false, 150.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "2" }
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
                    { 2, null, new DateTime(2024, 12, 27, 3, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, false, 34.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ed8b9230-223b-4609-8d13-aa6017edad09"), new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 0m, 0m, 0m, null, "2" },
                    { 3, null, new DateTime(2024, 12, 28, 2, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, false, 29.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f9a076c4-3475-4a28-a60c-6e0e3c03731a"), new DateTime(2024, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 7.5m, 25m, 0m, null, "3" },
                    { 4, null, new DateTime(2024, 12, 29, 3, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, false, 19.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("de1d6773-f027-4888-996a-0296e5c52708"), new DateTime(2024, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, 0m, 0m, 0m, null, "3" },
                    { 5, null, new DateTime(2024, 12, 26, 1, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, false, 19.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0b74ec7b-933b-4163-afa5-e0997681dccd"), new DateTime(2024, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5, 2m, 10m, 0m, null, "2" },
                    { 6, null, new DateTime(2025, 1, 26, 1, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, false, 19.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0b74ec7b-933b-4163-afa5-e0997681dccd"), new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 5, 2m, 10m, 0m, null, "2" },
                    { 7, null, new DateTime(2024, 12, 27, 3, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, false, 29.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("806cade1-2685-43dc-8cfc-682fc4229db6"), new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 6, 0m, 0m, 0m, null, "2" },
                    { 8, null, new DateTime(2025, 1, 28, 2, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, false, 29.99m, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("806cade1-2685-43dc-8cfc-682fc4229db6"), new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 6, 0m, 0m, 0m, null, "2" }
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

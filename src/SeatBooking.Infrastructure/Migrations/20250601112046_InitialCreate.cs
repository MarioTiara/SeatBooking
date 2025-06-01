using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SeatBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aircraft",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aircraft", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Airport",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airport", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "FlightInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightNumber = table.Column<int>(type: "int", nullable: false),
                    OperatingFlightNumber = table.Column<int>(type: "int", nullable: false),
                    AirlineCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    OperatingAirlineCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DepartureTerminal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ArrivalTerminal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passenger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassengerIndex = table.Column<int>(type: "int", nullable: false),
                    PassengerNameNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MealPreference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SeatPreference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passenger", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cabin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deck = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AircraftCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cabin_Aircraft_AircraftCode",
                        column: x => x.AircraftCode,
                        principalTable: "Aircraft",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightInfoStopAirport",
                columns: table => new
                {
                    FlightInfoId = table.Column<int>(type: "int", nullable: false),
                    AirportCode = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    StopOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightInfoStopAirport", x => new { x.FlightInfoId, x.AirportCode });
                    table.ForeignKey(
                        name: "FK_FlightInfoStopAirport_Airport_AirportCode",
                        column: x => x.AirportCode,
                        principalTable: "Airport",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightInfoStopAirport_FlightInfo_FlightInfoId",
                        column: x => x.FlightInfoId,
                        principalTable: "FlightInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Segment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightsMiles = table.Column<int>(type: "int", nullable: false),
                    AwardFare = table.Column<bool>(type: "bit", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    CabinClass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Equipment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    OriginAirportCode = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    DestinationAirportCode = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Departure = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Arrival = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingClass = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LayoverDuration = table.Column<int>(type: "int", nullable: false),
                    FareBasis = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectToGovernmentApproval = table.Column<bool>(type: "bit", nullable: false),
                    SegmentRef = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FlightInfoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Segment_Aircraft_Equipment",
                        column: x => x.Equipment,
                        principalTable: "Aircraft",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Segment_Airport_DestinationAirportCode",
                        column: x => x.DestinationAirportCode,
                        principalTable: "Airport",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Segment_Airport_OriginAirportCode",
                        column: x => x.OriginAirportCode,
                        principalTable: "Airport",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_Segment_FlightInfo_FlightId",
                        column: x => x.FlightId,
                        principalTable: "FlightInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Segment_FlightInfo_FlightInfoId",
                        column: x => x.FlightInfoId,
                        principalTable: "FlightInfo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Street2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AddressType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Passenger_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passenger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssuingCountry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryOfBirth = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentInfo_Passenger_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passenger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Email_Passenger_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passenger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FrequentFlyer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Airline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TierNumber = table.Column<int>(type: "int", nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrequentFlyer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FrequentFlyer_Passenger_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passenger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phone_Passenger_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passenger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialRequest_Passenger_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passenger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialServiceRequestRemark",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialServiceRequestRemark", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialServiceRequestRemark_Passenger_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passenger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatColumn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CabinId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatColumn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatColumn_Cabin_CabinId",
                        column: x => x.CabinId,
                        principalTable: "Cabin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SeatRow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatCodes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowNumber = table.Column<int>(type: "int", nullable: false),
                    CabinId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatRow_Cabin_CabinId",
                        column: x => x.CabinId,
                        principalTable: "Cabin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatSlot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StorefrontSlotCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Entitled = table.Column<bool>(type: "bit", nullable: false),
                    FreeOfCharge = table.Column<bool>(type: "bit", nullable: false),
                    FeeWaived = table.Column<bool>(type: "bit", nullable: false),
                    OriginallySelected = table.Column<bool>(type: "bit", nullable: false),
                    EntitledRuleId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FeeWaivedRuleId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RefundIndicator = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SeatRowId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatSlot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatSlot_SeatRow_SeatRowId",
                        column: x => x.SeatRowId,
                        principalTable: "SeatRow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PassengerSeatSelection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassengerId = table.Column<int>(type: "int", nullable: false),
                    SeatSlotId = table.Column<int>(type: "int", nullable: false),
                    SeatSlotId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerSeatSelection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassengerSeatSelection_Passenger_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passenger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassengerSeatSelection_SeatSlot_SeatSlotId",
                        column: x => x.SeatSlotId,
                        principalTable: "SeatSlot",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PassengerSeatSelection_SeatSlot_SeatSlotId1",
                        column: x => x.SeatSlotId1,
                        principalTable: "SeatSlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatCharacteristic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsRaw = table.Column<bool>(type: "bit", nullable: false),
                    IsSlot = table.Column<bool>(type: "bit", nullable: false),
                    SeatSlotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatCharacteristic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatCharacteristic_SeatSlot_SeatSlotId",
                        column: x => x.SeatSlotId,
                        principalTable: "SeatSlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatPriceAlternative",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatSlotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatPriceAlternative", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatPriceAlternative_SeatSlot_SeatSlotId",
                        column: x => x.SeatSlotId,
                        principalTable: "SeatSlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatTaxAlternative",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatSlotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatTaxAlternative", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatTaxAlternative_SeatSlot_SeatSlotId",
                        column: x => x.SeatSlotId,
                        principalTable: "SeatSlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SlotDesignation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SeatSlotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlotDesignation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlotDesignation_SeatSlot_SeatSlotId",
                        column: x => x.SeatSlotId,
                        principalTable: "SeatSlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SlotLimitation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SeatSlotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlotLimitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlotLimitation_SeatSlot_SeatSlotId",
                        column: x => x.SeatSlotId,
                        principalTable: "SeatSlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatPriceComponent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SeatPriceAlternativeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatPriceComponent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatPriceComponent_SeatPriceAlternative_SeatPriceAlternativeId",
                        column: x => x.SeatPriceAlternativeId,
                        principalTable: "SeatPriceAlternative",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SeatTaxComponent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SeatTaxAlternativeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatTaxComponent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatTaxComponent_SeatTaxAlternative_SeatTaxAlternativeId",
                        column: x => x.SeatTaxAlternativeId,
                        principalTable: "SeatTaxAlternative",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Airport",
                columns: new[] { "Code", "City", "Name" },
                values: new object[,]
                {
                    { "CGK", "Jakarta", "Soekarno-Hatta International" },
                    { "KUL", "Kuala Lumpur", "Kuala Lumpur International" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_PassengerId",
                table: "Address",
                column: "PassengerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cabin_AircraftCode",
                table: "Cabin",
                column: "AircraftCode");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentInfo_PassengerId",
                table: "DocumentInfo",
                column: "PassengerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Email_PassengerId",
                table: "Email",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightInfoStopAirport_AirportCode",
                table: "FlightInfoStopAirport",
                column: "AirportCode");

            migrationBuilder.CreateIndex(
                name: "IX_FrequentFlyer_PassengerId",
                table: "FrequentFlyer",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerSeatSelection_PassengerId",
                table: "PassengerSeatSelection",
                column: "PassengerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PassengerSeatSelection_SeatSlotId",
                table: "PassengerSeatSelection",
                column: "SeatSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerSeatSelection_SeatSlotId1",
                table: "PassengerSeatSelection",
                column: "SeatSlotId1");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_PassengerId",
                table: "Phone",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatCharacteristic_SeatSlotId",
                table: "SeatCharacteristic",
                column: "SeatSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatColumn_CabinId",
                table: "SeatColumn",
                column: "CabinId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatPriceAlternative_SeatSlotId",
                table: "SeatPriceAlternative",
                column: "SeatSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatPriceComponent_SeatPriceAlternativeId",
                table: "SeatPriceComponent",
                column: "SeatPriceAlternativeId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatRow_CabinId",
                table: "SeatRow",
                column: "CabinId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatSlot_SeatRowId",
                table: "SeatSlot",
                column: "SeatRowId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatTaxAlternative_SeatSlotId",
                table: "SeatTaxAlternative",
                column: "SeatSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatTaxComponent_SeatTaxAlternativeId",
                table: "SeatTaxComponent",
                column: "SeatTaxAlternativeId");

            migrationBuilder.CreateIndex(
                name: "IX_Segment_DestinationAirportCode",
                table: "Segment",
                column: "DestinationAirportCode");

            migrationBuilder.CreateIndex(
                name: "IX_Segment_Equipment",
                table: "Segment",
                column: "Equipment");

            migrationBuilder.CreateIndex(
                name: "IX_Segment_FlightId",
                table: "Segment",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Segment_FlightInfoId",
                table: "Segment",
                column: "FlightInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Segment_OriginAirportCode",
                table: "Segment",
                column: "OriginAirportCode");

            migrationBuilder.CreateIndex(
                name: "IX_SlotDesignation_SeatSlotId",
                table: "SlotDesignation",
                column: "SeatSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_SlotLimitation_SeatSlotId",
                table: "SlotLimitation",
                column: "SeatSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialRequest_PassengerId",
                table: "SpecialRequest",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialServiceRequestRemark_PassengerId",
                table: "SpecialServiceRequestRemark",
                column: "PassengerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "DocumentInfo");

            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "FlightInfoStopAirport");

            migrationBuilder.DropTable(
                name: "FrequentFlyer");

            migrationBuilder.DropTable(
                name: "PassengerSeatSelection");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "SeatCharacteristic");

            migrationBuilder.DropTable(
                name: "SeatColumn");

            migrationBuilder.DropTable(
                name: "SeatPriceComponent");

            migrationBuilder.DropTable(
                name: "SeatTaxComponent");

            migrationBuilder.DropTable(
                name: "Segment");

            migrationBuilder.DropTable(
                name: "SlotDesignation");

            migrationBuilder.DropTable(
                name: "SlotLimitation");

            migrationBuilder.DropTable(
                name: "SpecialRequest");

            migrationBuilder.DropTable(
                name: "SpecialServiceRequestRemark");

            migrationBuilder.DropTable(
                name: "SeatPriceAlternative");

            migrationBuilder.DropTable(
                name: "SeatTaxAlternative");

            migrationBuilder.DropTable(
                name: "Airport");

            migrationBuilder.DropTable(
                name: "FlightInfo");

            migrationBuilder.DropTable(
                name: "Passenger");

            migrationBuilder.DropTable(
                name: "SeatSlot");

            migrationBuilder.DropTable(
                name: "SeatRow");

            migrationBuilder.DropTable(
                name: "Cabin");

            migrationBuilder.DropTable(
                name: "Aircraft");
        }
    }
}

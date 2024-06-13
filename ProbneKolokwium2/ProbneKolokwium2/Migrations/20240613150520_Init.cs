using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProbneKolokwium2.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "boatstandard",
                columns: table => new
                {
                    IdBoatStandard = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_boatstandard", x => x.IdBoatStandard);
                });

            migrationBuilder.CreateTable(
                name: "clientcategory",
                columns: table => new
                {
                    IdClientCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiscountPerc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientcategory", x => x.IdClientCategory);
                });

            migrationBuilder.CreateTable(
                name: "boat",
                columns: table => new
                {
                    IdSailboat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdBoatStandard = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_boat", x => x.IdSailboat);
                    table.ForeignKey(
                        name: "FK_boat_boatstandard_IdBoatStandard",
                        column: x => x.IdBoatStandard,
                        principalTable: "boatstandard",
                        principalColumn: "IdBoatStandard",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdClientCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.IdClient);
                    table.ForeignKey(
                        name: "FK_client_clientcategory_IdClientCategory",
                        column: x => x.IdClientCategory,
                        principalTable: "clientcategory",
                        principalColumn: "IdClientCategory",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservation",
                columns: table => new
                {
                    IdReservation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBoatStandard = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    NumOfBoats = table.Column<int>(type: "int", nullable: false),
                    Fullfilled = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CancelReservation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservation", x => x.IdReservation);
                    table.ForeignKey(
                        name: "FK_reservation_boatstandard_IdBoatStandard",
                        column: x => x.IdBoatStandard,
                        principalTable: "boatstandard",
                        principalColumn: "IdBoatStandard",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservation_client_IdClient",
                        column: x => x.IdClient,
                        principalTable: "client",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sailboatreservation",
                columns: table => new
                {
                    IdSailboat = table.Column<int>(type: "int", nullable: false),
                    IdReservation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sailboatreservation", x => new { x.IdSailboat, x.IdReservation });
                    table.ForeignKey(
                        name: "FK_sailboatreservation_boat_IdSailboat",
                        column: x => x.IdSailboat,
                        principalTable: "boat",
                        principalColumn: "IdSailboat");
                    table.ForeignKey(
                        name: "FK_sailboatreservation_reservation_IdReservation",
                        column: x => x.IdReservation,
                        principalTable: "reservation",
                        principalColumn: "IdReservation");
                });

            migrationBuilder.InsertData(
                table: "boatstandard",
                columns: new[] { "IdBoatStandard", "Name", "level" },
                values: new object[,]
                {
                    { 1, "podstawowy", 1 },
                    { 2, "tani", 2 },
                    { 3, "średni", 3 },
                    { 4, "na bogato", 4 }
                });

            migrationBuilder.InsertData(
                table: "clientcategory",
                columns: new[] { "IdClientCategory", "DiscountPerc", "Name" },
                values: new object[,]
                {
                    { 1, 0, "plebs" },
                    { 2, 40, "magnat" },
                    { 3, 5, "średni" }
                });

            migrationBuilder.InsertData(
                table: "boat",
                columns: new[] { "IdSailboat", "Capacity", "Description", "IdBoatStandard", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1000, "no łódka no", 1, "ŁODZ", 100.0 },
                    { 2, 500, "no zaglowka no", 4, "zaglowka", 1000.0 },
                    { 3, 100, "no tankowiec no", 3, "tankowiec 1000", 500.0 }
                });

            migrationBuilder.InsertData(
                table: "client",
                columns: new[] { "IdClient", "Birthday", "IdClientCategory", "LastName", "Name", "Pesel", "email" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Klientowski", "Klient", "0000000001", "aaa@aaa.com" },
                    { 2, new DateTime(2004, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Kowalski", "Jan", "0000000002", "bbb@aaa.com" }
                });

            migrationBuilder.InsertData(
                table: "reservation",
                columns: new[] { "IdReservation", "CancelReservation", "Capacity", "DateFrom", "DateTo", "Fullfilled", "IdBoatStandard", "IdClient", "NumOfBoats", "Price" },
                values: new object[,]
                {
                    { 1, null, 50, new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3, 1, 1, 0.0 },
                    { 2, null, 520, new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 2, 2, 0.0 }
                });

            migrationBuilder.InsertData(
                table: "sailboatreservation",
                columns: new[] { "IdReservation", "IdSailboat" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 1, 2 },
                    { 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_boat_IdBoatStandard",
                table: "boat",
                column: "IdBoatStandard");

            migrationBuilder.CreateIndex(
                name: "IX_client_IdClientCategory",
                table: "client",
                column: "IdClientCategory");

            migrationBuilder.CreateIndex(
                name: "IX_reservation_IdBoatStandard",
                table: "reservation",
                column: "IdBoatStandard");

            migrationBuilder.CreateIndex(
                name: "IX_reservation_IdClient",
                table: "reservation",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_sailboatreservation_IdReservation",
                table: "sailboatreservation",
                column: "IdReservation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sailboatreservation");

            migrationBuilder.DropTable(
                name: "boat");

            migrationBuilder.DropTable(
                name: "reservation");

            migrationBuilder.DropTable(
                name: "boatstandard");

            migrationBuilder.DropTable(
                name: "client");

            migrationBuilder.DropTable(
                name: "clientcategory");
        }
    }
}

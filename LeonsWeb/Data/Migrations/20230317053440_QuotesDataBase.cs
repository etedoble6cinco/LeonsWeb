using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeonsWeb.Data.Migrations
{
    public partial class QuotesDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceTypeToShow = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartQuotes = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndQuotes = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndPromoDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartPromoDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfQuotesTaked = table.Column<int>(type: "int", nullable: false),
                    NumberOfQuotesNotTaked = table.Column<int>(type: "int", nullable: false),
                    NumberOfQuote = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServicePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Queues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameQuote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmoutToDiscountQuote = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountTaxQuote = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsTaked = table.Column<bool>(type: "bit", nullable: false),
                    PercentToDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceTypeToShow = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    PromoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Queues_Promos_PromoId",
                        column: x => x.PromoId,
                        principalTable: "Promos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Queues_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Queues_PromoId",
                table: "Queues",
                column: "PromoId");

            migrationBuilder.CreateIndex(
                name: "IX_Queues_ServiceId",
                table: "Queues",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Queues");

            migrationBuilder.DropTable(
                name: "Promos");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}

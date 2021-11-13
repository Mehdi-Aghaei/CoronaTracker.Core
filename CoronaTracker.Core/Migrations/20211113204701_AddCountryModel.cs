using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoronaTracker.Core.Migrations
{
    public partial class AddCountryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iso3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Continent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cases = table.Column<int>(type: "int", nullable: false),
                    TodayCases = table.Column<int>(type: "int", nullable: false),
                    Deaths = table.Column<int>(type: "int", nullable: false),
                    TodayDeaths = table.Column<int>(type: "int", nullable: false),
                    Recovered = table.Column<int>(type: "int", nullable: false),
                    TodayRecovered = table.Column<int>(type: "int", nullable: false),
                    Population = table.Column<int>(type: "int", nullable: false),
                    CasesPerOneMillion = table.Column<int>(type: "int", nullable: false),
                    DeathsPerOneMillion = table.Column<int>(type: "int", nullable: false),
                    RecoveredPerOneMillion = table.Column<float>(type: "real", nullable: false),
                    criticalPerOneMillion = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BelajarEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class SeedCountriesStatesCities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryName" },
                values: new object[,]
                {
                    { 4, "Indonesia" },
                    { 5, "Australia" },
                    { 6, "Canada" },
                    { 7, "Japan" },
                    { 8, "Germany" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "CountryId", "StateName" },
                values: new object[,]
                {
                    { 10, 4, "DKI Jakarta" },
                    { 11, 4, "Jawa Barat" },
                    { 12, 4, "Jawa Tengah" },
                    { 13, 4, "Jawa Timur" },
                    { 14, 4, "Bali" },
                    { 15, 5, "New South Wales" },
                    { 16, 5, "Victoria" },
                    { 17, 6, "Ontario" },
                    { 18, 6, "British Columbia" },
                    { 19, 7, "Tokyo" },
                    { 20, 7, "Osaka" },
                    { 21, 8, "Bavaria" },
                    { 22, 8, "Berlin" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "CityName", "StateId" },
                values: new object[,]
                {
                    { 18, "Jakarta", 10 },
                    { 19, "Bandung", 11 },
                    { 20, "Bekasi", 11 },
                    { 21, "Semarang", 12 },
                    { 22, "Surabaya", 13 },
                    { 23, "Malang", 13 },
                    { 24, "Denpasar", 14 },
                    { 25, "Sydney", 15 },
                    { 26, "Newcastle", 15 },
                    { 27, "Melbourne", 16 },
                    { 28, "Toronto", 17 },
                    { 29, "Ottawa", 17 },
                    { 30, "Vancouver", 18 },
                    { 31, "Tokyo", 19 },
                    { 32, "Shinjuku", 19 },
                    { 33, "Osaka City", 20 },
                    { 34, "Munich", 21 },
                    { 35, "Nuremberg", 21 },
                    { 36, "Berlin", 22 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "StateId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "StateId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "StateId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "StateId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "StateId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "StateId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "StateId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "StateId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "StateId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "StateId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "StateId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "StateId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "StateId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 8);
        }
    }
}

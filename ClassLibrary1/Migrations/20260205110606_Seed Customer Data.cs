using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClassLibrary1.Migrations
{
    /// <inheritdoc />
    public partial class SeedCustomerData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "ID", "Address", "CreditRating", "Name" },
                values: new object[,]
                {
                    { 1, "8 Johnstown Road, Cork", 200f, "Patricia McKenna" },
                    { 2, "Garden House Crowther Way, Dublin", 400f, "Helen Bennett" },
                    { 3, "1900 Oak St., Vancouver", 2000f, "Yoshi Tanami" },
                    { 4, "12 Orchestra Terrace, Dublin 20", 800f, "John Steel" },
                    { 5, "Rue Joseph-Bens 532, Brussels", 600f, "Catherine Dewey" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: 5);
        }
    }
}

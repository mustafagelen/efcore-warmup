using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _14___DataSeeding.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Title" },
                values: new object[] { 11, "Test1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Test1" },
                    { 3, "Test3" }
                });
        }
    }
}

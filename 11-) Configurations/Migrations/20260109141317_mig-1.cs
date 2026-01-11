using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _11___Configurations.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Test_BlogId",
                table: "Posts");

            migrationBuilder.CreateTable(
                name: "Testers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false, computedColumnSql: "[X] * 2")
                },
                constraints: table =>
                {
                });

            migrationBuilder.InsertData(
                table: "Test",
                columns: new[] { "KeyKolonu", "Concurency", "Description", "Title" },
                values: new object[,]
                {
                    { 1, 1, "This is the first blog", "First Blog" },
                    { 2, 1, "This is the second blog", "Second Blog" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Title",
                table: "Testers",
                column: "X");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Blog_CustomName",
                table: "Posts",
                column: "BlogId",
                principalTable: "Test",
                principalColumn: "KeyKolonu",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Blog_CustomName",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Testers");

            migrationBuilder.DeleteData(
                table: "Test",
                keyColumn: "KeyKolonu",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Test",
                keyColumn: "KeyKolonu",
                keyValue: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Test_BlogId",
                table: "Posts",
                column: "BlogId",
                principalTable: "Test",
                principalColumn: "KeyKolonu",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

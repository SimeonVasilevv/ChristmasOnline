using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChristmasOnlineWeb.Migrations
{
    /// <inheritdoc />
    public partial class replaceUniqueWithAutoincrementForDisplayOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_DisplayOrder",
                table: "Categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Categories_DisplayOrder",
                table: "Categories",
                column: "DisplayOrder",
                unique: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationToMM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Photos_UserId",
                schema: "users",
                table: "Photos");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                schema: "users",
                table: "Photos",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Photos_UserId",
                schema: "users",
                table: "Photos");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                schema: "users",
                table: "Photos",
                column: "UserId",
                unique: true);
        }
    }
}

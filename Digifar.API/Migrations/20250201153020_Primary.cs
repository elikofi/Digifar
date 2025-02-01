using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Digifar.API.Migrations
{
    /// <inheritdoc />
    public partial class Primary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Otps_PhoneNumber",
                table: "Otps",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Otps_PhoneNumber",
                table: "Otps");
        }
    }
}

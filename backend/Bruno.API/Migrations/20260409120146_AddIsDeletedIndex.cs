using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bruno.API.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_IsDeleted",
                table: "Vehicles",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_IsDeleted",
                table: "Vehicles");
        }
    }
}

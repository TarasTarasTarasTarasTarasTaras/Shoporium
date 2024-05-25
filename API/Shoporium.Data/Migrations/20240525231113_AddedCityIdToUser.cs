using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoporium.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCityIdToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InnerCityId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_InnerCityId",
                table: "Users",
                column: "InnerCityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_InnerCities_InnerCityId",
                table: "Users",
                column: "InnerCityId",
                principalTable: "InnerCities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_InnerCities_InnerCityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_InnerCityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InnerCityId",
                table: "Users");
        }
    }
}

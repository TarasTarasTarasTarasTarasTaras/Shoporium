using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoporium.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedLoginDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "LoginDetails");

            migrationBuilder.RenameColumn(
                name: "LastLoginAttempt",
                table: "LoginDetails",
                newName: "LastLoginAttemptUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastLoginAttemptUtc",
                table: "LoginDetails",
                newName: "LastLoginAttempt");

            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                table: "LoginDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

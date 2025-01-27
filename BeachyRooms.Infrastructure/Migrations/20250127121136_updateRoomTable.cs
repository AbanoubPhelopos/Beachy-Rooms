using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeachyRooms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateRoomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Rooms",
                newName: "HotelName");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Rooms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Rooms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RoomNumber",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "HotelName",
                table: "Rooms",
                newName: "Name");
        }
    }
}

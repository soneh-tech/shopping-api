using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FemishShoppingAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedClientIDColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientID",
                table: "Sellers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientID",
                table: "Sellers");
        }
    }
}

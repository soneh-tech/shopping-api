using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FemishShoppingAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedMobileDeviceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceID",
                table: "Sellers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MobileDeviceDeviceID",
                table: "Sellers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeviceID",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MobileDeviceDeviceID",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MobileDevices",
                columns: table => new
                {
                    DeviceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileDevices", x => x.DeviceID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_MobileDeviceDeviceID",
                table: "Sellers",
                column: "MobileDeviceDeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_MobileDeviceDeviceID",
                table: "Customers",
                column: "MobileDeviceDeviceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_MobileDevices_MobileDeviceDeviceID",
                table: "Customers",
                column: "MobileDeviceDeviceID",
                principalTable: "MobileDevices",
                principalColumn: "DeviceID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_MobileDevices_MobileDeviceDeviceID",
                table: "Sellers",
                column: "MobileDeviceDeviceID",
                principalTable: "MobileDevices",
                principalColumn: "DeviceID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_MobileDevices_MobileDeviceDeviceID",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_MobileDevices_MobileDeviceDeviceID",
                table: "Sellers");

            migrationBuilder.DropTable(
                name: "MobileDevices");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_MobileDeviceDeviceID",
                table: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_MobileDeviceDeviceID",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeviceID",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "MobileDeviceDeviceID",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "DeviceID",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MobileDeviceDeviceID",
                table: "Customers");
        }
    }
}

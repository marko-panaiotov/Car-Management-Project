using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace car_management_backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCarGarages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarGarage_Cars_CarId",
                table: "CarGarage");

            migrationBuilder.DropForeignKey(
                name: "FK_CarGarage_Garages_GarageId",
                table: "CarGarage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarGarage",
                table: "CarGarage");

            migrationBuilder.RenameTable(
                name: "CarGarage",
                newName: "CarGarages");

            migrationBuilder.RenameIndex(
                name: "IX_CarGarage_GarageId",
                table: "CarGarages",
                newName: "IX_CarGarages_GarageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarGarages",
                table: "CarGarages",
                columns: new[] { "CarId", "GarageId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarGarages_Cars_CarId",
                table: "CarGarages",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarGarages_Garages_GarageId",
                table: "CarGarages",
                column: "GarageId",
                principalTable: "Garages",
                principalColumn: "GarageId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarGarages_Cars_CarId",
                table: "CarGarages");

            migrationBuilder.DropForeignKey(
                name: "FK_CarGarages_Garages_GarageId",
                table: "CarGarages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarGarages",
                table: "CarGarages");

            migrationBuilder.RenameTable(
                name: "CarGarages",
                newName: "CarGarage");

            migrationBuilder.RenameIndex(
                name: "IX_CarGarages_GarageId",
                table: "CarGarage",
                newName: "IX_CarGarage_GarageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarGarage",
                table: "CarGarage",
                columns: new[] { "CarId", "GarageId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarGarage_Cars_CarId",
                table: "CarGarage",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarGarage_Garages_GarageId",
                table: "CarGarage",
                column: "GarageId",
                principalTable: "Garages",
                principalColumn: "GarageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace car_management_backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Garages",
                columns: table => new
                {
                    GarageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GarageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GarageLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GarageCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GarageCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garages", x => x.GarageId);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarManufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarProductionYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarLicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarGarageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                    table.ForeignKey(
                        name: "FK_Cars_Garages_CarGarageId",
                        column: x => x.CarGarageId,
                        principalTable: "Garages",
                        principalColumn: "GarageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Maintenenaces",
                columns: table => new
                {
                    MaintenanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintenanceCarId = table.Column<int>(type: "int", nullable: false),
                    MaintenanceCarName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaintenanceServiceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaintenanceScheduledTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaintenanceGarageId = table.Column<int>(type: "int", nullable: false),
                    MaintenaceGarageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenenaces", x => x.MaintenanceId);
                    table.ForeignKey(
                        name: "FK_Maintenenaces_Cars_MaintenanceCarId",
                        column: x => x.MaintenanceCarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maintenenaces_Garages_MaintenanceGarageId",
                        column: x => x.MaintenanceGarageId,
                        principalTable: "Garages",
                        principalColumn: "GarageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarGarageId",
                table: "Cars",
                column: "CarGarageId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenenaces_MaintenanceCarId",
                table: "Maintenenaces",
                column: "MaintenanceCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenenaces_MaintenanceGarageId",
                table: "Maintenenaces",
                column: "MaintenanceGarageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Maintenenaces");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Garages");
        }
    }
}

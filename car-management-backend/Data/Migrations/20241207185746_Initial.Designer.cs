﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using car_management_backend.Data;

#nullable disable

namespace car_management_backend.Data.Migrations
{
    [DbContext(typeof(CarManagementDbContext))]
    [Migration("20241207185746_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("car_management_backend.Data.Entities.Garage", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarId"));

                    b.Property<int>("CarGarageId")
                        .HasColumnType("int");

                    b.Property<string>("CarLicensePlate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarManufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarModel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CarProductionYear")
                        .HasColumnType("datetime2");

                    b.HasKey("CarId");

                    b.HasIndex("CarGarageId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("car_management_backend.Data.Entities.Garage", b =>
                {
                    b.Property<int>("GarageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GarageId"));

                    b.Property<int>("GarageCapacity")
                        .HasColumnType("int");

                    b.Property<string>("GarageCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GarageLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GarageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GarageId");

                    b.ToTable("Garages");
                });

            modelBuilder.Entity("car_management_backend.Data.Entities.Maintenance", b =>
                {
                    b.Property<int>("MaintenanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaintenanceId"));

                    b.Property<string>("MaintenaceGarageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaintenanceCarId")
                        .HasColumnType("int");

                    b.Property<string>("MaintenanceCarName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaintenanceGarageId")
                        .HasColumnType("int");

                    b.Property<DateTime>("MaintenanceScheduledTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MaintenanceServiceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaintenanceId");

                    b.HasIndex("MaintenanceCarId");

                    b.HasIndex("MaintenanceGarageId");

                    b.ToTable("Maintenenaces");
                });

            modelBuilder.Entity("car_management_backend.Data.Entities.Garage", b =>
                {
                    b.HasOne("car_management_backend.Data.Entities.Garage", "Garage")
                        .WithMany()
                        .HasForeignKey("CarGarageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Garage");
                });

            modelBuilder.Entity("car_management_backend.Data.Entities.Maintenance", b =>
                {
                    b.HasOne("car_management_backend.Data.Entities.Garage", "Garage")
                        .WithMany()
                        .HasForeignKey("MaintenanceCarId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("car_management_backend.Data.Entities.Garage", "Garage")
                        .WithMany()
                        .HasForeignKey("MaintenanceGarageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Garage");

                    b.Navigation("Garage");
                });
#pragma warning restore 612, 618
        }
    }
}

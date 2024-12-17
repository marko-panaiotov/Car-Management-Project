﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using car_management_backend.Data;

#nullable disable

namespace car_management_backend.Data.Migrations
{
    [DbContext(typeof(CarManagementDbContext))]
    partial class CarManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("car_management_backend.Data.Entities.Car", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarId"));

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductionYear")
                        .HasColumnType("int");

                    b.HasKey("CarId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("car_management_backend.Data.Entities.CarGarage", b =>
                {
                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("GarageId")
                        .HasColumnType("int");

                    b.HasKey("CarId", "GarageId");

                    b.HasIndex("GarageId");

                    b.ToTable("CarGarage");
                });

            modelBuilder.Entity("car_management_backend.Data.Entities.Garage", b =>
                {
                    b.Property<int>("GarageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GarageId"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GarageId");

                    b.ToTable("Garages");
                });

            modelBuilder.Entity("car_management_backend.Data.Entities.GarageReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AvailableCapacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("GarageId")
                        .HasColumnType("int");

                    b.Property<int>("Requests")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GarageId");

                    b.ToTable("GarageReports");
                });

            modelBuilder.Entity("car_management_backend.Data.Entities.Maintenance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("CarName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GarageId")
                        .HasColumnType("int");

                    b.Property<string>("GarageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ScheduledDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ServiceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("GarageId");

                    b.ToTable("Maintenances");
                });

            modelBuilder.Entity("car_management_backend.Data.Entities.CarGarage", b =>
                {
                    b.HasOne("car_management_backend.Data.Entities.Car", "Car")
                        .WithMany("CarGarages")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("car_management_backend.Data.Entities.Garage", "Garage")
                        .WithMany("CarGarages")
                        .HasForeignKey("GarageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Garage");
                });

            modelBuilder.Entity("car_management_backend.Data.Entities.GarageReport", b =>
                {
                    b.HasOne("car_management_backend.Data.Entities.Garage", "Garage")
                        .WithMany()
                        .HasForeignKey("GarageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Garage");
                });

            modelBuilder.Entity("car_management_backend.Data.Entities.Maintenance", b =>
                {
                    b.HasOne("car_management_backend.Data.Entities.Car", "Car")
                        .WithMany("Maintenances")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("car_management_backend.Data.Entities.Garage", "Garage")
                        .WithMany("Maintenances")
                        .HasForeignKey("GarageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Garage");
                });

            modelBuilder.Entity("car_management_backend.Data.Entities.Car", b =>
                {
                    b.Navigation("CarGarages");

                    b.Navigation("Maintenances");
                });

            modelBuilder.Entity("car_management_backend.Data.Entities.Garage", b =>
                {
                    b.Navigation("CarGarages");

                    b.Navigation("Maintenances");
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using car_management_backend.Data.Entities;
using System.Reflection.Metadata;

namespace car_management_backend.Data
{
    public class CarManagementDbContext : DbContext
    {


        public DbSet<Car> Cars { get; set; }
        public DbSet<Garage> Garages { get; set; }
        public DbSet<Maintenance> Maintenenaces { get; set; }
        public DbSet<CarGarage> CarGarages { get; set; } = null!;

        public CarManagementDbContext(DbContextOptions<CarManagementDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Maintenance>()
            .HasOne(m => m.Garage)
            .WithMany()
            .HasForeignKey(m => m.GarageId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Maintenance>()
            .HasOne(m => m.Car)
            .WithMany()
            .HasForeignKey(m => m.CarId)
            .OnDelete(DeleteBehavior.Restrict);

            /*modelBuilder.Entity<Car>()
            .HasOne(c => c.Garage)
            .WithMany()
            .HasForeignKey(c => c.CarGarageId)
            .OnDelete(DeleteBehavior.Restrict);*/

            modelBuilder.Entity<CarGarage>()
                .HasKey(cg => new { cg.CarId, cg.GarageId });

            modelBuilder.Entity<CarGarage>()
                .HasOne(cg => cg.Car)
                .WithMany(c => c.CarGarages)
                .HasForeignKey(cg => cg.CarId);

            modelBuilder.Entity<CarGarage>()
                .HasOne(cg => cg.Garage)
                .WithMany(g => g.CarGarages)
                .HasForeignKey(cg => cg.GarageId);

        }

    }

}


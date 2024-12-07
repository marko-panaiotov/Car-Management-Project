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

        public CarManagementDbContext(DbContextOptions<CarManagementDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Maintenance>()
            .HasOne(m => m.Garage)
            .WithMany()
            .HasForeignKey(m => m.MaintenanceGarageId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Maintenance>()
            .HasOne(m => m.Car)
            .WithMany()
            .HasForeignKey(m => m.MaintenanceCarId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
            .HasOne(c => c.Garage)
            .WithMany()
            .HasForeignKey(c => c.CarGarageId)
            .OnDelete(DeleteBehavior.Restrict);

        }

    }

}


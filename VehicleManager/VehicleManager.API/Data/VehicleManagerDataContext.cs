using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VehicleManager.API.Models;

namespace VehicleManager.API.Data
{
    public class VehicleManagerDataContext : DbContext
    {
        //Con
        public VehicleManagerDataContext() : base("VehicleManager")
        {

        }
        //DBsets
        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<Sale> Sales { get; set; }
        public IDbSet<Vehicle> Vehicles { get; set; }
        public IDbSet<VehicleType> VehicleTypes { get; set; }
        public IDbSet<Color> Colors { get; set; }

        //Model Config
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //vehicle has many sale
            modelBuilder.Entity<Vehicle>()
                         .HasMany(vehicle => vehicle.Sales)
                         .WithRequired(sale => sale.Vehicle)
                         .HasForeignKey(Sale => Sale.VehicleID);

            //Customer has many sales
            modelBuilder.Entity<Customer>()
                          .HasMany(Customer => Customer.Sales)
                          .WithRequired(sale => sale.Customer)
                          .HasForeignKey(Sale => Sale.CustomerID);

            //Color has many
            modelBuilder.Entity<Color>()
                    .HasMany(color => color.Vehicle)
                    .WithRequired(vehicle => vehicle.Color)
			        .HasForeignKey(vehicle => vehicle.ColorID);

            //V-Type has many
            modelBuilder.Entity<VehicleType>()
                    .HasMany(VehicleType => VehicleType.Vehicle)
                    .WithRequired(vehicle => vehicle.VehicleType)
                    .HasForeignKey(vehicle => vehicle.VehicleTypeID);
        }
    }
}
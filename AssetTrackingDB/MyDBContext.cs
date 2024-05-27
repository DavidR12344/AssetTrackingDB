using AssetTrackingDB.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingDB
{
    public class MyDBContext : DbContext
    {

        public DbSet<Electronic> electronicsDB { get; set; }
        //public DbSet<Computer> computers { get; set; }
        /// public DbSet<Mobile> mobiles { get; set; }

        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Order;Integrated Security=True";

        /// <summary>
        /// Set up a connection to local database with connections string
        /// </summary>
        /// <param name="optionsBuilder"></param>

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Electronic>()
                     .HasKey(e => e.Id); // Assuming there's an Id property

            modelBuilder.Entity<Electronic>()
                .Property(e => e.OfficeName)
                .IsRequired()
            .HasMaxLength(100);

            modelBuilder.Entity<Electronic>()
                .Property(e => e.Type)
                .IsRequired()
            .HasMaxLength(50);

            modelBuilder.Entity<Electronic>()
                .Property(e => e.Brand)
                .IsRequired()
            .HasMaxLength(50);

            modelBuilder.Entity<Electronic>()
                .Property(e => e.Model)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Electronic>()
                .Property(e => e.PurchasedDate)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<Electronic>()
                .Property(e => e.Currency)
                .IsRequired()
                .HasMaxLength(3);

           modelBuilder.Entity<Computer>().HasBaseType<Electronic>();
            modelBuilder.Entity<Mobile>().HasBaseType<Electronic>();*/

        }
    }
}

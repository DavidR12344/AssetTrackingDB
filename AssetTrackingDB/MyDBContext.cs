using AssetTrackingDB.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingDB
{
    public class MyDBContext: DbContext
    {
        public DbSet<Electronic> electronicsDB { get; set; }
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Order;Integrated Security=True";


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
        }
    }
}

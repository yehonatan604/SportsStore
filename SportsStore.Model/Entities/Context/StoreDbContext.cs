using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Model.Items;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using SportsStore.Model.Users;
using SportsStore.Model.Costumers;

namespace SportsStore.Model
{
    
    public class StoreContext : DbContext
    {
        // User DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Logged> LoggedIns { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<MessageBox> MessageBoxes { get; set; }
        public DbSet<Message> Messages { get; set; }

        // Customer DbSet

        public DbSet<Customer> customers { get; set; }

        // Accessories DbSets
        public DbSet<Item> Items { get; set; }
        public DbSet<Clothe> Clothes { get; set; }
        public DbSet<Ball> Balls { get; set; }
        public DbSet<Bat> Bats { get; set; }
        public DbSet<Net> Nets { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SalesView> SaleViews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder) => 
            optionBuilder.UseSqlServer("Server=DESKTOP-T74S10A;Database=NewSportsStore;Trusted_Connection = True;");
    }
}

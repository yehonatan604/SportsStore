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

        // Customer DbSet

        public DbSet<Customer> Customers { get; set; }

        // Accessories DbSets
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SalesView> SaleViews { get; set; }
        public DbSet<UsersView> usersView { get; set; }
        public DbSet<LogsView> logsView { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder) => 
            optionBuilder.UseSqlServer("Server=DESKTOP-T74S10A;Database=MyStore;Trusted_Connection = True;");
    }
}

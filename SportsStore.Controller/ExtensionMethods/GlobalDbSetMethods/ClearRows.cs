using Microsoft.EntityFrameworkCore;
using SportsStore.Model;
using SportsStore.Model.Costumers;
using SportsStore.Model.Items;
using SportsStore.Model.Users;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Controller
{
    public static class ClearRows
    {
        public static void Clear<T>(this DbSet<T> dbSet, DbContext db) where T : class
        {
            dbSet.RemoveRange(dbSet);
            db.SaveChanges();
        }
    }
}

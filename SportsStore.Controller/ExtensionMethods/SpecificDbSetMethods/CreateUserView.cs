using Microsoft.EntityFrameworkCore;
using SportsStore.Model.Items;
using SportsStore.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Controller
{
    public static class CreateUserView
    {
        public static void CreateView(this DbSet<UsersView> Db, DbContext dataBase, User user)
        {
            Db.Add(new UsersView
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    HireDate = user.HireDate,
                    UserType = user.UserType.ToString(),
                    LastSale = user.LastSale,
                    SalesCount = user.SalesCount,
                    SalesTotal = user.SalesTotal
                });
            dataBase.SaveChanges();
        }
    }
}

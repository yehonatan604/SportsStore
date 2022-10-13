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
    public static class CreateLogsView
    {
        public static void CreateView(this DbSet<LogsView> Db, DbContext dataBase, Log log)
        {
            Db.Add(new LogsView
            {
                LogId = log.Id,
                UserId = log.User.Id,
                FirstName = log.User.FirstName,
                LastName = log.User.LastName,
                UserType = log.User.UserType.ToString(),
                ActionType = log.ActionType.ToString(),
                Description = log.Description,
                DateTime = log.DateTime,
            });
            dataBase.SaveChanges();
        }
    }
}

using SportsStore.Enums;
using SportsStore.Model.Users;
using SportStore.Controller.DbConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Controller
{
    public abstract class DbCrud : IDbConnectable
    {
        public virtual int NoUserId { get; set; }
        public static User? LoggedInUser { get; set; }
        public static string? LoggedUserEmail { get; set; }
        public static bool IsRememberMe { get; set; }

        public DbCrud()
        {
            IDbConnectable.Db = DbConnector.GetInstance(this).Db;
            NoUserId = IDbConnectable.Db.Users.Single(x => x.UserType == UserTypes.No_User).Id;
            LoggedUserEmail = string.Empty;
            IsRememberMe = false;
        }

        public static void ChangeLoggedUserEmail(string email)
        {
            LoggedUserEmail = email;
        }
        public static void AddLoggedIn(int id)
        {
            if (IDbConnectable.Db is not null)
            {
                IDbConnectable.Db.LoggedIns.Add(new() { DateTime = DateTime.Now, User = IDbConnectable.Db.Users.Single(x => x.Id == id) });
                IDbConnectable.Db.SaveChanges();
                LoggedUserEmail = IDbConnectable.Db.Users.Single(x => x.Id == id).Email;
            }
        }
    }
}

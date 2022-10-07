using SportsStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Controller
{
    public class Delete : DbCrud, IDbDeletable
    {
        private readonly StoreContext? db;

        public Delete() : base()
        {
            db = IDbConnectable.Db;
        }

        // Clear Table Methods
        public void ClearLogs()
        {
            db.Logs.Clear(db);
        }
        public void ClearLoggedIns()
        {
            db.LoggedIns.Clear(db);
        }

        // Remove record Meethods
        public void RemoveUser(int id)
        {
            db.Users.Remove(db.Users.Single(user => user.Id == id));
            db.SaveChanges();
        }
    }
}
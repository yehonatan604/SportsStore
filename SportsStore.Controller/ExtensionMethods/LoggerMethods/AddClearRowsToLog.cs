using Microsoft.EntityFrameworkCore;
using SportsStore.Enums;

namespace SportsStore.Controller
{
    public static class AddClearRowsToLog
    {
        public static void AddClearRowsLog<T>(this DbSet<T> dbSet, DbContext db, LogEntityHandler logger) where T : class
        {
            logger.Add(new string[] { EntityHandler.LoggedInUser.Id.ToString(), $"{EntityHandler.LoggedInUser.Id} Cleared all rows in Db: {dbSet.GetType().BaseType}", ActionTypes.ClearAllRows.ToString() });
            db.SaveChanges();
        }
    }
}

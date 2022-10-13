using Microsoft.EntityFrameworkCore;
using SportsStore.Enums;

namespace SportsStore.Controller
{
    public static class AddUpdateToLogs
    {
        public static void AddUpdateLog<T>(this DbSet<T> dbSet, DbContext db, LogEntityHandler logger) where T : class
        {
            logger.Add(new string[] { EntityHandler.LoggedInUser.Id.ToString(), $"{EntityHandler.LoggedInUser.Id} Updated Db: {dbSet.GetType().BaseType}", ActionTypes.Update.ToString() });
            db.SaveChanges();
        }
    }
}

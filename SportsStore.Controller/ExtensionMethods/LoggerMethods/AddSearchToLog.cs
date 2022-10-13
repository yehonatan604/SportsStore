using Microsoft.EntityFrameworkCore;
using SportsStore.Enums;

namespace SportsStore.Controller
{
    public static class AddSearchToLog
    {
        public static void AddSearchLog<T>(this DbSet<T> dbSet, DbContext db, LogEntityHandler logger) where T : class
        {
            logger.Add(new string[] { EntityHandler.LoggedInUser.Id.ToString(), $"{EntityHandler.LoggedInUser.Id} Searched in Db: {dbSet.GetType().BaseType}", ActionTypes.Search.ToString() });
            db.SaveChanges();
        }
    }
}

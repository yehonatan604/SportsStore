using Microsoft.EntityFrameworkCore;
using SportsStore.Enums;

namespace SportsStore.Controller
{
    public static class AddGenerateRecordsToLogs
    {
        public static void AddGenerateRecordsLog<T>(this DbSet<T> dbSet, DbContext db, LogEntityHandler logger) where T : class
        {
            logger.Add(new string[] { EntityHandler.LoggedInUser.Id.ToString(), $"{EntityHandler.LoggedInUser.Id} Generated records in Db: {dbSet.GetType().BaseType}", ActionTypes.GenerateRecords.ToString() });
            db.SaveChanges();
        }
    }
}

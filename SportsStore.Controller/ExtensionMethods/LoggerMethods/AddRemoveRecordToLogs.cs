using Microsoft.EntityFrameworkCore;
using SportsStore.Enums;

namespace SportsStore.Controller
{
    public static class AddRemoveRecordToLogs
    {
        public static void AddRemoveRecordLog<T>(this DbSet<T> dbSet, DbContext db, LogEntityHandler logger, int id) where T : class
        {
            logger.Add(new string[] { EntityHandler.LoggedInUser.Id.ToString(), $"{EntityHandler.LoggedInUser.Id} Removed RecordId:{id} from Db: {dbSet.GetType().BaseType}", ActionTypes.RemoveRecord.ToString() });
            db.SaveChanges();
        }
    }
}

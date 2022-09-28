using SportsStore.Model;

namespace SportStore.Controller.DbConnector
{
    // A Singleton Db Connector
    public class DbConnector : IDbConnector 
    {
        private readonly StoreContext db;
        private static DbConnector? instance;
        private static readonly object key = new();

        private DbConnector()
        {
            db = new StoreContext(this);
        }
        // call this method to get the singleton instance
        public static DbConnector GetInstance()
        {
            lock (key)
            {
                instance ??= new DbConnector();
                return instance;
            }
        }
        // call this method to get the db
        public StoreContext GetDb()
        {
            return db;
        }
    }
}

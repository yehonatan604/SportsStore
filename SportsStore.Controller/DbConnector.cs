using SportsStore.Model;

namespace SportStore.Controller.DbConnector
{
    public interface IDbConnectable
    {
        // Mark Interface
    }
    
    // A Singleton Db Connector
    public class DbConnector 
    {
        public StoreContext Db { get; set; }
        public DbConnector Connect { get; set; }

        private static DbConnector? instance;
        private static readonly object key = new();

        private DbConnector()
        {
            Db = new StoreContext();
        }

        // call this method to get the singleton instance - if you are IDbConnectable
        public static DbConnector GetInstance(IDbConnectable dbConnectable)
        {
            _ = dbConnectable ?? throw new NotImplementedException();

            lock (key)
            {
                instance ??= new DbConnector();
                return instance;
            }
        }
    }
}

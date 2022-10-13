using SportsStore.Enums;
using SportsStore.Model.Users;

namespace SportsStore.Controller
{
    public class LogEntityHandler : EntityHandler, IEntityHandalable
    {
        public static LogEntityHandler Logger { get; private set; }

        public LogEntityHandler()
        {
            Logger = this;
        }
        //Create
        public void Add(params string[] args)
        {
            int id = Convert.ToInt32(args[0]);
            ActionTypes actionType = (ActionTypes)Enum.Parse(typeof(ActionTypes), args[2]);

            Log log = new()
            {
                User = Db.Users.Single(x => x.Id == id),
                DateTime = DateTime.Now,
                Description = args[1],
                ActionType = actionType
            };
            Db.Logs.Add(log);
            Db.logsView.CreateView(Db, log);
            Db.SaveChanges();
        }
        public void AddSearchLog() => Db.Logs.AddSearchLog(Db, Logger);
        public void Generate() => throw new NotImplementedException();

        //Read
        public IEnumerable<object> GetTable(string s, params string[] args) => s switch
        {
            "ByUserType" => from logs in Db.logsView
                            where logs.UserType == args[0]
                            select logs,

            "ByActionType" => from logs in Db.logsView
                              where logs.ActionType == args[0]
                              select logs,

            "ByAction_Date" => from logs in Db.logsView
                               where logs.DateTime.ToString() == args[0]
                               select logs,

            "ByUserType_ActionType" => from logs in Db.logsView
                                       where logs.UserType == args[0] &&
                                             logs.ActionType == args[1]
                                       select logs,

            "ByUserType_Date" => from logs in Db.logsView
                                 where logs.UserType == args[0] &&
                                       logs.DateTime.ToString() == args[1]
                                 select logs,

            "ByActionType_Date" => from logs in Db.logsView
                                   where logs.ActionType == args[0] &&
                                         logs.DateTime.ToString() == args[1]
                                   select logs,

            _ => Db.logsView
        };
        public List<string> GetList(string s, params string[] args) => s switch
        {
            "ByLogAction" => (from log in Db.Logs
                              select log.ActionType.ToString()).Distinct().ToList(),

            "ByLogDate" => (from log in Db.Logs
                            select log.DateTime.Date.ToString()).Distinct().ToList(),

            _ => new List<string>()
        };
        public IEnumerable<object> Search(string s, string arg = "") => s switch
        {
            "ById" => from logs in Db.Logs
                      where (logs.User.Id.ToString()).Contains(arg)
                      select logs,

            "ByName" => from logs in Db.Logs
                        where (logs.User.FirstName).Contains(arg) || (logs.User.LastName).Contains(arg)
                        select logs,

            _ => new List<string>()
        };
        public List<string> GetStats() => 
            throw new NotImplementedException();

        //Update
        public bool Update(string s, params string[] args) => throw new NotImplementedException();
        
        //Delete
        public void Clear()
        {
            Db.Logs.Clear(Db);
            Db.Logs.AddClearRowsLog(Db, Logger);
        }
        public void Remove(int id) => throw new NotImplementedException();
    }
}

using SportsStore.Enums;
using SportsStore.Model.Users;

namespace SportsStore.Controller
{
    public class UserEntityHandler : EntityHandler, IEntityHandalable
    {
        private readonly LogEntityHandler Logger = LogEntityHandler.Logger;

        //Create
        public void Add(params string[] args)
        {
            string description = string.Empty;
            ActionTypes actionType = ActionTypes.Register;
            try
            {
                UserTypes userType = (UserTypes)Enum.Parse(typeof(UserTypes), args[2]);
                User user = new(args[0], args[1], userType, args[3], args[4]);
                Db.Users.Add(user);
                Db.usersView.CreateView(Db, user);
                string id = Db.Users.Skip(Db.Users.Count() - 1).First().Id.ToString();
                description = $"Id:{id} - {args[0]} Registered Succesfully";
            }
            catch
            {
                description = $"Registeration Failed!!!";
                actionType = ActionTypes.RegisterFailed;
            }
            finally
            {
                Logger.Add(new string[] { NoUserId.ToString(), description, actionType.ToString() });
                Db.SaveChanges();
            }
        }
        public void Generate()
        {
            List<User> users = new()
            {
                new("Karim", "Jabar", UserTypes.Manager, "Joee@email.com", 1234.ToString() ),
                new( "Joee", "Montana", UserTypes.SalesMan, "Joee@email.com", 1234.ToString() ),
                new ( "Rita", "Shemesh", UserTypes.SalesMan, "Joee@email.com", 1234.ToString() ),
                new ( "Yuli", "Zorer", UserTypes.SalesMan, "Joee@email.com", 1234.ToString() ),
                new ("Jerry", "Tom", UserTypes.SalesMan, "Joee@email.com", 1234.ToString() )
            };
            users.ForEach(user => Add(new string[] { user.FirstName, user.LastName, user.UserType.ToString(), user.Email, user.Password }));
            
            Db.Users.AddGenerateRecordsLog(Db, Logger);
            Db.SaveChanges();
        }
        public void AddSearchLog() => Db.Users.AddSearchLog(Db, Logger);

        //Read
        public IEnumerable<object> GetTable(string s, params string[] args) => s switch
        {
            "ByType" => from user in Db.usersView
                        where user.UserType == args[0]
                        select user,

            "BySaleDate" => from user in Db.usersView
                            join sale in Db.Sales
                            on user.Id equals sale.User.Id
                            where sale.SaleDate.Date.ToString() == args[0]
                            select user,

            "ByHireYear" => from user in Db.usersView
                            where user.HireDate.Year.ToString() == args[0]
                            select user,

            "ByType_SaleDate" => from user in Db.usersView
                                 join sale in Db.Sales
                                 on user.Id equals sale.User.Id
                                 where user.UserType == args[0] && sale.SaleDate.Date.ToString() == args[1]
                                 select user,

            "ByType_HireYear" => from user in Db.usersView
                                 where user.UserType == args[0] && user.HireDate.Year.ToString() == args[1]
                                 select user,

            "BySaleDate_HireYear" => from user in Db.usersView
                                     join sale in Db.Sales
                                 on user.Id equals sale.User.Id
                                     where sale.SaleDate.Date.ToString() == args[0] && user.HireDate.Year.ToString() == args[1]
                                     select user,

            "ByAll" => (from user in Db.usersView
                        join sale in Db.Sales
                         on user.Id equals sale.User.Id
                        where
                         user.UserType == args[0] &&
                         sale.SaleDate.Date.ToString() == args[1] &&
                         user.HireDate.Year.ToString() == args[2]
                        select user).Distinct(),

            _ => Db.usersView
        };
        public List<string> GetList(string s, params string[] args) => s switch
        {

            "ByHireDate" => (from user in Db.Users
                             select user.HireDate.Year.ToString()).Distinct().ToList(),

            "Users" => (from user in Db.Users
                        select user.Id.ToString()).ToList(),

            _ => new List<string>()
        };
        public IEnumerable<object> Search(string s, string arg = "") => s switch
        {
            "Name" => from user in Db.Users
                      where (user.FirstName).Contains(arg) || (user.LastName).Contains(arg)
                      select user,

            "Id" => from user in Db.Users
                    where (user.Id.ToString()).Contains(arg)
                    select user
        };
        public List<string> GetStats() =>
            throw new NotImplementedException();
        public string GetUserType(int id) => Db.Users.Single(user => user.Id == id).UserType.ToString();

        // Update
        public bool Update(string s, params string[] args)
        {
            try
            {
                int userId = Convert.ToInt16(args[0]);
                switch (s)
                {
                    case "UserType":
                        {
                            Db.Users.Single(user => user.Id == userId).UserType = (UserTypes)Enum.Parse(typeof(UserTypes), args[1]);
                            break;
                        }
                    case "Email":
                        {
                            Db.Users.Single(user => user.Id == userId).Email = args[2];
                            break;
                        }
                    case "HireDate":
                        {
                            Db.Users.Single(user => user.Id == userId).HireDate = Convert.ToDateTime(args[3]).Date;
                            break;
                        }
                    case "Password":
                        {
                            Db.Users.Single(user => user.Id == userId).Password = args[4];
                            break;
                        }
                    case "RememberMe":
                        {
                            Db.Users.Single(user => user.Id == userId).RememberMe = args[5] == true.ToString() ?
                            !Db.Users.Single(user => user.Id == userId).RememberMe : Db.Users.Single(user => user.Id == userId).RememberMe;
                            break;
                        }
                }
                Db.Users.AddUpdateLog(Db, Logger);
                Db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Delete
        public void Clear()
        {
            Db.Users.Clear(Db);
            Db.Users.AddClearRowsLog(Db, Logger);
        }
        public void Remove(int id)
        {
            Db.Users.Remove(Db.Users.Single(user => user.Id == id));
            Db.Users.AddRemoveRecordLog(Db, Logger, id);
            Db.SaveChanges();
        }
    }
}

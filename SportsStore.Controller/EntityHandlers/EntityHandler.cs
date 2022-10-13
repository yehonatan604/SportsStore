using SportsStore.Enums;
using SportsStore.Model;
using SportsStore.Model.Users;
using SportStore.Controller.DbConnector;

namespace SportsStore.Controller
{
    public abstract class EntityHandler : IDbConnectable
    {
        protected readonly StoreContext? Db;
        public virtual int NoUserId { get; set; }
        public static User? LoggedInUser { get; set; }
        public static string? LoggedUserEmail { get; set; }
        public static bool IsRememberMe { get; set; }

        public EntityHandler()
        {
            IDbConnectable.Db = DbConnector.GetInstance(this).Db;
            Db = IDbConnectable.Db;
            if (!Db.Users.Any())
            {
                Db.Users.Add(new User("NO_USER", "NO_USER", (UserTypes)500, "NO_USER", "1"));
                Db.Users.Add(new User("Admin", "Admin", (UserTypes)0, "Admin", "Admin1234"));
                Db.SaveChanges();
            }
            NoUserId = Db.Users.Single(user => user.UserType == UserTypes.No_User).Id;
        }

        public virtual bool CheckLastSessionExit()
        {
            ActionTypes actionType = ActionTypes.Exit;
            if (Db.Logs.Any())
            {
                actionType = (from action in Db.Logs
                              orderby action.Id ascending
                              select action.ActionType).Last();
            }

            return actionType == ActionTypes.Exit;
        }
        public virtual bool CheckAvailability(string email)
        {
            var record = from user in Db.Users
                         where user.Email == email
                         select user;

            return !record.Any();
        }
        public virtual bool CheckLogin(string email, string password)
        {
            var record = from user in Db.Users
                         where user.Email == email && user.Password == password
                         select user.Id;

            ActionTypes actionType = record.Any() ? ActionTypes.Login : ActionTypes.LoginFailed;
            string text = record.Any() ? $"{email} Logged in succesfully" : "Login failed";
            return record.Any();
        }
        public virtual bool CheckLoggedIns()
        {
            return Db.LoggedIns.Any();
        }
        public virtual int CheckAuthorizationLevel()
        {
            return (int)LoggedInUser.UserType;
        }

        protected virtual void AddLoggedIn(int id)
        {
            if (IDbConnectable.Db is not null)
            {
                IDbConnectable.Db.LoggedIns.Add(new() { DateTime = DateTime.Now, User = IDbConnectable.Db.Users.Single(x => x.Id == id) });
                IDbConnectable.Db.SaveChanges();
                LoggedUserEmail = IDbConnectable.Db.Users.Single(x => x.Id == id).Email;
            }
        }
        protected virtual void ClearLoggedIns()
        {
            Db.LoggedIns.Clear(Db);
        }

        public virtual void OnExitProgram() // should be called when Window Closes
        {
            LogEntityHandler handler = new();
            int userId = LoggedInUser != null ? LoggedInUser.Id : NoUserId;

            if (userId != NoUserId)
            {
                if (!LoggedInUser.RememberMe)
                {
                    ClearLoggedIns();
                    handler.Add(new string[] { LoggedInUser.Id.ToString(), $"{LoggedInUser.Id} logged off", ActionTypes.Logoff.ToString() });
                }
            }
            handler.Add(userId.ToString(), $"id:{userId} exit program", ActionTypes.Exit.ToString());
            Environment.Exit(0);
        }
        public virtual void OnStartProgram() // should be called when Window Start
        {
            LogEntityHandler handler = new();
            int userId = NoUserId;

            // firstly, checks if last session crushed.
            if (!new UserEntityHandler().CheckLastSessionExit())
            {
                handler.Add(new string[] { NoUserId.ToString(), $"crush was found, previous session did not exit", ActionTypes.Crush.ToString() });
            }
            // if no user logged in, system exit to desktop:
            if (LoggedUserEmail is null && !Db.LoggedIns.Any())
            {
                OnExitProgram();
            }

            // if LoggedIns entity is empty, we get the id by the LoggedUserEmail:
            if (!Db.LoggedIns.Any())
            {
                userId = Db.Users.Single(x => x.Email == LoggedUserEmail).Id;
                LoggedInUser = (from user in Db.Users
                                orderby user.Id
                                where user.Id == userId
                                select user).Last();
                if (IsRememberMe)
                {
                    LoggedInUser.RememberMe = true;
                }
            }
            // else we get the id & email from the LoggedIns entity:
            else
            {
                LoggedInUser = (from user in Db.LoggedIns
                                orderby user.Id
                                select user.User).Last();

                LoggedUserEmail = LoggedInUser.Email;
                userId = LoggedInUser.Id;

                if (LoggedInUser.RememberMe)
                {
                    IsRememberMe = true;
                }
            }
            ClearLoggedIns();
            AddLoggedIn(userId);
            handler.Add(new string[] { LoggedInUser.Id.ToString(), $"id: {LoggedInUser.Id} email:{LoggedUserEmail} logged in", ActionTypes.Login.ToString() });
            handler.Add(new string[] { LoggedInUser.Id.ToString(), $"id:{LoggedInUser.Id} start program", ActionTypes.StartUp.ToString() });
        }
    }
}

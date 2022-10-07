using SportsStore.Controller.CRUD;
using SportsStore.Enums;
using SportsStore.Model;
using SportsStore.Model.Costumers;
using SportsStore.Model.Items;
using SportsStore.Model.Users;
using SportStore.Controller.DbConnector;

namespace SportsStore.Controller
{
    public class Create : DbCrud, IDbCreatable
    { 
        // Db Connector Access
        private readonly StoreContext? db;
        
        // Constructor
        public Create() : base()
        {
            db = IDbConnectable.Db;
            if (!db.Users.Any())
            {
                db.Users.Add(new User("NO_USER", "NO_USER", (UserTypes)500, "NO_USER", "1"));
                db.Users.Add(new User("Admin", "Admin", (UserTypes)0, "Admin", "Admin1234"));
                db.SaveChanges();
            }
        }

        // Add to log Methods
        public void AddLog(int id, string description, ActionTypes actionType)
        {
            db.Logs.Add(new()
            {
                User = db.Users.Single(x => x.Id == id),
                DateTime = DateTime.Now,
                Description = description,
                ActionType = actionType
            });
            db.SaveChanges();
        }

        // Add to Db Methods
        public bool AddStock(int itemId, int quantity)
        {
            
                var id = from stock in db.Stocks
                         where stock.Item.Id == itemId
                         select stock;

                if (id.Any())
                {
                    db.Stocks.Single(x => x.Item.Id == itemId).Quantity += quantity;
                    db.Stocks.Single(x => x.Item.Id == itemId).LastAdded = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }

                db.Stocks.Add(new()
                {
                    Item = db.Items.Single(x => x.Id == itemId),
                    Quantity = quantity,
                    LastAdded = DateTime.Now,
                    ThisItemId = db.Items.Single(x => x.Id == itemId).Id,
                    ItemName = db.Items.Single(x => x.Id == itemId).Name,
                    ItemType = db.Items.Single(x => x.Id == itemId).ItemType,
                    ItemInnerType = db.Items.Single(x => x.Id == itemId).ItemInnerType,
                    ItemPrice = db.Items.Single(x => x.Id == itemId).Price,
                    ItemColor = db.Items.Single(x => x.Id == itemId).Color,
                    ItemSize = db.Items.Single(x => x.Id == itemId).Size,
                    ItemCreated = db.Items.Single(x => x.Id == itemId).Created,
                });
                db.SaveChanges();
                return true;
            
        }
        public bool AddSale(int itemId, int quantity, int customerID)
        {
            if (quantity <= db.Stocks.Single(x => x.Item.Id == itemId).Quantity && LoggedInUser is not null)
            {
                Item item = db.Items.Single(x => x.Id == itemId);
                Customer customer = db.customers.Single(x => x.Id == customerID);
                double price = item.Price * quantity;

                db.Stocks.Single(x => x.Item.Id == itemId).Quantity -= quantity;

                customer.PurchasesCount = customer.PurchasesCount is null ? 1 : customer.PurchasesCount += 1;
                customer.TotalPurchases = customer.TotalPurchases is null ? price : customer.TotalPurchases += price;
                customer.LastPurchase = DateTime.Now;

                LoggedInUser.SalesCount = LoggedInUser.SalesCount is null ? 1 : LoggedInUser.SalesCount += 1;
                LoggedInUser.SalesTotal = LoggedInUser.SalesTotal is null ? price : LoggedInUser.SalesTotal += price;
                LoggedInUser.LastSale = DateTime.Now;

                db.Sales.Add(new()
                {
                    Item = item,
                    Quantity = quantity,
                    Customer = customer,
                    TotalPrice = price,
                    SaleDate = DateTime.Now,
                    User = LoggedInUser
                });

                db.SaleViews.Add(new()
                {
                    Quantity = quantity,
                    TotalPrice = price,
                    SaleDate = DateTime.Now,
                    ThisItemId = item.Id,
                    ItemName = item.Name,
                    ItemType = item.ItemType,
                    ItemInnerType = item.ItemInnerType,
                    ItemPrice = item.Price,
                    ItemColor = item.Color ?? string.Empty,
                    ItemSize = item.Size ?? string.Empty,
                    SalsemanId = LoggedInUser.Id,
                    SalsemanFname = LoggedInUser.FirstName,
                    SalsemanLname = LoggedInUser.LastName
                });
                db.SaveChanges();

                AddLog(LoggedInUser.Id, $"Sale! {LoggedInUser.Id} made a sale: {price}$", ActionTypes.Sale);
                return true;
            }
            return false;
        }
        public bool AddItem(string itemType, string name, double price, string color, string itemInnerType, string size = "")
        {
            try
            {

                ItemCreator creator = itemType switch
                {
                    "Ball" => new BallCreator(name, price, color, itemInnerType, size),
                    "Bat" => new BatCreator(name, price, color, itemInnerType, size),
                    "Net" => new NetCreator(name, price, color, itemInnerType, size),
                    "Clothe" => new ClotheCreator(name, price, color, itemInnerType, size),
                    _ => throw new NotImplementedException()
                };
                db.Items.Add(creator.CreateItem());
                db.SaveChanges();

                string description = $"{LoggedInUser.FirstName} Added New Item";
                AddLog(LoggedInUser.Id, description, ActionTypes.AddItem);
                int id = (from i in db.Items
                          orderby i.Id descending
                          select i.Id).First();
                AddStock(id, 0);
                return true;
            }
            catch
            {
                string description = "Add Item Failed";
                AddLog(NoUserId, description, ActionTypes.AddItemFailed);
                return false;
            }
        }
        public bool AddNewUser(string firstName, string lastName, UserTypes userType, string email, string password)
        {
            try
            {
                db.Users.Add(new(firstName, lastName, userType, email, password));
                db.SaveChanges();
                int id = db.Users.Skip(db.Users.Count() - 1).First().Id;
                string description = $"{firstName} Registered Succesfully";
                AddLog(id, description, ActionTypes.Register);
            }
            catch
            {
                string description = "Registeration Failed";
                AddLog(NoUserId, description, ActionTypes.RegisterFailed);
                return false;
            }
            return true;
        }
        public bool AddCustomer(string firstName, string lastName, string email, DateTime dob)
        {
            db.customers.Add(new()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                DateOfBirth = dob,
                AddedAt = DateTime.Now,
                TotalPurchases = 0,
                PurchasesCount = 0,
                Discount = 0,
                LastPurchase = null
            });
            db.SaveChanges();
            return true;
        }

        // Strart/End Program Handlers
        public void OnExitProgram() // should be called when Window Closes
        {
            int userId = LoggedInUser != null ? LoggedInUser.Id : NoUserId;

            if (userId != NoUserId)
            {
                if (!LoggedInUser.RememberMe)
                {
                    new Delete().ClearLoggedIns();
                    AddLog(LoggedInUser.Id, $"{LoggedInUser.Id} logged off", ActionTypes.Logoff);
                }
            }

            AddLog(userId, $"id:{userId} exit program", ActionTypes.Exit);
        }
        public void OnStartProgram() // should be called when Window Start
        {
            Read reader = new();
            int userId = NoUserId;

            // firstly, checks if last session crushed.
            if (!reader.CheckLastSessionExit())
            {
                AddLog(NoUserId, $"crush was found, previous session did not exit", ActionTypes.Crush);
            }
            // if no user logged in, system exit to desktop:
            if (LoggedInUser == null && LoggedUserEmail == string.Empty)
            {
                OnExitProgram();
            }

            // if LoggedIns entity is empty, we get the id by the LoggedUserEmail:
            if (!db.LoggedIns.Any())
            {
                userId = db.Users.Single(x => x.Email == LoggedUserEmail).Id;
                AddLoggedIn(userId);
                LoggedInUser = (from user in db.LoggedIns
                                orderby user.Id
                                select user.User).Last();
                if (IsRememberMe)
                {
                    LoggedInUser.RememberMe = true;
                }
                AddLog(LoggedInUser.Id, $"id: {LoggedInUser.Id} email:{LoggedUserEmail} logged in", ActionTypes.Login);
                AddLog(LoggedInUser.Id, $"id:{LoggedInUser.Id} start program", ActionTypes.StartUp);
                return;
            }
            // else we get the id & email from the LoggedIns entity:
            else
            {
                LoggedUserEmail = (from user in db.LoggedIns
                                   orderby user.Id
                                   select user.User.Email).Last();

                userId = db.Users.Single(x => x.Email == LoggedUserEmail).Id;

                LoggedInUser = (from user in db.LoggedIns
                                orderby user.Id
                                select user.User).Last();

                if (LoggedInUser.RememberMe)
                {
                    IsRememberMe = true;
                }
                new Delete().ClearLoggedIns();
                AddLoggedIn(userId);
                AddLog(LoggedInUser.Id, $"id: {LoggedInUser.Id} email:{LoggedUserEmail} logged in", ActionTypes.Login);
                AddLog(LoggedInUser.Id, $"id:{LoggedInUser.Id} start program", ActionTypes.StartUp);
                return;
            }
        }
    }
}

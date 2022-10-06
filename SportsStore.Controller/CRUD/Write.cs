using SportsStore.Controller.CRUD;
using SportsStore.Enums;
using SportsStore.Model;
using SportsStore.Model.Costumers;
using SportsStore.Model.Items;
using SportsStore.Model.Users;
using SportStore.Controller.DbConnector;

namespace SportsStore.Controller
{
    public class Write :IDbConnectable
    {
        // Db Connector Access
        private readonly StoreContext db;

        // Properties
        public static User? LoggedInUser { get; set; }
        private static string LoggedUserEmail { get; set; } = string.Empty;
        public static bool IsRememberMe { get; set; } = false;
        public int NoUserId { get; set; }

        // Constructor
        public Write()
        {
            db = DbConnector.GetInstance(this).Db;
            if (!(db.Users).Any())
            {
                db.Users.Add(new User("NO_USER", "NO_USER", (UserTypes)500, "NO_USER", "1"));
                db.Users.Add(new User("Admin", "Admin", (UserTypes)0, "Admin", "Admin1234"));
                db.SaveChanges();
            }
            NoUserId = db.Users.Single(x => x.UserType == UserTypes.No_User).Id;
        }

        // Public Static Method for Changing Current Logged-In User Email
        public static void ChangeLoggedUserEmail(string email)
        {
            LoggedUserEmail = email;
        }

        // Public Methods for editing selected user details
        public void ChangeUserType(int id, string type)
        {
            db.Users.Single(user => user.Id == id).UserType = (UserTypes)Enum.Parse(typeof(UserTypes), type);
            db.SaveChanges();
        }
        public void ChangeUserEmail(int id, string email)
        {
            db.Users.Single(user => user.Id == id).Email = email;
            db.SaveChanges();
        }
        public void ChangeUserHireDate(int id, string date)
        {
            db.Users.Single(user => user.Id == id).HireDate = Convert.ToDateTime(date).Date;
            db.SaveChanges();
        }
        public void ChangeUserPassword(int id, string password)
        {
            db.Users.Single(user => user.Id == id).Password = password;
            db.SaveChanges();
        }
        public void RemoveUser(int id)
        {
            db.Users.Remove(db.Users.Single(user => user.Id == id));
            db.SaveChanges();
        }

        // Public Methods for editing selected customer details

        public void ChangeCustomerFirstName(int id, string fName)
        {
            db.customers.Single(customer => customer.Id == id).FirstName = fName;
            db.SaveChanges();
        }
        public void ChangeCustomerLastName(int id, string lName)
        {
            db.customers.Single(customer => customer.Id == id).LastName = lName;
            db.SaveChanges();
        }
        public void ChangeCustomerEmail(int id, string email)
        {
            db.customers.Single(customer => customer.Id == id).Email = email;
            db.SaveChanges();
        }
        public void ChangeCustomerDOB(int id, string dob)
        {
            db.customers.Single(customer => customer.Id == id).DateOfBirth = Convert.ToDateTime(dob);
            db.SaveChanges();
        }

        // Add to log Methods
        public void AddLoggedIn(int id)
        {
            db.LoggedIns.Add(new() { DateTime = DateTime.Now, User = db.Users.Single(x => x.Id == id) });
            db.SaveChanges();
            LoggedUserEmail = db.Users.Single(x => x.Id == id).Email;
            AddLog(id, $"id: {id} email:{LoggedUserEmail} logged in", ActionTypes.Login);
        }
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
        public bool EditStock(int itemId, string name = "", string price = "", string quantity = "",
                              string itemType = "", string innerItemType = "", string color = "", string size = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    db.Stocks.Where(x => x.Item.Id == Convert.ToInt16(itemId)).Select(x => x.Item).ToList()[0].Name = name;
                }
                if (!string.IsNullOrEmpty(price))
                {
                    db.Stocks.Where(x => x.Item.Id == Convert.ToInt16(itemId)).Select(x => x.Item).ToList()[0].Price = Convert.ToInt16(price);
                }
                if (!string.IsNullOrEmpty(quantity))
                {
                    db.Stocks.Where(x => x.Item.Id == Convert.ToInt16(itemId)).Select(x => x).ToList()[0].Quantity = Convert.ToInt16(quantity);
                }
                if (!string.IsNullOrEmpty(itemType))
                {
                    db.Stocks.Where(x => x.Item.Id == Convert.ToInt16(itemId)).Select(x => x.Item).ToList()[0].ItemType = itemType;
                }
                if (!string.IsNullOrEmpty(innerItemType))
                {
                    db.Stocks.Where(x => x.Item.Id == Convert.ToInt16(itemId)).Select(x => x.Item).ToList()[0].ItemInnerType = innerItemType;
                }
                if (!string.IsNullOrEmpty(color))
                {
                    db.Stocks.Where(x => x.Item.Id == Convert.ToInt16(itemId)).Select(x => x.Item).ToList()[0].Color = color;
                }
                if (!string.IsNullOrEmpty(size))
                {
                    db.Stocks.Where(x => x.Item.Id == Convert.ToInt16(itemId)).Select(x => x.Item).ToList()[0].Size = size;
                }
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        // Strart/End Handlers
        public void ExitProgram() // Called when Window Closes
        {
            int userId = LoggedInUser != null ? LoggedInUser.Id : NoUserId;

            if (userId != NoUserId)
            {
                if (!LoggedInUser.RememberMe)
                {
                    db.LoggedIns.Clear(db);
                    AddLog(LoggedInUser.Id, $"{LoggedInUser.Id} logged off", ActionTypes.Logoff);
                }
            }

            AddLog(userId, $"id:{userId} exit program", ActionTypes.Exit);
        }
        public void OnStartProgram() // Called when Window Start
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
                ExitProgram();
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
                db.LoggedIns.Clear(db);
                AddLoggedIn(userId);
                AddLog(LoggedInUser.Id, $"id:{LoggedInUser.Id} start program", ActionTypes.StartUp);
                return;
            }
        }
    }
}

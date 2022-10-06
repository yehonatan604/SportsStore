using Microsoft.EntityFrameworkCore;
using SportsStore.Enums;
using SportsStore.Model;
using SportsStore.Model.Costumers;
using SportsStore.Model.Items;
using SportStore.Controller.DbConnector;
using System.Reflection.Metadata.Ecma335;

namespace SportsStore.Controller
{
    public class Read : IDbConnectable
    {
        // Db Connector Access
        private readonly StoreContext db;

        // Constructor
        public Read()
        {
            db = DbConnector.GetInstance(this).Db;
        }

        // Checks
        public bool CheckLastSessionExit()
        {
            ActionTypes actionType = (from action in db.Logs
                                      orderby action.Id ascending
                                      select action.ActionType).Last();

            return actionType == ActionTypes.Exit;
        }
        public bool CheckAvailability(string email)
        {
            var record = from user in db.Users
                         where user.Email == email
                         select user;

            return !record.Any();
        }
        public bool CheckLogin(string email, string password)
        {
            var record = from user in db.Users
                         where user.Email == email && user.Password == password
                         select user.Id;

            ActionTypes actionType = record.Any() ? ActionTypes.Login : ActionTypes.LoginFailed;
            string text = record.Any() ? $"{email} Logged in succesfully" : "Login failed";
            return record.Any();
        }
        public int CheckAuthorizationLevel()
        {
            return (int)Write.LoggedInUser.UserType;
        }

        // GetCustomers Overloads - an overload for each case of given arguments
        public IEnumerable<object> GetCustomers(string arg1, string arg2, string arg3, string arg4, string arg5, string arg6) =>
            from customer in db.customers
            where
             (
                 DateTime.Now.Year - customer.DateOfBirth.Year > Convert.ToInt16(arg1) &&
                 DateTime.Now.Year - customer.DateOfBirth.Year < Convert.ToInt16(arg2) &&
                 customer.TotalPurchases > Convert.ToInt16(arg3) &&
                 customer.TotalPurchases < Convert.ToInt32(arg4) &&
                 customer.PurchasesCount > Convert.ToInt16(arg5) &&
                 customer.PurchasesCount < Convert.ToInt32(arg6)
             )
            select customer;
        public IEnumerable<object> GetCustomers(string s, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7)
        {
            return s switch
            {
                "ById" => from customer in db.customers
                          where
                           (
                               customer.Id == Convert.ToInt16(arg7) &&
                               DateTime.Now.Year - customer.DateOfBirth.Year > Convert.ToInt16(arg1) &&
                               DateTime.Now.Year - customer.DateOfBirth.Year < Convert.ToInt16(arg2) &&
                               customer.TotalPurchases > Convert.ToInt16(arg3) &&
                               customer.TotalPurchases < Convert.ToInt32(arg4) &&
                               customer.PurchasesCount > Convert.ToInt16(arg5) &&
                               customer.PurchasesCount < Convert.ToInt32(arg6)
                           )
                          select customer,
                "ByDate" => (from customer in db.customers
                             join sale in db.Sales
                             on customer.Id equals sale.Customer.Id
                             where
                              (
                                  sale.SaleDate.Date.ToString() == arg7 &&
                                  DateTime.Now.Year - customer.DateOfBirth.Year > Convert.ToInt16(arg1) &&
                                  DateTime.Now.Year - customer.DateOfBirth.Year < Convert.ToInt16(arg2) &&
                                  customer.TotalPurchases > Convert.ToInt16(arg3) &&
                                  customer.TotalPurchases < Convert.ToInt32(arg4) &&
                                  customer.PurchasesCount > Convert.ToInt16(arg5) &&
                                  customer.PurchasesCount < Convert.ToInt32(arg6)
                              )
                             select customer).Distinct(),
            };
        }
        public IEnumerable<object> GetCustomers(string s, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8) =>
                  from customer in db.customers
                  join sale in db.Sales
                  on customer.Id equals sale.Customer.Id
                  where
                                (
                                    customer.Id == Convert.ToInt16(arg7) &&
                                    sale.SaleDate.Date.ToString() == arg8 &&
                                    DateTime.Now.Year - customer.DateOfBirth.Year > Convert.ToInt16(arg1) &&
                                    DateTime.Now.Year - customer.DateOfBirth.Year < Convert.ToInt16(arg2) &&
                                    customer.TotalPurchases > Convert.ToInt16(arg3) &&
                                    customer.TotalPurchases < Convert.ToInt32(arg4) &&
                                    customer.PurchasesCount > Convert.ToInt16(arg5) &&
                                    customer.PurchasesCount < Convert.ToInt32(arg6)
                                )
                  select customer;

        // GetSales Overloads - an overload for each case of given arguments
        public IEnumerable<object> GetSales(string arg1, string arg2) =>
             from sale in db.SaleViews
             where
              (
                  sale.ItemPrice > Convert.ToInt16(arg1) &&
                  sale.ItemPrice < Convert.ToInt32(arg2)
              )
             select sale;
        public IEnumerable<object> GetSales(string s, string arg1, string arg2, string arg3)
        {
            switch (s)
            {
                case "ByItemId":
                    {
                        return from sale in db.SaleViews
                               where
                                (
                                    sale.ThisItemId == Convert.ToInt16(arg1) &&
                                    sale.ItemPrice > Convert.ToInt16(arg2) &&
                                    sale.ItemPrice < Convert.ToInt32(arg3)
                                )
                               select sale;
                    }
                case "ByType":
                    {
                        return from sale in db.SaleViews
                               where
                                (
                                    sale.ItemType == arg1 &&
                                    sale.ItemPrice > Convert.ToInt16(arg2) &&
                                    sale.ItemPrice < Convert.ToInt32(arg3)
                                )
                               select sale;
                    }
                case "BySalseMan":
                    {
                        return from sale in db.SaleViews
                               where
                                (
                                    sale.SalsemanId == Convert.ToInt16(arg1) &&
                                    sale.ItemPrice > Convert.ToInt16(arg2) &&
                                    sale.ItemPrice < Convert.ToInt32(arg3)
                                )
                               select sale;
                    }
                case "ByDate":
                    {
                        return from sale in db.SaleViews
                               where
                                (
                                    sale.SaleDate.Date.ToString() == arg1 &&
                                    sale.ItemPrice > Convert.ToInt16(arg2) &&
                                    sale.ItemPrice < Convert.ToInt32(arg3)
                                )
                               select sale;
                    }
                default:
                    {
                        return GetTable("Sales");
                    }
            };

        }
        public IEnumerable<object> GetSales(string s, string arg1, string arg2, string arg3, string arg4)
        {
            switch (s)
            {
                case "ByTypeInnerType":
                    {
                        return from sale in db.Sales
                               where
                                (
                                    sale.Item.ItemType == arg1 &&
                                    sale.Item.ItemInnerType == arg2 &&
                                    sale.Item.Price > Convert.ToInt16(arg3) &&
                                    sale.Item.Price < Convert.ToInt32(arg4)
                                )
                               select sale;
                    }
                default:
                    {
                        return GetTable("Sales");
                    }
            };

        }
        public IEnumerable<object> GetSales(string s, string arg1, string arg2, string arg3, string arg4, string arg5)
        {
            switch (s)
            {
                case "ByTypeInnerTypeDate":
                    {
                        return from sale in db.SaleViews
                               where
                                (
                                    sale.ItemType == arg1 &&
                                    sale.ItemInnerType == arg2 &&
                                    sale.SaleDate.Date.ToString() == arg3 &&
                                    sale.ItemPrice > Convert.ToInt16(arg4) &&
                                    sale.ItemPrice < Convert.ToInt32(arg5)
                                )
                               select sale;
                    }
                case "ByTypeInnerTypeSalesman":
                    {
                        return from sale in db.SaleViews
                               where
                                (
                                    sale.ItemType == arg1 &&
                                    sale.ItemInnerType == arg2 &&
                                    sale.SalsemanId == Convert.ToInt16(arg3) &&
                                    sale.ItemPrice > Convert.ToInt16(arg4) &&
                                    sale.ItemPrice < Convert.ToInt32(arg5)
                                )
                               select sale;
                    }
                case "ByTypeInnerTypeColor":
                    {
                        return from sale in db.SaleViews
                               where
                                (
                                    sale.ItemType == arg1 &&
                                    sale.ItemInnerType == arg2 &&
                                    sale.ItemColor == arg3 &&
                                    sale.ItemPrice > Convert.ToInt16(arg4) &&
                                    sale.ItemPrice < Convert.ToInt32(arg5)
                                )
                               select sale;
                    }
                case "ByTypeInnerTypeSize":
                    {
                        return from sale in db.SaleViews
                               where
                                (
                                    sale.ItemType == arg1 &&
                                    sale.ItemInnerType == arg2 &&
                                    sale.ItemSize == arg3 &&
                                    sale.ItemPrice > Convert.ToInt16(arg4) &&
                                    sale.ItemPrice < Convert.ToInt32(arg5)
                                )
                               select sale;
                    }
                default:
                    {
                        return GetTable("Sales");
                    }
            };

        }
        public IEnumerable<object> GetSales(string s, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            switch (s)
            {
                case "ByTypeInnerTypeColorSize":
                    {
                        return from sale in db.SaleViews
                               where
                                (
                                    sale.ItemType == arg1 &&
                                    sale.ItemInnerType == arg2 &&
                                    sale.ItemColor == arg3 &&
                                    sale.ItemSize == arg4 &&
                                    sale.ItemPrice > Convert.ToInt16(arg5) &&
                                    sale.ItemPrice < Convert.ToInt32(arg6)
                                )
                               select sale;
                    }
                default:
                    {
                        return GetTable("Sales");
                    }
            };

        }
        public IEnumerable<object> GetSales(string s, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7)
        {
            switch (s)
            {
                case "ByTypeInnerTypeColorSizeSalesman":
                    {
                        return from sale in db.SaleViews
                               where
                                (
                                    sale.ItemType == arg1 &&
                                    sale.ItemInnerType == arg2 &&
                                    sale.ItemColor == arg3 &&
                                    sale.ItemSize == arg4 &&
                                    sale.SalsemanId == Convert.ToInt16(arg5) &&
                                    sale.ItemPrice > Convert.ToInt16(arg6) &&
                                    sale.ItemPrice < Convert.ToInt32(arg7)
                                )
                               select sale;
                    }
                case "ByTypeInnerTypeColorSizeDate":
                    {
                        return from sale in db.SaleViews
                               where
                                (
                                    sale.ItemType == arg1 &&
                                    sale.ItemInnerType == arg2 &&
                                    sale.ItemColor == arg3 &&
                                    sale.ItemSize == arg4 &&
                                    sale.SaleDate.Date.ToString() == arg5 &&
                                    sale.ItemPrice > Convert.ToInt16(arg6) &&
                                    sale.ItemPrice < Convert.ToInt32(arg7)
                                )
                               select sale;
                    }
                default:
                    {
                        return GetTable("Sales");
                    }
            };

        }

        // GetUser Overloads - an overload for each case of given arguments

        public IEnumerable<object> GetUsers(string s, string arg1) =>
            s switch
            {
                "ByType" => from user in db.Users
                            where user.UserType == (UserTypes)Convert.ToInt16(arg1)
                            select user,

                "BySaleDate" => from user in db.Users
                                join sale in db.Sales
                                on user.Id equals sale.User.Id
                                where sale.SaleDate.Date.ToString() == arg1
                                select user,

                "ByHireYear" => from user in db.Users
                                where user.HireDate.Year.ToString() == arg1
                                select user,

                _ => throw new NotImplementedException()
            };
        public IEnumerable<object> GetUsers(string s, string arg1, string arg2) =>
            s switch
            {
                "ByTypeSaleDate" => from user in db.Users
                                    join sale in db.Sales
                                    on user.Id equals sale.User.Id
                                    where user.UserType == (UserTypes)Convert.ToInt16(arg1) && sale.SaleDate.Date.ToString() == arg2
                                    select user,

                "ByTypeHireYear" => from user in db.Users
                                    where user.UserType == (UserTypes)Convert.ToInt16(arg1) && user.HireDate.Year.ToString() == arg2
                                    select user,

                "BySaleDateHireYear" => from user in db.Users
                                        join sale in db.Sales
                                    on user.Id equals sale.User.Id
                                        where sale.SaleDate.Date.ToString() == arg1 && user.HireDate.Year.ToString() == arg1
                                        select user,

                _ => throw new NotImplementedException()
            };
        public IEnumerable<object> GetUsers(string s, string arg1, string arg2, string arg3) =>
            s switch
            {
                "ByAll" => (from user in db.Users
                           join sale in db.Sales
                            on user.Id equals sale.User.Id
                           where 
                            user.UserType == (UserTypes)Convert.ToInt16(arg1) && 
                            sale.SaleDate.Date.ToString() == arg2 && 
                            user.HireDate.Year.ToString() == arg3
                           select user).Distinct(),

                _ => throw new NotImplementedException()
            };

        // GetTable Overloads - an overload for each case of given arguments
        public IEnumerable<object> GetTable(string s = "*")
        {
            switch (s)
            {
                case "Sales":
                    {
                        return db.SaleViews;
                    }
                case "Customers":
                    {
                        return db.customers;
                    }
                case "Users":
                    {
                        return db.Users;
                    }
                case "Logs":
                    {
                        return from log in db.Logs
                               orderby log.Id ascending
                               select new
                               {
                                   log.Id,
                                   UserId = log.User.Id,
                                   log.User.FirstName,
                                   log.User.LastName,
                                   ActionType = log.ActionType.ToString(),
                                   log.DateTime
                               };
                    }
                default: // Stock View is default
                    {
                        return db.Stocks;
                    }
            };
        }
        public IEnumerable<object> GetTable(string s, string arg1)
        {
            switch (s)
            {
                case "ViewsByType":
                    {
                        return from stock in db.Stocks
                               join item in db.Items
                               on stock.Item.Id equals item.Id
                               where item.ItemType == arg1
                               select stock;
                    }

                case "ViewsByColor":
                    {
                        return from stock in db.Stocks
                               join item in db.Items
                               on stock.Item.Id equals item.Id
                               where item.Color == arg1
                               select stock;
                    }
                case "ViewsBySize":
                    {
                        return from stock in db.Stocks
                               join item in db.Items
                               on stock.Item.Id equals item.Id
                               where item.Size == arg1
                               select stock;
                    }
                case "LogsById":
                    {
                        return from log in db.Logs
                               orderby log.Id ascending
                               where log.User.Id == Convert.ToInt16(arg1)
                               select new
                               {
                                   log.Id,
                                   UserId = log.User.Id,
                                   log.User.FirstName,
                                   log.User.LastName,
                                   ActionType = log.ActionType.ToString(),
                                   log.DateTime
                               };
                    }
                case "LogsByDate":
                    {
                        return from log in db.Logs
                               orderby log.Id ascending
                               where log.DateTime.ToString() == arg1
                               select new
                               {
                                   log.Id,
                                   UserId = log.User.Id,
                                   log.User.FirstName,
                                   log.User.LastName,
                                   ActionType = log.ActionType.ToString(),
                                   log.DateTime
                               };
                    }
                case "LogsByAction":
                    {
                        return from log in db.Logs
                               orderby log.Id ascending
                               where log.ActionType == (ActionTypes)Enum.Parse(typeof(ActionTypes), arg1)
                               select new
                               {
                                   log.Id,
                                   UserId = log.User.Id,
                                   log.User.FirstName,
                                   log.User.LastName,
                                   ActionType = log.ActionType.ToString(),
                                   log.DateTime
                               };
                    }
                default:
                    {
                        return GetTable();
                    }
            };
        }
        public IEnumerable<object> GetTable(string s, string arg1, string arg2)
        {
            switch (s)
            {
                case "ViewsByPrice":
                    {
                        return from stock in db.Stocks
                               join item in db.Items
                               on stock.Item.Id equals item.Id
                               where item.Price > Convert.ToInt16(arg1) && item.Price < Convert.ToInt32(arg2)
                               select stock;
                    }
                case "ViewsByInnerType":
                    {
                        return from stock in db.Stocks
                               join item in db.Items
                               on stock.Item.Id equals item.Id
                               where item.ItemType == arg1 && item.ItemInnerType == arg2
                               select stock;
                    }
                case "ByTypeInnerType":
                    {
                        return from sale in db.Sales
                               where sale.Item.ItemType == arg1 && sale.Item.ItemInnerType == arg2
                               select sale;
                    }

                case "ByTPrice":
                    {
                        return from sale in db.Sales
                               where sale.TotalPrice > Convert.ToInt32(arg1) && sale.TotalPrice < Convert.ToInt32(arg2)
                               select sale;
                    }
                case "LogsByActionDate":
                    {
                        return from log in db.Logs
                               where log.ActionType == (ActionTypes)Enum.Parse(typeof(ActionTypes), arg1) && log.DateTime.Date.ToString() == arg2
                               select new
                               {
                                   log.Id,
                                   UserId = log.User.Id,
                                   log.User.FirstName,
                                   log.User.LastName,
                                   ActionType = log.ActionType.ToString(),
                                   log.DateTime
                               };
                    }
                case "LogsByActionId":
                    {
                        return from log in db.Logs
                               orderby log.Id ascending
                               where log.User.Id == Convert.ToInt16(arg1) && log.ActionType == (ActionTypes)Enum.Parse(typeof(ActionTypes), arg2)
                               select new
                               {
                                   log.Id,
                                   UserId = log.User.Id,
                                   log.User.FirstName,
                                   log.User.LastName,
                                   ActionType = log.ActionType.ToString(),
                                   log.DateTime
                               };
                    }
                case "LogsByUserIdDate":
                    {
                        return from log in db.Logs
                               orderby log.Id ascending
                               where log.DateTime.Date.ToString() == arg1 && log.User.Id == Convert.ToInt16(arg2)
                               select new
                               {
                                   log.Id,
                                   UserId = log.User.Id,
                                   log.User.FirstName,
                                   log.User.LastName,
                                   ActionType = log.ActionType.ToString(),
                                   log.DateTime
                               };
                    }
                default:
                    {
                        return GetTable();
                    }
            };
        }
        public IEnumerable<object> GetTable(string s, string arg1, string arg2, string arg3)
        {
            switch (s)
            {
                case "ViewsByItemPrice":
                    {
                        return from stock in db.Stocks
                               join item in db.Items
                               on stock.Item.Id equals item.Id
                               where item.ItemType == arg1 &&
                                     item.Price > Convert.ToInt16(arg2) &&
                                     item.Price < Convert.ToInt32(arg3)
                               select stock;
                    }
                case "ViewsByColor":
                    {
                        return from stock in db.Stocks
                               join item in db.Items
                               on stock.Item.Id equals item.Id
                               where item.ItemType == arg1 && item.ItemInnerType == arg2 && item.Color == arg3
                               select stock;
                    }
                case "ViewsBySize":
                    {
                        return from stock in db.Stocks
                               join item in db.Items
                               on stock.Item.Id equals item.Id
                               where item.ItemType == arg1 && item.ItemInnerType == arg2 && item.Size == arg3
                               select stock;
                    }
                case "LogsByAll":
                    {
                        return from log in db.Logs
                               orderby log.Id ascending
                               where log.DateTime.Date.ToString() == arg1 && log.User.Id == Convert.ToInt16(arg2) && log.ActionType == (ActionTypes)Enum.Parse(typeof(ActionTypes), arg3)
                               select new
                               {
                                   log.Id,
                                   UserId = log.User.Id,
                                   log.User.FirstName,
                                   log.User.LastName,
                                   ActionType = log.ActionType.ToString(),
                                   log.DateTime
                               };
                    }
                default:
                    {
                        return GetTable();
                    }
            };
        }
        public IEnumerable<object> GetTable(string s, string arg1, string arg2, string arg3, string arg4)
        {
            switch (s)
            {
                case "ViewsByInnerItemPrice":
                    {
                        return from stock in db.Stocks
                               join item in db.Items
                               on stock.Item.Id equals item.Id
                               where item.ItemType == arg1 &&
                                     item.ItemInnerType == arg2 &&
                                     item.Price > Convert.ToInt16(arg3) &&
                                     item.Price < Convert.ToInt32(arg4)
                               select stock;
                    }

                case "ViewsByAllNoPrice":
                    {
                        return from stock in db.Stocks
                               join item in db.Items
                               on stock.Item.Id equals item.Id
                               where item.ItemType == arg1 && item.ItemInnerType == arg2 && item.Color == arg3 && item.Size == arg4
                               select stock;
                    }
                default:
                    {
                        return GetTable();
                    }
            }
        }
        public IEnumerable<object> GetTable(string s, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            switch (s)
            {
                case "SalesByAll":
                    {
                        return from sale in db.Sales
                               join item in db.Items
                               on sale.Item.Id equals item.Id
                               where sale.Item.ItemType == arg1 && item.ItemInnerType == arg2 && item.Color == arg3 && item.Size == arg4 &&
                                     item.Price > Convert.ToInt16(arg5) &&
                                     item.Price < Convert.ToInt32(arg6)
                               select sale;
                    }
                case "ViewsByAll":
                    {
                        return from stock in db.Stocks
                               join item in db.Items
                               on stock.Item.Id equals item.Id
                               where item.ItemType == arg1 &&
                                     item.ItemInnerType == arg2 &&
                                     item.Color == arg3 &&
                                     item.Size == arg4 &&
                                     item.Price > Convert.ToInt16(arg5) &&
                                     item.Price < Convert.ToInt32(arg6)
                               select stock;
                    }
                default:
                    {
                        return GetTable();
                    }
            }
        }

        // Search Methods
        public IEnumerable<object> ItemSearch(string str)
        {
            return from items in db.Items
                   where (items.Name).Contains(str)
                   select items;
        }
        public IEnumerable<object> CustomerSearch(string str)
        {
            return from customer in db.customers
                   where (customer.FirstName).Contains(str) || (customer.LastName).Contains(str)
                   select customer;
        }
        public IEnumerable<object> UsersSearchName(string str)
        {
            return from user in db.Users
                   where (user.FirstName).Contains(str) || (user.LastName).Contains(str)
                   select user;
        }
        public IEnumerable<object> UsersSearchId(string str)
        {
            return from user in db.Users
                   where (user.Id.ToString()).Contains(str)
                   select user;
        }

        // Return UserType as string 
        public string GetUserType(int id)
        {
            return db.Users.Single(user => user.Id == id).UserType.ToString();
        }

        // Return List Overloads for each case of given arguments
        public List<string> GetList(string s)
        {
            return s switch
            {
                "ByItem" => (from sale in db.Sales
                             select sale.Item.ItemType.ToString()).Distinct().ToList(),

                "BySalesMan" => (from sale in db.Sales
                                 select sale.User.Id.ToString()).Distinct().ToList(),

                "ByDate" => (from sale in db.Sales
                             select sale.SaleDate.Date.ToString()).Distinct().ToList(),

                "ByLogUserId" => (from log in db.Logs
                                  select log.User.Id.ToString()).Distinct().ToList(),

                "ByLogAction" => (from log in db.Logs
                                  select log.ActionType.ToString()).Distinct().ToList(),

                "ByLogDate" => (from log in db.Logs
                                select log.DateTime.Date.ToString()).Distinct().ToList(),

                "ByCostumer" => (from costumer in db.customers
                                 select costumer.Id.ToString()).Distinct().ToList(),

                "ByHireDate" => (from user in db.Users
                                 select user.HireDate.Year.ToString()).Distinct().ToList(),
                "Users" => (from user in db.Users
                            select user.Id.ToString()).ToList(),

                _ =>
                         new List<string>()
            };
        }
        public List<string> GetList(string s, string arg1)
        {
            return s switch
            {
                "CustomerDetails" => new List<string>()
                {
                    db.customers.Single(x => x.Id.ToString() == arg1).FirstName,
                    db.customers.Single(x => x.Id.ToString() == arg1).LastName,
                    db.customers.Single(x => x.Id.ToString() == arg1).Email,
                    db.customers.Single(x => x.Id.ToString() == arg1).DateOfBirth.Date.ToString()
                },

                _ => new List<string>()
            };
        }
        public List<string> GetList(string s, string arg1, string arg2)
        {
            return s switch
            {
                "SalesByDate" => (from sale in db.Sales
                                  where sale.Customer.Id == Convert.ToInt16(arg1) && sale.SaleDate.Date.ToString() == arg2
                                  select $"{sale.Id}").Distinct().ToList(),

                "SalesInfo" => new List<string>()
                {
                    db.customers.Single(x => x.Id == Convert.ToInt16(arg1)).FirstName,
                    db.customers.Single(x => x.Id == Convert.ToInt16(arg1)).LastName,
                    db.Sales.Where(x => x.Id == Convert.ToInt16(arg2)).Select(x => x.Item.Name).ToList()[0],
                    db.Sales.Single(x => x.Id == Convert.ToInt16(arg2)).Quantity.ToString(),
                    db.Sales.Single(x => x.Id == Convert.ToInt16(arg2)).TotalPrice.ToString(),
                    db.Sales.Single(x => x.Id == Convert.ToInt16(arg2)).SaleDate.ToString(),
                },

                _ => new List<string>()
            };

        }

        // Get Stats Methods
        public List<string> GetSalesStats()
        {
            string bestItem = (from sale in db.Sales
                               orderby sale.Item.Name ascending
                               select sale.Item.Name).First();

            string bestItemType = (from sale in db.Sales
                                   orderby sale.Item.ItemType ascending
                                   select sale.Item.ItemType).First();

            string bestItemInnerType = (from sale in db.Sales
                                        orderby sale.Item.ItemInnerType ascending
                                        select sale.Item.ItemInnerType).First();

            string bestItemColor = (from sale in db.Sales
                                    orderby sale.Item.Color ascending
                                    select sale.Item.Color).First();

            string bestItemSize = (from sale in db.Sales
                                   orderby sale.Item.Size ascending
                                   select sale.Item.Size).First();

            string bestSalesman = (from sale in db.Sales
                                   orderby sale.User.Id ascending
                                   select $"{sale.User.FirstName} {sale.User.LastName}").First();

            string bestSaleDate = (from sale in db.Sales
                                   orderby sale.SaleDate ascending
                                   select sale.SaleDate.Date.ToString()).First();

            return new List<string>() { bestItem, bestItemType, bestItemInnerType,
                                        bestItemColor, bestItemSize, bestSalesman, bestSaleDate };
        }
        public List<string> GetCustomerStats()
        {
            string mostSpent = (from customer in db.customers
                                orderby customer.TotalPurchases descending
                                select $"{customer.FirstName} {customer.LastName}").First();

            string mostReturning = (from customer in db.customers
                                    orderby customer.PurchasesCount descending
                                    select $"{customer.FirstName} {customer.LastName}").First();

            string mostVeteran = (from customer in db.customers
                                  orderby customer.AddedAt ascending
                                  select $"{customer.FirstName} {customer.LastName}").First();

            string mostNew = (from customer in db.customers
                              orderby customer.AddedAt descending
                              select $"{customer.FirstName} {customer.LastName}").First();

            return new List<string>() { mostSpent, mostReturning, mostVeteran, mostNew };
        }
    }
}

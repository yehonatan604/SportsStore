using Microsoft.EntityFrameworkCore;
using SportsStore.Enums;
using SportsStore.Model;
using SportsStore.Model.Costumers;
using SportsStore.Model.Items;
using SportStore.Controller.DbConnector;
using System.Reflection.Metadata.Ecma335;

namespace SportsStore.Controller
{
    public class Read
    {
        // Db Connector Access
        private readonly StoreContext db;

        // Constructor
        public Read()
        {
            db = DbConnector.GetInstance().GetDb();
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
        public static int CheckAuthorizationLevel()
        {
            return (int)(Write.LoggedInUser.UserType);
        }

        // GetSales Overloads - an overload for each case of given arguments
        public IEnumerable<object> GetSales(string arg1, string arg2) =>
             from sale in db.Sales
             where
              (
                  sale.Item.Price > Convert.ToInt16(arg1) &&
                  sale.Item.Price < Convert.ToInt32(arg2)
              )
             select new
             {
                 ItemID = sale.Item.Id,
                 ItemName = sale.Item.Name,
                 sale.Item.ItemType,
                 sale.Item.Price,
                 sale.Quantity,
                 sale.TotalPrice,
                 salesManId = sale.User.Id,
                 salesManFName = sale.User.FirstName,
                 salesManLName = sale.User.LastName,
                 sale.SaleDate
             };
        public IEnumerable<object> GetSales(string s, string arg1, string arg2, string arg3)
        {
            switch (s)
            {
                case "ByItemId":
                    {
                        return from sale in db.Sales
                               where
                                (
                                    sale.Item.Id == Convert.ToInt16(arg1) &&
                                    sale.Item.Price > Convert.ToInt16(arg2) &&
                                    sale.Item.Price < Convert.ToInt32(arg3)
                                )
                               select new
                               {
                                   ItemID = sale.Item.Id,
                                   ItemName = sale.Item.Name,
                                   sale.Item.ItemType,
                                   sale.Item.Price,
                                   sale.Quantity,
                                   sale.TotalPrice,
                                   salesManId = sale.User.Id,
                                   salesManFName = sale.User.FirstName,
                                   salesManLName = sale.User.LastName,
                                   sale.SaleDate
                               };
                    }
                case "ByType":
                    {
                        return from sale in db.Sales
                               where
                                (
                                    sale.Item.ItemType == arg1 &&
                                    sale.Item.Price > Convert.ToInt16(arg2) &&
                                    sale.Item.Price < Convert.ToInt32(arg3)
                                )
                               select new
                               {
                                   ItemID = sale.Item.Id,
                                   ItemName = sale.Item.Name,
                                   sale.Item.ItemType,
                                   sale.Item.Price,
                                   sale.Quantity,
                                   sale.TotalPrice,
                                   salesManId = sale.User.Id,
                                   salesManFName = sale.User.FirstName,
                                   salesManLName = sale.User.LastName,
                                   sale.SaleDate
                               };
                    }
                case "BySalseMan":
                    {
                        return from sale in db.Sales
                               where
                                (
                                    sale.User.Id == Convert.ToInt16(arg1) &&
                                    sale.Item.Price > Convert.ToInt16(arg2) &&
                                    sale.Item.Price < Convert.ToInt32(arg3)
                                )
                               select new
                               {
                                   ItemID = sale.Item.Id,
                                   ItemName = sale.Item.Name,
                                   sale.Item.ItemType,
                                   sale.Item.Price,
                                   sale.Quantity,
                                   sale.TotalPrice,
                                   salesManId = sale.User.Id,
                                   salesManFName = sale.User.FirstName,
                                   salesManLName = sale.User.LastName,
                                   sale.SaleDate
                               };
                    }
                case "ByDate":
                    {
                        return from sale in db.Sales
                               where
                                (
                                    sale.SaleDate.Date.ToString() == arg1 &&
                                    sale.Item.Price > Convert.ToInt16(arg2) &&
                                    sale.Item.Price < Convert.ToInt32(arg3)
                                )
                               select new
                               {
                                   ItemID = sale.Item.Id,
                                   ItemName = sale.Item.Name,
                                   sale.Item.ItemType,
                                   sale.Item.Price,
                                   sale.Quantity,
                                   sale.TotalPrice,
                                   salesManId = sale.User.Id,
                                   salesManFName = sale.User.FirstName,
                                   salesManLName = sale.User.LastName,
                                   sale.SaleDate
                               };
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
                               select new
                               {
                                   ItemID = sale.Item.Id,
                                   ItemName = sale.Item.Name,
                                   sale.Item.ItemType,
                                   sale.Item.Price,
                                   sale.Quantity,
                                   sale.TotalPrice,
                                   salesManId = sale.User.Id,
                                   salesManFName = sale.User.FirstName,
                                   salesManLName = sale.User.LastName,
                                   sale.SaleDate
                               };
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
                        return from sale in db.Sales
                               where
                                (
                                    sale.Item.ItemType == arg1 &&
                                    sale.Item.ItemInnerType == arg2 &&
                                    sale.SaleDate.Date.ToString() == arg3 &&
                                    sale.Item.Price > Convert.ToInt16(arg4) &&
                                    sale.Item.Price < Convert.ToInt32(arg5)
                                )
                               select new
                               {
                                   ItemID = sale.Item.Id,
                                   ItemName = sale.Item.Name,
                                   sale.Item.ItemType,
                                   sale.Item.Price,
                                   sale.Quantity,
                                   sale.TotalPrice,
                                   salesManId = sale.User.Id,
                                   salesManFName = sale.User.FirstName,
                                   salesManLName = sale.User.LastName,
                                   sale.SaleDate
                               };
                    }
                case "ByTypeInnerTypeSalesman":
                    {
                        return from sale in db.Sales
                               where
                                (
                                    sale.Item.ItemType == arg1 &&
                                    sale.Item.ItemInnerType == arg2 &&
                                    sale.User.Id == Convert.ToInt16(arg3) &&
                                    sale.Item.Price > Convert.ToInt16(arg4) &&
                                    sale.Item.Price < Convert.ToInt32(arg5)
                                )
                               select new
                               {
                                   ItemID = sale.Item.Id,
                                   ItemName = sale.Item.Name,
                                   sale.Item.ItemType,
                                   sale.Item.Price,
                                   sale.Quantity,
                                   sale.TotalPrice,
                                   salesManId = sale.User.Id,
                                   salesManFName = sale.User.FirstName,
                                   salesManLName = sale.User.LastName,
                                   sale.SaleDate
                               };
                    }
                case "ByTypeInnerTypeColor":
                    {
                        return from sale in db.Sales
                               where
                                (
                                    sale.Item.ItemType == arg1 &&
                                    sale.Item.ItemInnerType == arg2 &&
                                    sale.Item.Color == arg3 &&
                                    sale.Item.Price > Convert.ToInt16(arg4) &&
                                    sale.Item.Price < Convert.ToInt32(arg5)
                                )
                               select new
                               {
                                   ItemID = sale.Item.Id,
                                   ItemName = sale.Item.Name,
                                   sale.Item.ItemType,
                                   sale.Item.Price,
                                   sale.Quantity,
                                   sale.TotalPrice,
                                   salesManId = sale.User.Id,
                                   salesManFName = sale.User.FirstName,
                                   salesManLName = sale.User.LastName,
                                   sale.SaleDate
                               };
                    }
                case "ByTypeInnerTypeSize":
                    {
                        return from sale in db.Sales
                               where
                                (
                                    sale.Item.ItemType == arg1 &&
                                    sale.Item.ItemInnerType == arg2 &&
                                    sale.Item.Size == arg3 &&
                                    sale.Item.Price > Convert.ToInt16(arg4) &&
                                    sale.Item.Price < Convert.ToInt32(arg5)
                                )
                               select new
                               {
                                   ItemID = sale.Item.Id,
                                   ItemName = sale.Item.Name,
                                   sale.Item.ItemType,
                                   sale.Item.Price,
                                   sale.Quantity,
                                   sale.TotalPrice,
                                   salesManId = sale.User.Id,
                                   salesManFName = sale.User.FirstName,
                                   salesManLName = sale.User.LastName,
                                   sale.SaleDate
                               };
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
                        return from sale in db.Sales
                               where
                                (
                                    sale.Item.ItemType == arg1 &&
                                    sale.Item.ItemInnerType == arg2 &&
                                    sale.Item.Color == arg3 &&
                                    sale.Item.Size == arg4 &&
                                    sale.Item.Price > Convert.ToInt16(arg5) &&
                                    sale.Item.Price < Convert.ToInt32(arg6)
                                )
                               select new
                               {
                                   ItemID = sale.Item.Id,
                                   ItemName = sale.Item.Name,
                                   sale.Item.ItemType,
                                   sale.Item.Price,
                                   sale.Quantity,
                                   sale.TotalPrice,
                                   salesManId = sale.User.Id,
                                   salesManFName = sale.User.FirstName,
                                   salesManLName = sale.User.LastName,
                                   sale.SaleDate
                               };
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
                        return from sale in db.Sales
                               where
                                (
                                    sale.Item.ItemType == arg1 &&
                                    sale.Item.ItemInnerType == arg2 &&
                                    sale.Item.Color == arg3 &&
                                    sale.Item.Size == arg4 &&
                                    sale.User.Id == Convert.ToInt16(arg5) &&
                                    sale.Item.Price > Convert.ToInt16(arg6) &&
                                    sale.Item.Price < Convert.ToInt32(arg7)
                                )
                               select new
                               {
                                   ItemID = sale.Item.Id,
                                   ItemName = sale.Item.Name,
                                   sale.Item.ItemType,
                                   sale.Item.Price,
                                   sale.Quantity,
                                   sale.TotalPrice,
                                   salesManId = sale.User.Id,
                                   salesManFName = sale.User.FirstName,
                                   salesManLName = sale.User.LastName,
                                   sale.SaleDate
                               };
                    }
                case "ByTypeInnerTypeColorSizeDate":
                    {
                        return from sale in db.Sales
                               where
                                (
                                    sale.Item.ItemType == arg1 &&
                                    sale.Item.ItemInnerType == arg2 &&
                                    sale.Item.Color == arg3 &&
                                    sale.Item.Size == arg4 &&
                                    sale.SaleDate.Date.ToString() == arg5 &&
                                    sale.Item.Price > Convert.ToInt16(arg6) &&
                                    sale.Item.Price < Convert.ToInt32(arg7)
                                )
                               select new
                               {
                                   ItemID = sale.Item.Id,
                                   ItemName = sale.Item.Name,
                                   sale.Item.ItemType,
                                   sale.Item.Price,
                                   sale.Quantity,
                                   sale.TotalPrice,
                                   salesManId = sale.User.Id,
                                   salesManFName = sale.User.FirstName,
                                   salesManLName = sale.User.LastName,
                                   sale.SaleDate
                               };
                    }
                default:
                    {
                        return GetTable("Sales");
                    }
            };

        }

        public List<string> GetSaleInfo(string customerId, string saleId)
        {
            return new List<string>()
            {
                db.customers.Single(x => x.Id == Convert.ToInt16(customerId)).FirstName,
                db.customers.Single(x => x.Id == Convert.ToInt16(customerId)).LastName,
                db.Sales.Where(x => x.Id == Convert.ToInt16(saleId)).Select(x => x.Item.Name).ToList()[0],
                db.Sales.Single(x => x.Id == Convert.ToInt16(saleId)).Quantity.ToString(),
                db.Sales.Single(x => x.Id == Convert.ToInt16(saleId)).TotalPrice.ToString(),
                db.Sales.Single(x => x.Id == Convert.ToInt16(saleId)).SaleDate.ToString(),
            };
        }

        // GetTable Overloads - an overload for each case of given arguments
        public IEnumerable<object> GetTable(string s = "*")
        {
            switch (s)
            {
                case "Sales":
                    {
                        return from sale in db.Sales
                               join item in db.Items
                               on sale.Item.Id equals item.Id
                               join user in db.Users
                               on sale.User.Id equals user.Id
                               select new
                               {
                                   item.Id,
                                   item.Name,
                                   item.ItemType,
                                   item.ItemInnerType,
                                   item.Price,
                                   item.Color,
                                   item.Size,
                                   sale.Quantity,
                                   sale.TotalPrice,
                                   salesManId = user.Id,
                                   user.FirstName,
                                   user.LastName,
                                   sale.SaleDate
                               };
                    }
                case "Users":
                    {
                        return from user in db.Users
                               select new
                               {
                                   UserID = user.Id,
                                   user.FirstName,
                                   user.LastName,
                                   user.Email,
                                   user.HireDate,
                                   user.SalesTotal,
                                   user.SalesCount,
                                   UserType = user.UserType.ToString()
                               };
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
                        return from stock in db.Stocks
                               join item in db.Items
                               on stock.Item.Id equals item.Id
                               select new
                               {
                                   item.Id,
                                   item.Name,
                                   item.ItemType,
                                   item.ItemInnerType,
                                   item.Price,
                                   item.Color,
                                   item.Size,
                                   InStock = stock.Quantity,
                                   item.Created,
                               };
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
                               select new
                               {
                                   item.Id,
                                   item.Name,
                                   item.ItemType,
                                   item.ItemInnerType,
                                   item.Price,
                                   item.Color,
                                   item.Size,
                                   InStock = stock.Quantity,
                                   item.Created,
                               };
                    }

                case "ViewsByColor":
                    {
                        return from stock in db.Stocks
                               join item in db.Items
                               on stock.Item.Id equals item.Id
                               where item.Color == arg1
                               select new
                               {
                                   item.Id,
                                   item.Name,
                                   item.ItemType,
                                   item.ItemInnerType,
                                   item.Price,
                                   item.Color,
                                   item.Size,
                                   InStock = stock.Quantity,
                                   item.Created,
                               };
                    }
                case "ViewsBySize":
                    {
                        return from stock in db.Stocks
                               join item in db.Items
                               on stock.Item.Id equals item.Id
                               where item.Size == arg1
                               select new
                               {
                                   item.Id,
                                   item.Name,
                                   item.ItemType,
                                   item.ItemInnerType,
                                   item.Price,
                                   item.Color,
                                   item.Size,
                                   InStock = stock.Quantity,
                                   item.Created,
                               };
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
                               select new
                               {
                                   item.Id,
                                   item.Name,
                                   item.ItemType,
                                   item.ItemInnerType,
                                   item.Price,
                                   item.Color,
                                   item.Size,
                                   InStock = stock.Quantity,
                                   item.Created,
                               };
                    }
                case "ViewsByInnerType":
                    {
                        return from stock in db.Stocks
                               join item in db.Items
                               on stock.Item.Id equals item.Id
                               where item.ItemType == arg1 && item.ItemInnerType == arg2
                               select new
                               {
                                   item.Id,
                                   item.Name,
                                   item.ItemType,
                                   item.ItemInnerType,
                                   item.Price,
                                   item.Color,
                                   item.Size,
                                   InStock = stock.Quantity,
                                   item.Created,
                               };
                    }
                case "ByTypeInnerType":
                    {
                        return from sale in db.Sales
                               where sale.Item.ItemType == arg1 && sale.Item.ItemInnerType == arg2
                               select new
                               {
                                   ItemID = sale.Item.Id,
                                   ItemName = sale.Item.Name,
                                   sale.Item.ItemType,
                                   sale.Item.Price,
                                   sale.Quantity,
                                   sale.TotalPrice,
                                   salesManId = sale.User.Id,
                                   salesManFName = sale.User.FirstName,
                                   salesManLName = sale.User.LastName,
                                   sale.SaleDate
                               };
                    }

                case "ByTPrice":
                    {
                        return from sale in db.Sales
                               where sale.TotalPrice > Convert.ToInt32(arg1) && sale.TotalPrice < Convert.ToInt32(arg2)
                               select new
                               {
                                   ItemID = sale.Item.Id,
                                   ItemName = sale.Item.Name,
                                   sale.Item.ItemType,
                                   sale.Item.Price,
                                   sale.Quantity,
                                   sale.TotalPrice,
                                   salesManId = sale.User.Id,
                                   salesManFName = sale.User.FirstName,
                                   salesManLName = sale.User.LastName,
                                   sale.SaleDate
                               };
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
                               select new
                               {
                                   item.Id,
                                   item.Name,
                                   item.ItemType,
                                   item.ItemInnerType,
                                   item.Price,
                                   item.Color,
                                   item.Size,
                                   InStock = stock.Quantity,
                                   item.Created,
                               };
                    }
                case "ViewsByColor":
                    {
                        return from stock in db.Stocks
                               join item in db.Items
                               on stock.Item.Id equals item.Id
                               where item.ItemType == arg1 && item.ItemInnerType == arg2 && item.Color == arg3
                               select new
                               {
                                   item.Id,
                                   item.Name,
                                   item.ItemType,
                                   item.ItemInnerType,
                                   item.Price,
                                   item.Color,
                                   item.Size,
                                   InStock = stock.Quantity,
                                   item.Created,
                               };
                    }
                case "ViewsBySize":
                    {
                        return from stock in db.Stocks
                               join item in db.Items
                               on stock.Item.Id equals item.Id
                               where item.ItemType == arg1 && item.ItemInnerType == arg2 && item.Size == arg3
                               select new
                               {
                                   item.Id,
                                   item.Name,
                                   item.ItemType,
                                   item.ItemInnerType,
                                   item.Price,
                                   item.Color,
                                   item.Size,
                                   InStock = stock.Quantity,
                                   item.Created,
                               };
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
                               select new
                               {
                                   item.Id,
                                   item.Name,
                                   item.ItemType,
                                   item.ItemInnerType,
                                   item.Price,
                                   item.Color,
                                   item.Size,
                                   InStock = stock.Quantity,
                                   item.Created,
                               };
                    }

                case "ViewsByAllNoPrice":
                    {
                        return from stock in db.Stocks
                               join item in db.Items
                               on stock.Item.Id equals item.Id
                               where item.ItemType == arg1 && item.ItemInnerType == arg2 && item.Color == arg3 && item.Size == arg4
                               select new
                               {
                                   item.Id,
                                   item.Name,
                                   item.ItemType,
                                   item.ItemInnerType,
                                   item.Price,
                                   item.Color,
                                   item.Size,
                                   InStock = stock.Quantity,
                                   item.Created,
                               };
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
                               select new
                               {
                                   ItemID = sale.Item.Id,
                                   ItemName = sale.Item.Name,
                                   sale.Item.ItemType,
                                   sale.Item.Price,
                                   sale.Quantity,
                                   sale.TotalPrice,
                                   salesManId = sale.User.Id,
                                   salesManFName = sale.User.FirstName,
                                   salesManLName = sale.User.LastName,
                                   sale.SaleDate
                               };
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
                               select new
                               {
                                   item.Id,
                                   item.Name,
                                   item.ItemType,
                                   item.ItemInnerType,
                                   item.Price,
                                   item.Color,
                                   item.Size,
                                   InStock = stock.Quantity,
                                   item.Created,
                               };
                    }
                default:
                    {
                        return GetTable();
                    }
            }
        }

        public IEnumerable<object> Search(string str)
        {
            return from items in db.Items
                   where (items.Name).Contains(str)
                   select items;
        }

        // Return UserType as string 
        public string GetUserType(int id)
        {
            return db.Users.Single(user => user.Id == id).UserType.ToString();
        }

        // Return List Methods
        public List<string> GetList(string s)
        {
            switch (s)
            {
                case "ByItem":
                    {
                        return (from sale in db.Sales
                                select sale.Item.ItemType.ToString()).Distinct().ToList();
                    }
                case "BySalesMan":
                    {
                        return (from sale in db.Sales
                                select sale.User.Id.ToString()).Distinct().ToList();
                    }
                case "ByDate":
                    {
                        return (from sale in db.Sales
                                select sale.SaleDate.Date.ToString()).Distinct().ToList();
                    }
                case "ByLogUserId":
                    {
                        return (from log in db.Logs
                                select log.User.Id.ToString()).Distinct().ToList();
                    }
                case "ByLogAction":
                    {
                        return (from log in db.Logs
                                select log.ActionType.ToString()).Distinct().ToList();
                    }
                case "ByLogDate":
                    {
                        return (from log in db.Logs
                                select log.DateTime.Date.ToString()).Distinct().ToList();
                    }
                case "ByCostumer":
                    {
                        return (from costumer in db.customers
                                select costumer.Id.ToString()).Distinct().ToList();
                    }
                default:
                    {
                        return new List<string>();
                    }
            }
        }
        public List<string> GetList(string s, string arg1, string arg2 = "")
        {
            List<string> list;
            switch (s)
            {
                case "ByCostumerDate":
                    {
                        return (from sale in db.Sales
                                where sale.Customer.Id == Convert.ToInt16(arg1)
                                select sale.SaleDate.Date.ToString()).Distinct().ToList();
                    }
                case "SalesByDate":
                    {
                        return (from sale in db.Sales
                                where sale.Customer.Id == Convert.ToInt16(arg1) && sale.SaleDate.Date.ToString() == arg2
                                select $"{sale.Id}").Distinct().ToList();
                    }
                default:
                    {
                        return new List<string>();
                    }
            }
        }
    }
}

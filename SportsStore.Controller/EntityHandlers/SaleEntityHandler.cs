using SportsStore.Enums;
using SportsStore.Model.Costumers;
using SportsStore.Model.Items;
using SportsStore.Model.Users;

namespace SportsStore.Controller
{
    public class SaleEntityHandler : EntityHandler, IEntityHandalable
    {
        private readonly LogEntityHandler Logger = LogEntityHandler.Logger;

        //Create
        public void Add(params string[] args)
        {
            int id = Convert.ToInt32(args[0]);
            int quantity = Convert.ToInt32(args[1]);
            int customerId = Convert.ToInt32(args[2]);

            if (quantity <= Db.Stocks.Single(x => x.Id == id).Quantity && LoggedInUser is not null)
            {
                Stock item = Db.Stocks.Single(x => x.Id == id);
                Customer customer = Db.Customers.Single(x => x.Id == customerId);
                double price = item.Price * quantity;

                item.Quantity -= quantity;

                customer.PurchasesCount = customer.PurchasesCount is null ? 1 : customer.PurchasesCount += 1;
                customer.TotalPurchases = customer.TotalPurchases is null ? price : customer.TotalPurchases += price;
                customer.LastPurchase = DateTime.Now;

                LoggedInUser.SalesCount = LoggedInUser.SalesCount is null ? 1 : LoggedInUser.SalesCount += 1;
                LoggedInUser.SalesTotal = LoggedInUser.SalesTotal is null ? price : LoggedInUser.SalesTotal += price;
                LoggedInUser.LastSale = DateTime.Now;
                
                Sale sale = new()
                {
                    Stock = item,
                    Quantity = quantity,
                    Customer = customer,
                    TotalPrice = price,
                    SaleDate = DateTime.Now,
                    User = LoggedInUser
                };

                Db.Sales.Add(sale);
                Db.SaleViews.CreateView(Db, sale);
                Logger.Add(new string[] { LoggedInUser.Id.ToString(), $"Sale! {LoggedInUser.Id} made a sale: {price}$", ActionTypes.Sale.ToString() });
                Db.SaveChanges();
            }
        }
        public void AddSearchLog() => Db.Sales.AddSearchLog(Db, Logger);
        public void Generate()
        {
            Random rnd = new();

            DateTime RandomDate(DateTime start, DateTime end) => start.AddDays(rnd.Next((end - start).Days));
            DateTime end = DateTime.Now;

            List<Stock> stocks = Db.Stocks.ToList();
            List<User> users = Db.Users.ToList();
            List<Customer> customers = Db.Customers.ToList();

            for (int i = 0; i < rnd.Next(1, 100); i++)
            {
                User user = users[rnd.Next(2, users.Count)];
                UsersView userView = Db.usersView.Single(x => x.Id == user.Id);
                Customer customer = customers[rnd.Next(0, customers.Count)];
                DateTime saleDate = RandomDate(customer.AddedAt, end);

                stocks.ForEach(stock =>
                {
                    int quantity = rnd.Next(1, 100);
                    double totalPrice = stock.Price * quantity;

                    stock.Quantity = quantity;

                    Sale sale = new()
                    {
                        Stock = stock,
                        Quantity = quantity,
                        Customer = customer,
                        TotalPrice = totalPrice,
                        SaleDate = saleDate,
                        User = user
                    };

                    Db.Sales.Add(sale);
                    Db.SaleViews.CreateView(Db, sale);

                    var query = (from sales in Db.Sales
                                 orderby sales.SaleDate
                                 where sales.Customer.Id == customer.Id
                                 select sales.SaleDate).Last();

                    customer.PurchasesCount = customer.PurchasesCount == null || customer.PurchasesCount == 0 ? 1 : customer.PurchasesCount += 1;
                    customer.TotalPurchases = customer.TotalPurchases == null || customer.TotalPurchases == 0 ? totalPrice : customer.TotalPurchases += totalPrice;
                    customer.LastPurchase = query;

                    user.SalesCount = user.SalesCount == null || user.SalesCount == 0 ? 1 : user.SalesCount += 1;
                    user.SalesTotal = user.SalesTotal == null || user.SalesTotal == 0 ? totalPrice : user.SalesTotal += totalPrice;
                    user.LastSale = query;

                    userView.SalesCount = userView.SalesCount == null || userView.SalesCount == 0 ? 1 : userView.SalesCount += 1;
                    userView.SalesTotal = userView.SalesTotal == null || userView.SalesTotal == 0 ? totalPrice : userView.SalesTotal += totalPrice;
                    userView.LastSale = query;

                    Db.Sales.AddGenerateRecordsLog(Db, Logger);
                    Db.SaveChanges();
                });
            }

            Db.Sales.AddGenerateRecordsLog(Db, Logger);
            Db.SaveChanges();
        }

        // Read
        public IEnumerable<object> GetTable(string s, params string[] args) => s switch
        {
            "Default" => from sale in Db.SaleViews
                         where
                          (
                              sale.ItemPrice > Convert.ToInt16(args[0]) &&
                              sale.ItemPrice < Convert.ToInt32(args[1])
                          )
                         select sale,

            "ByItemId" => from sale in Db.SaleViews
                          where
                            (
                                sale.ItemId == Convert.ToInt16(args[0]) &&
                                sale.ItemPrice > Convert.ToInt16(args[1]) &&
                                sale.ItemPrice < Convert.ToInt32(args[2])
                            )
                          select sale,

            "ByType" => from sale in Db.SaleViews
                        where
                              (
                                  sale.ItemType == args[0] &&
                                  sale.ItemPrice > Convert.ToInt16(args[1]) &&
                                  sale.ItemPrice < Convert.ToInt32(args[2])
                              )
                        select sale,
            "BySalseMan" => from sale in Db.SaleViews
                            where
                                  (
                                      sale.SalsemanId == Convert.ToInt16(args[0]) &&
                                      sale.ItemPrice > Convert.ToInt16(args[1]) &&
                                      sale.ItemPrice < Convert.ToInt32(args[2])
                                  )
                            select sale,
            "ByDate" => from sale in Db.SaleViews
                        where
                              (
                                  sale.SaleDate.Date.ToString() == args[0] &&
                                  sale.ItemPrice > Convert.ToInt16(args[1]) &&
                                  sale.ItemPrice < Convert.ToInt32(args[2])
                              )
                        select sale,

            "ByType_InnerType" => from sale in Db.SaleViews
                                  where
                                  (
                                      sale.ItemType == args[0] &&
                                      sale.ItemInnerType == args[1] &&
                                      sale.ItemPrice > Convert.ToInt16(args[2]) &&
                                      sale.ItemPrice < Convert.ToInt32(args[3])
                                  )
                                  select sale,

            "ByType_InnerType_Date" => from sale in Db.SaleViews
                                       where
                                        (
                                            sale.ItemType == args[0] &&
                                            sale.ItemInnerType == args[1] &&
                                            sale.SaleDate.Date.ToString() == args[2] &&
                                            sale.ItemPrice > Convert.ToInt16(args[3]) &&
                                            sale.ItemPrice < Convert.ToInt32(args[4])
                                        )
                                       select sale,

            "ByType_InnerType_Salesman" => from sale in Db.SaleViews
                                           where
                                            (
                                                sale.ItemType == args[0] &&
                                                sale.ItemInnerType == args[1] &&
                                                sale.SalsemanId == Convert.ToInt16(args[2]) &&
                                                sale.ItemPrice > Convert.ToInt16(args[3]) &&
                                                sale.ItemPrice < Convert.ToInt32(args[4])
                                            )
                                           select sale,

            "ByType_InnerType_Color" => from sale in Db.SaleViews
                                        where
                                         (
                                             sale.ItemType == args[0] &&
                                             sale.ItemInnerType == args[1] &&
                                             sale.ItemColor == args[2] &&
                                             sale.ItemPrice > Convert.ToInt16(args[3]) &&
                                             sale.ItemPrice < Convert.ToInt32(args[4])
                                         )
                                        select sale,

            "ByType_InnerType_Size" => from sale in Db.SaleViews
                                       where
                                        (
                                            sale.ItemType == args[0] &&
                                            sale.ItemInnerType == args[1] &&
                                            sale.ItemSize == args[2] &&
                                            sale.ItemPrice > Convert.ToInt16(args[3]) &&
                                            sale.ItemPrice < Convert.ToInt32(args[4])
                                        )
                                       select sale,

            "ByType_InnerType_Color_Size" => from sale in Db.SaleViews
                                             where
                                              (
                                                  sale.ItemType == args[0] &&
                                                  sale.ItemInnerType == args[1] &&
                                                  sale.ItemColor == args[2] &&
                                                  sale.ItemSize == args[3] &&
                                                  sale.ItemPrice > Convert.ToInt16(args[4]) &&
                                                  sale.ItemPrice < Convert.ToInt32(args[5])
                                              )
                                             select sale,

            "ByType_InnerType_Color_Size_Salesman" => from sale in Db.SaleViews
                                                      where
                                                       (
                                                           sale.ItemType == args[0] &&
                                                           sale.ItemInnerType == args[1] &&
                                                           sale.ItemColor == args[2] &&
                                                           sale.ItemSize == args[3] &&
                                                           sale.SalsemanId == Convert.ToInt16(args[4]) &&
                                                           sale.ItemPrice > Convert.ToInt16(args[5]) &&
                                                           sale.ItemPrice < Convert.ToInt32(args[6])
                                                       )
                                                      select sale,
            "ByType_InnerType_Color_Size_Date" => from sale in Db.SaleViews
                                                  where
                                                   (
                                                       sale.ItemType == args[0] &&
                                                       sale.ItemInnerType == args[1] &&
                                                       sale.ItemColor == args[2] &&
                                                       sale.ItemSize == args[3] &&
                                                       sale.SaleDate.Date.ToString() == args[4] &&
                                                       sale.ItemPrice > Convert.ToInt16(args[5]) &&
                                                       sale.ItemPrice < Convert.ToInt32(args[6])
                                                   )
                                                  select sale,
            _ => Db.SaleViews
        };
        public List<string> GetList(string s, params string[] args) => s switch
        {
            "ByItem" => (from sale in Db.Sales
                         select sale.Stock.ItemType.ToString()).Distinct().ToList(),

            "BySalesMan" => (from sale in Db.Sales
                             select sale.User.Id.ToString()).Distinct().ToList(),

            "ByDate" => (from sale in Db.Sales
                         select sale.SaleDate.Date.ToString()).Distinct().ToList(),

            "SalesByDate" => (from sale in Db.Sales
                              where sale.Customer.Id == Convert.ToInt16(args[0]) && sale.SaleDate.Date.ToString() == args[1]
                              select $"{sale.Id}").Distinct().ToList(),

            "SalesInfo" => new List<string>()
                {
                    Db.Customers.Single(x => x.Id == Convert.ToInt16(args[0])).FirstName,
                    Db.Customers.Single(x => x.Id == Convert.ToInt16(args[0])).LastName,
                    Db.Sales.Where(x => x.Id == Convert.ToInt16(args[1])).Select(x => x.Stock.Name).ToList()[0],
                    Db.Sales.Single(x => x.Id == Convert.ToInt16(args[1])).Quantity.ToString(),
                    Db.Sales.Single(x => x.Id == Convert.ToInt16(args[1])).TotalPrice.ToString(),
                    Db.Sales.Single(x => x.Id == Convert.ToInt16(args[1])).SaleDate.ToString(),
                },

            _ =>
                     new List<string>()
        };
        public IEnumerable<object> Search(string s, string arg = "") =>
            throw new NotImplementedException();
        public List<string> GetStats()
        {
            string bestItem = (from sale in Db.Sales
                               orderby sale.Stock.Name ascending
                               select sale.Stock.Name).First();

            string bestItemType = (from sale in Db.Sales
                                   orderby sale.Stock.ItemType ascending
                                   select sale.Stock.ItemType).First();

            string bestItemInnerType = (from sale in Db.Sales
                                        orderby sale.Stock.ItemInnerType ascending
                                        select sale.Stock.ItemInnerType).First();

            string bestItemColor = (from sale in Db.Sales
                                    orderby sale.Stock.Color ascending
                                    select sale.Stock.Color).First();

            string bestItemSize = (from sale in Db.Sales
                                   orderby sale.Stock.Size ascending
                                   select sale.Stock.Size).First();

            string bestSalesman = (from sale in Db.Sales
                                   orderby sale.User.Id ascending
                                   select $"{sale.User.FirstName} {sale.User.LastName}").First();

            string bestSaleDate = (from sale in Db.Sales
                                   orderby sale.SaleDate ascending
                                   select sale.SaleDate.Date.ToString()).First();

            return new List<string>() { bestItem, bestItemType, bestItemInnerType,
                                        bestItemColor, bestItemSize, bestSalesman, bestSaleDate };
        }

        //Update
        public bool Update(string s, params string[] args) => throw new NotImplementedException();

        //Delete
        public void Remove(int id) => throw new NotImplementedException();
        public void Clear()
        {
            Db.Sales.Clear(Db);
            Db.Sales.AddClearRowsLog(Db, Logger);
        }
    }
}

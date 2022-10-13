using SportsStore.Enums;
using SportsStore.Model.Costumers;

namespace SportsStore.Controller
{
    public class CustomerEntityHandler : EntityHandler, IEntityHandalable
    {
        private readonly LogEntityHandler Logger = LogEntityHandler.Logger;

        //Create
        public void Add(params string[] args)
        {
            Db.Customers.Add(new()
            {
                FirstName = args[0],
                LastName = args[1],
                Email = args[2],
                DateOfBirth = Convert.ToDateTime(args[3]),
                AddedAt = string.IsNullOrEmpty(args[4]) ? DateTime.Now : Convert.ToDateTime(args[4]),
                TotalPurchases = 0,
                PurchasesCount = 0,
                Discount = 0,
                LastPurchase = null
            });
            Logger.Add(new string[] { LoggedInUser.Id.ToString(), $"Added Customer: {args[0]} {args[1]}", ActionTypes.AddCustomer.ToString() });
            Db.SaveChanges();
        }
        public void AddSearchLog() => Db.Customers.AddSearchLog(Db, Logger);
        public void Generate()
        {
            Random rnd = new();
            DateTime RandomDate(DateTime start, DateTime end) => start.AddDays(rnd.Next((end-start).Days));

            DateTime startDob = new(1950, 1, 1);
            DateTime endDob = new(2009, 1, 1);
            DateTime start = new(2020, 1, 1);
            DateTime end = DateTime.Now;

            List<Customer> generatedCustomers = new()
            {
                new() {FirstName = "Roman", LastName = "Kuman", Email = "roman@email.com"},
                new() {FirstName = "Mimi", LastName = "Henderix", Email = "roman@email.com"},
                new() {FirstName = "Peter", LastName = "Ben", Email = "peter@email.com"},
                new() {FirstName = "Guy", LastName = "Yanai", Email = "guy@email.com"},
                new() {FirstName = "Yona", LastName = "Sarona", Email = "yona@email.com"},
                new() {FirstName = "Shosh", LastName = "Vendeta", Email = "shosh@email.com"},
            };
            generatedCustomers.ForEach(customer =>
            {
                customer.DateOfBirth = RandomDate(startDob, endDob); 
                customer.AddedAt = RandomDate(start, end);
                Add(new string[] {customer.FirstName, customer.LastName, customer.Email, customer.DateOfBirth.Date.ToString(), customer.AddedAt.Date.ToString() });
            });

            List<Customer> customers = Db.Customers.ToList();
            customers.ForEach(customer =>
            {
                customer.TotalPurchases = 0;
                customer.PurchasesCount = rnd.Next(5, 25);
                customer.Discount = customer.AddedAt.Year == 2020 ? 15 :
                customer.Discount = customer.AddedAt.Year == 2021 ? 10 : 0;
                customer.LastPurchase = null;
            });

            Db.Customers.AddGenerateRecordsLog(Db, Logger);
            Db.SaveChanges();
        }

        //Read
        public IEnumerable<object> GetTable(string s, params string[] args) => s switch
        {
            "Default" =>
            from customer in Db.Customers
            where
             (
                 DateTime.Now.Year - customer.DateOfBirth.Year > Convert.ToInt16(args[0]) &&
                 DateTime.Now.Year - customer.DateOfBirth.Year < Convert.ToInt16(args[1]) &&
                 customer.TotalPurchases > Convert.ToInt16(args[2]) &&
                 customer.TotalPurchases < Convert.ToInt32(args[3]) &&
                 customer.PurchasesCount > Convert.ToInt16(args[4]) &&
                 customer.PurchasesCount < Convert.ToInt32(args[5])
             )
            select customer,

            "ById" => from customer in Db.Customers
                      where
                       (
                           customer.Id == Convert.ToInt16(args[6]) &&
                           DateTime.Now.Year - customer.DateOfBirth.Year > Convert.ToInt16(args[0]) &&
                           DateTime.Now.Year - customer.DateOfBirth.Year < Convert.ToInt16(args[1]) &&
                           customer.TotalPurchases > Convert.ToInt16(args[2]) &&
                           customer.TotalPurchases < Convert.ToInt32(args[3]) &&
                           customer.PurchasesCount > Convert.ToInt16(args[4]) &&
                           customer.PurchasesCount < Convert.ToInt32(args[5])
                       )
                      select customer,
            "ByDate" => (from customer in Db.Customers
                         join sale in Db.Sales
                         on customer.Id equals sale.Customer.Id
                         where
                          (
                              sale.SaleDate.Date.ToString() == args[6] &&
                              DateTime.Now.Year - customer.DateOfBirth.Year > Convert.ToInt16(args[0]) &&
                              DateTime.Now.Year - customer.DateOfBirth.Year < Convert.ToInt16(args[1]) &&
                              customer.TotalPurchases > Convert.ToInt16(args[2]) &&
                              customer.TotalPurchases < Convert.ToInt32(args[3]) &&
                              customer.PurchasesCount > Convert.ToInt16(args[4]) &&
                              customer.PurchasesCount < Convert.ToInt32(args[5])
                          )
                         select customer).Distinct(),

            "ByAll" => from customer in Db.Customers
                       join sale in Db.Sales
                       on customer.Id equals sale.Customer.Id
                       where
                                     (
                                         customer.Id == Convert.ToInt16(args[6]) &&
                                         sale.SaleDate.Date.ToString() == args[7] &&
                                         DateTime.Now.Year - customer.DateOfBirth.Year > Convert.ToInt16(args[0]) &&
                                         DateTime.Now.Year - customer.DateOfBirth.Year < Convert.ToInt16(args[1]) &&
                                         customer.TotalPurchases > Convert.ToInt16(args[2]) &&
                                         customer.TotalPurchases < Convert.ToInt32(args[3]) &&
                                         customer.PurchasesCount > Convert.ToInt16(args[4]) &&
                                         customer.PurchasesCount < Convert.ToInt32(args[5])
                                     )
                       select customer,

            _ => Db.Customers
        };
        public List<string> GetList(string s, params string[] args) => s switch
        {
            "ByCustomer" => (from costumer in Db.Customers
                             select costumer.Id.ToString()).Distinct().ToList(),

            "ByCustomer_Date" => (from customer in Db.Customers
                                  join sale in Db.Sales
                                  on customer.Id equals sale.Customer.Id
                                  where sale.SaleDate.Date.ToString() == args[0]
                                  select customer.Id.ToString()).Distinct().ToList(),

            "CustomerDetails" => new List<string>()
                {
                    Db.Customers.Single(x => x.Id.ToString() == args[0]).FirstName,
                    Db.Customers.Single(x => x.Id.ToString() == args[0]).LastName,
                    Db.Customers.Single(x => x.Id.ToString() == args[0]).Email,
                    Db.Customers.Single(x => x.Id.ToString() == args[0]).DateOfBirth.Date.ToString()
                },

            _ =>
                     new List<string>()
        };
        public IEnumerable<object> Search(string s, string arg = "") =>
            from customer in Db.Customers
            where (customer.FirstName).Contains(arg) || (customer.LastName).Contains(arg)
            select customer;
        public List<string> GetStats()
        {
            string mostSpent = (from customer in Db.Customers
                                orderby customer.TotalPurchases descending
                                select $"{customer.FirstName} {customer.LastName}").First();

            string mostReturning = (from customer in Db.Customers
                                    orderby customer.PurchasesCount descending
                                    select $"{customer.FirstName} {customer.LastName}").First();

            string mostVeteran = (from customer in Db.Customers
                                  orderby customer.AddedAt ascending
                                  select $"{customer.FirstName} {customer.LastName}").First();

            string mostNew = (from customer in Db.Customers
                              orderby customer.AddedAt descending
                              select $"{customer.FirstName} {customer.LastName}").First();

            return new List<string>() { mostSpent, mostReturning, mostVeteran, mostNew };
        }

        //Update
        public bool Update(string s, string[] args)
        {
            try
            {
                switch (s)
                {
                    case "FirstName":
                        {
                            Db.Customers.Single(customer => customer.Id == Convert.ToInt16(args[0])).FirstName = args[1];
                            break;
                        }
                    case "LastName":
                        {
                            Db.Customers.Single(customer => customer.Id == Convert.ToInt16(args[0])).LastName = args[2];
                            break;
                        }
                    case "Email":
                        {
                            Db.Customers.Single(customer => customer.Id == Convert.ToInt16(args[0])).Email = args[3];
                            break;
                        }
                    case "DOB":
                        {
                            Db.Customers.Single(customer => customer.Id == Convert.ToInt16(args[0])).DateOfBirth = Convert.ToDateTime(args[4]);
                            break;
                        }
                }
                Db.Customers.AddUpdateLog(Db, Logger);
                Db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Delete
        public void Remove(int id)
        {
            Db.Customers.Remove(Db.Customers.Single(cstmr => cstmr.Id == id));
            Db.Customers.AddRemoveRecordLog(Db, Logger, id);
            Db.SaveChanges();
        }
        public void Clear()
        {
            Db.Customers.Clear(Db);
            Db.Customers.AddClearRowsLog(Db, Logger);
        }
    }
}

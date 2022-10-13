using Microsoft.EntityFrameworkCore;
using SportsStore.Model.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Controller
{
    public static class CreateSaleView
    {
        public static void CreateView(this DbSet<SalesView> Db, DbContext dataBase, Sale sale)
        {
            Db.Add(
                new SalesView
                {
                    SaleDate = sale.SaleDate,
                    ItemId = sale.Stock.Id,
                    Quantity = sale.Stock.Quantity,
                    TotalPrice = sale.TotalPrice,
                    ItemName = sale.Stock.Name,
                    ItemType = sale.Stock.ItemType,
                    ItemInnerType = sale.Stock.ItemInnerType,
                    ItemPrice = sale.Stock.Price,
                    ItemColor = sale.Stock.Color ?? string.Empty,
                    ItemSize = sale.Stock.Size ?? string.Empty,
                    SalsemanId = sale.User.Id,
                    SalsemanFname = sale.User.FirstName,
                    SalsemanLname = sale.User.LastName,
                    CustomerId = sale.Customer.Id,
                    CustomerFname = sale.Customer.FirstName,
                    CustomerLname = sale.Customer.LastName
                });
            dataBase.SaveChanges();
        }
    }
}

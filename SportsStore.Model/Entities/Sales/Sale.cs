using SportsStore.Model.Costumers;
using SportsStore.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Model.Items
{
    public class Sale
    {
        public virtual int Id { get; set; }
        public Item Item { get; set; }
        public User User { get; set; }
        public Customer Customer { get; set; }

        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        
    }
    public class SalesView
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public  int ThisItemId { get; set; }
        public  string ItemName { get; set; }
        public  string ItemType { get; set; }
        public  string ItemInnerType { get; set; }
        public  double ItemPrice { get; set; }
        public  string ItemColor { get; set; }
        public  string ItemSize { get; set; }
        public  int SalsemanId { get; set; }
        public  string SalsemanFname { get; set; }
        public  string SalsemanLname { get; set; }
    }
}

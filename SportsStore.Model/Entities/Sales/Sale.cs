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
        public int Id { get; set; }
        public Item Item { get; set; }
        public User User { get; set; }
        public Customer? Customer { get; set; }
        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
}

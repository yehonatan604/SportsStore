using SportsStore.Model.Costumers;
using SportsStore.Model.Items;
using SportsStore.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Model.Entities.Sales
{
    public class Refund
    {
        public int Id { get; set; }
        public Sale Sale { get; set; }
        public string Reason { get; set; }
        public DateTime RefundDate { get; set; }
        public double TotalPrice { get; set; }
    }
}

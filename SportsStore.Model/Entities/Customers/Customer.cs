using SportsStore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Model.Costumers
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime AddedAt { get; set; }
        public double? TotalPurchases { get; set; }
        public int? PurchasesCount { get; set; }
        public double? Discount { get; set; }
        public DateTime? LastPurchase { get; set; }
    }
}

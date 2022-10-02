using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Model.Items
{ 
    public  class Stock
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public DateTime LastAdded { get; set; }

        public int ThisItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public string ItemInnerType { get; set; }
        public double ItemPrice { get; set; }
        public string ItemColor { get; set; }
        public string ItemSize { get; set; }
        public DateTime ItemCreated { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Model.Items
{
    public class Stock
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ItemType { get; set; }
        public virtual string ItemInnerType { get; set; }
        public virtual string? Size { get; set; }
        public virtual string? Color { get; set; }
        public virtual double Price { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual int Quantity { get; set; }
        public virtual DateTime LastAdded { get; set; }
    }
}

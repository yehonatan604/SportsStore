using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Model.Items
{
    public abstract class Item
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ItemType { get; set; }
        public virtual string ItemInnerType { get; set; }
        public virtual string? Size { get; set; }
        public virtual string? Color { get; set; }
        public virtual double Price { get; set; }
        public virtual DateTime Created { get; set; }

        public Item(string name, double price, string color, string itemInnerTtpe, string size)
        {
            Name = name;
            Price = price;
            Created = DateTime.Now;
            Color = color;
            Size = size;
            ItemInnerType = itemInnerTtpe;
        }
    }
}

using SportsStore.Enums;
using SportsStore.Model.Items;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Controller.CRUD
{
    public abstract class ItemCreator
    {
        public virtual string Name { get; set; }
        public virtual string ItemInnerType { get; set; }
        public virtual string Color { get; set; }
        public virtual string Size { get; set; }
        public virtual double Price { get; set; }

        public abstract Stock CreateItem(string name, double price, string color, string itemInnerType, string size);
    }
}

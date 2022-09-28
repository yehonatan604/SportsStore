using SportsStore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Model.Items
{
    public class Clothe : Item
    {
        public Clothe(string name, double price, string color, string itemInnerType, string size) : base(name, price, color,itemInnerType, size)
        {
            ItemType = ItemTypes.Clothe.ToString();
        }

    }
}

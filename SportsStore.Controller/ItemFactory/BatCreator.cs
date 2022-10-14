using SportsStore.Enums;
using SportsStore.Model.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Controller.CRUD
{
    public class BatCreator : ItemCreator
    {
        public override Stock CreateItem(string name, double price, string color, string itemInnerType, string size) =>
            new Net()
            {
                Name = name,
                Price = price,
                Color = color,
                ItemType = ItemTypes.Bat.ToString(),
                ItemInnerType = itemInnerType,
                Size = string.Empty,
                LastAdded = DateTime.Now,
                Created = DateTime.Now,
                Quantity = 0
            };
    }
}

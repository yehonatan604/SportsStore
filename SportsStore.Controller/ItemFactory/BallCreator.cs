using SportsStore.Enums;
using SportsStore.Model.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Controller.CRUD
{
    public class BallCreator : ItemCreator
    {
        public override Stock CreateItem(string name, double price, string color, string itemInnerType, string size) =>
            new Ball()
            {
                Name = name,
                Price = price,
                Color = color,
                ItemType = ItemTypes.Ball.ToString(),
                ItemInnerType = itemInnerType,
                Size = string.Empty,
                LastAdded = DateTime.Now,
                Created = DateTime.Now,
                Quantity = 0
            };
    }
}
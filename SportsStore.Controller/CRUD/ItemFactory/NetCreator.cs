using SportsStore.Model.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Controller.CRUD
{
    public class NetCreator : ItemCreator
    {
        public NetCreator(string name, double price, string color, string itemInnerType, string size) :
                    base(name, price, color, size) => ItemInnerType = itemInnerType;
        public override Item CreateItem() => new Net(Name, Price, "", ItemInnerType, "");
    }
}
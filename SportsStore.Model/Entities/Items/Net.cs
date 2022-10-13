using SportsStore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Model.Items
{
    public class Net : Stock
    {
        public Net() : base()
        {
            ItemType = ItemTypes.Net.ToString();
        }
    }
}

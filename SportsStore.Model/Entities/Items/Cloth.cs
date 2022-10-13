using SportsStore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Model.Items
{
    public class Clothe : Stock
    {
        public Clothe() : base()
        {
            ItemType = ItemTypes.Clothe.ToString();
        }
    }
}

using SportsStore.Enums;

namespace SportsStore.Model.Items
{
    public class Bat : Item
    {

        public Bat(string name, double price, string color, string itemInnerType, string size) :
                    base(name, price, color, itemInnerType, size)
        {
            ItemType = ItemTypes.Bat.ToString();
        }
    }
}
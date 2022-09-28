using SportsStore.Enums;


namespace SportsStore.Model.Items
{
    public class Ball : Item
    {
        public Ball(string name, double price, string color, string itemInnerType, string size) : 
                    base(name, price, color, itemInnerType, size)
        {
            ItemType = ItemTypes.Ball.ToString();
        }
    }
}

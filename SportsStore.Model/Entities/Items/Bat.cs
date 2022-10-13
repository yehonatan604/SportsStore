using SportsStore.Enums;

namespace SportsStore.Model.Items
{
    public class Bat : Stock
    {

        public Bat() : base()
        {
            ItemType = ItemTypes.Bat.ToString();
        }
    }
}
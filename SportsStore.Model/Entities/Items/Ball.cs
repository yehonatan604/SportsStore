using SportsStore.Enums;


namespace SportsStore.Model.Items
{
    public class Ball : Stock
    {
        public Ball() : base()
        {
            ItemType = ItemTypes.Ball.ToString();
        }
    }
}

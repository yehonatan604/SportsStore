using SportsStore.Controller.CRUD;
using SportsStore.Enums;

namespace SportsStore.Controller
{
    public class StockEntityHandler : EntityHandler, IEntityHandalable
    {
        private readonly LogEntityHandler Logger = LogEntityHandler.Logger;

        //Create
        public void Add(params string[] args)
        {
            ItemCreator creator = args[5] switch
            {
                "Ball" => new BallCreator(),
                "Bat" => new BatCreator(),
                "Net" => new NetCreator(),
                "Clothe" => new ClotheCreator(),
                _ => throw new NotImplementedException()
            };
            Db.Stocks.Add(creator.CreateItem(args[0], float.Parse(args[1]), args[2], args[3], args[4]));
            Logger.Add(new string[] { LoggedInUser.Id.ToString(), $"Added Item: {args[0]}", ActionTypes.AddStock.ToString() });
            Db.SaveChanges();
        }
        public void Generate()
        {
            Db.Stocks.Add(new BallCreator().CreateItem("Ronaldo Ball", 99.9f, ColorTypes.White.ToString(), BallTypes.Soccer.ToString(), string.Empty));
            Db.Stocks.Add(new BallCreator().CreateItem("Jordan Ball", 99.9f, ColorTypes.Red.ToString(), BallTypes.BasketBall.ToString(), string.Empty));
            Db.Stocks.Add(new BallCreator().CreateItem("Ping-Pong Ball", 15, ColorTypes.White.ToString(), BallTypes.PingPongBall.ToString(), string.Empty));
            Db.Stocks.Add(new BallCreator().CreateItem("Ping-Pong Ball", 15, ColorTypes.Yellow.ToString(), BallTypes.PingPongBall.ToString(), string.Empty));

            Db.Stocks.Add(new BatCreator().CreateItem("Al Bundy Baseball Bat", 150f, string.Empty, BatTypes.BaseBall.ToString(), string.Empty));
            Db.Stocks.Add(new BatCreator().CreateItem("Premium Golf Bat", 267f, string.Empty, BatTypes.Golf.ToString(), string.Empty));

            Db.Stocks.Add(new NetCreator().CreateItem("Ping-Pong Net", 59.9f, string.Empty, NetTypes.PingPong.ToString(), string.Empty));
            Db.Stocks.Add(new NetCreator().CreateItem("Tennis Net", 350f, string.Empty, NetTypes.Tennis.ToString(), string.Empty));

            Db.Stocks.Add(new ClotheCreator().CreateItem("X Shirt", 199.9f, ColorTypes.Black.ToString(), ClotheTypes.Shirt.ToString(), ShirtSizeTypes.Medium.ToString()));
            Db.Stocks.Add(new ClotheCreator().CreateItem("X Shirt", 199.9f, ColorTypes.Black.ToString(), ClotheTypes.Shirt.ToString(), ShirtSizeTypes.Large.ToString()));
            Db.Stocks.Add(new ClotheCreator().CreateItem("X Pants", 65f, ColorTypes.Red.ToString(), ClotheTypes.Shirt.ToString(), "32"));
            Db.Stocks.Add(new ClotheCreator().CreateItem("X Pants", 65f, ColorTypes.Red.ToString(), ClotheTypes.Shirt.ToString(), "28"));
            Db.Stocks.Add(new ClotheCreator().CreateItem("Messi Shoes", 65f, ColorTypes.Yellow.ToString(), ClotheTypes.Shoes.ToString(), "42"));

            Db.Stocks.AddGenerateRecordsLog(Db, Logger);
            Db.SaveChanges();
        }
        public void AddSearchLog() => Db.Stocks.AddSearchLog(Db, Logger);

        // Read
        public IEnumerable<object> GetTable(string s, params string[] args) => s switch
        {
            "ByType" => from stock in Db.Stocks
                        where stock.ItemType == args[0]
                        select stock,

            "ByColor" => from stock in Db.Stocks
                         where stock.Color == args[0]
                         select stock,

            "BySize" => from stock in Db.Stocks
                        where stock.Size == args[0]
                        select stock,

            "ByPrice" => from stock in Db.Stocks
                         where stock.ItemType == args[0] && stock.Price > Convert.ToInt16(args[1]) && stock.Price < Convert.ToInt32(args[2])
                         select stock,

            "ByInnerType" => from stock in Db.Stocks
                             where stock.ItemType == args[0] && stock.ItemInnerType == args[1]
                             select stock,

            "ByType_Price" => from stock in Db.Stocks
                              where stock.ItemType == args[0] &&
                                    stock.Price > Convert.ToInt16(args[1]) &&
                                    stock.Price < Convert.ToInt32(args[2])
                              select stock,

            "ByType_InnerType_Color" => from stock in Db.Stocks
                                        where stock.ItemType == args[0] && stock.ItemInnerType == args[1] && stock.Color == args[2]
                                        select stock,

            "ByType_InnerType_Size" => from stock in Db.Stocks
                                       where stock.ItemType == args[0] && stock.ItemInnerType == args[1] && stock.Size == args[2]
                                       select stock,

            "ByType_InnerType_Price" => from stock in Db.Stocks
                                        where stock.ItemType == args[0] &&
                                              stock.ItemInnerType == args[1] &&
                                              stock.Price > Convert.ToInt16(args[2]) &&
                                              stock.Price < Convert.ToInt32(args[3])
                                        select stock,

            "ByAll_NoPrice" => from stock in Db.Stocks
                               where stock.ItemType == args[0] &&
                                     stock.ItemInnerType == args[1] &&
                                     stock.Color == args[2] &&
                                     stock.Size == args[3]
                               select stock,

            "ByAll" => from stock in Db.Stocks
                       where stock.ItemType == args[0] &&
                             stock.ItemInnerType == args[1] &&
                             stock.Color == args[2] &&
                             stock.Size == args[3] &&
                             stock.Price > Convert.ToInt16(args[4]) &&
                             stock.Price < Convert.ToInt32(args[5])
                       select stock,

            _ => Db.Stocks
        };
        public IEnumerable<object> Search(string s, string arg = "") =>
            from stocks in Db.Stocks
            where (stocks.Name).Contains(arg)
            select stocks;
        public List<string> GetList(string s, params string[] args) =>
            throw new NotImplementedException();
        public List<string> GetStats() =>
            throw new NotImplementedException();

        //Update
        public bool Update(string s, params string[] args)
        {
            try
            {
                switch (s)
                {
                    case "Name":
                        {
                            Db.Stocks.Where(x => x.Id == Convert.ToInt16(args[0])).Select(x => x).ToList()[0].Name = args[1];
                            break;
                        }
                    case "Price":
                        {
                            Db.Stocks.Where(x => x.Id == Convert.ToInt16(args[0])).Select(x => x).ToList()[0].Price = Convert.ToInt16(args[2]);
                            break;
                        }
                    case "Quantity":
                        {
                            Db.Stocks.Where(x => x.Id == Convert.ToInt16(args[0])).Select(x => x).ToList()[0].Quantity = Convert.ToInt16(args[3]);
                            break;
                        }
                    case "ItemType":
                        {
                            Db.Stocks.Where(x => x.Id == Convert.ToInt16(args[0])).Select(x => x).ToList()[0].ItemType = args[4];
                            break;
                        }
                    case "ItemInnerType":
                        {
                            Db.Stocks.Where(x => x.Id == Convert.ToInt16(args[0])).Select(x => x).ToList()[0].ItemInnerType = args[5];
                            break;
                        }
                    case "Color":
                        {
                            Db.Stocks.Where(x => x.Id == Convert.ToInt16(args[0])).Select(x => x).ToList()[0].Color = args[6];
                            break;
                        }
                    case "Size":
                        {
                            Db.Stocks.Where(x => x.Id == Convert.ToInt16(args[0])).Select(x => x).ToList()[0].Size = args[7];
                            break;
                        }
                }
                Db.Stocks.AddUpdateLog(Db, Logger);
                Db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Delete
        public void Remove(int id)
        {
            Db.Stocks.Remove(Db.Stocks.Single(stock => stock.Id == id));
            Db.Stocks.AddRemoveRecordLog(Db, Logger, id);
            Db.SaveChanges();
        }
        public void Clear()
        {
            Db.Stocks.Clear(Db);
            Db.Stocks.AddClearRowsLog(Db, Logger);
        }
    }
}

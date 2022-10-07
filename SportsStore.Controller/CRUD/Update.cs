using SportsStore.Enums;
using SportsStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Controller
{
    public class Update: DbCrud, IDbUpdatable
    {
        private readonly StoreContext? db;

        public Update()
        {
            db = IDbConnectable.Db;
        }

        // Public Methods for editing selected user details
        public void UserType(int id, string type)
        {
            db.Users.Single(user => user.Id == id).UserType = (UserTypes)Enum.Parse(typeof(UserTypes), type);
            db.SaveChanges();
        }
        public void UserEmail(int id, string email)
        {
            db.Users.Single(user => user.Id == id).Email = email;
            db.SaveChanges();
        }
        public void UserHireDate(int id, string date)
        {
            db.Users.Single(user => user.Id == id).HireDate = Convert.ToDateTime(date).Date;
            db.SaveChanges();
        }
        public void UserPassword(int id, string password)
        {
            db.Users.Single(user => user.Id == id).Password = password;
            db.SaveChanges();
        }

        // Public Methods for editing selected customer details
        public void CustomerFirstName(int id, string fName)
        {
            db.customers.Single(customer => customer.Id == id).FirstName = fName;
            db.SaveChanges();
        }
        public void CustomerLastName(int id, string lName)
        {
            db.customers.Single(customer => customer.Id == id).LastName = lName;
            db.SaveChanges();
        }
        public void CustomerEmail(int id, string email)
        {
            db.customers.Single(customer => customer.Id == id).Email = email;
            db.SaveChanges();
        }
        public void CustomerDOB(int id, string dob)
        {
            db.customers.Single(customer => customer.Id == id).DateOfBirth = Convert.ToDateTime(dob);
            db.SaveChanges();
        }

        // Public Methods for editing selected stock details
        public bool EditStock(int itemId, string name = "", string price = "", string quantity = "",
                              string itemType = "", string innerItemType = "", string color = "",
                              string size = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    db.Stocks.Where(x => x.Item.Id == Convert.ToInt16(itemId)).Select(x => x.Item).ToList()[0].Name = name;
                }
                if (!string.IsNullOrEmpty(price))
                {
                    db.Stocks.Where(x => x.Item.Id == Convert.ToInt16(itemId)).Select(x => x.Item).ToList()[0].Price = Convert.ToInt16(price);
                }
                if (!string.IsNullOrEmpty(quantity))
                {
                    db.Stocks.Where(x => x.Item.Id == Convert.ToInt16(itemId)).Select(x => x).ToList()[0].Quantity = Convert.ToInt16(quantity);
                }
                if (!string.IsNullOrEmpty(itemType))
                {
                    db.Stocks.Where(x => x.Item.Id == Convert.ToInt16(itemId)).Select(x => x.Item).ToList()[0].ItemType = itemType;
                }
                if (!string.IsNullOrEmpty(innerItemType))
                {
                    db.Stocks.Where(x => x.Item.Id == Convert.ToInt16(itemId)).Select(x => x.Item).ToList()[0].ItemInnerType = innerItemType;
                }
                if (!string.IsNullOrEmpty(color))
                {
                    db.Stocks.Where(x => x.Item.Id == Convert.ToInt16(itemId)).Select(x => x.Item).ToList()[0].Color = color;
                }
                if (!string.IsNullOrEmpty(size))
                {
                    db.Stocks.Where(x => x.Item.Id == Convert.ToInt16(itemId)).Select(x => x.Item).ToList()[0].Size = size;
                }
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
using SportsStore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Controller
{
    public interface IDbCreatable : IDbConnectable
    {
        void AddLog(int id, string description, ActionTypes actionType);

        bool AddStock(int itemId, int quantity);
        bool AddSale(int itemId, int quantity, int customerID);
        bool AddItem(string itemType, string name, double price, string color, string itemInnerType, string size = "");
        bool AddNewUser(string firstName, string lastName, UserTypes userType, string email, string password);
        bool AddCustomer(string firstName, string lastName, string email, DateTime dob);
    }
}

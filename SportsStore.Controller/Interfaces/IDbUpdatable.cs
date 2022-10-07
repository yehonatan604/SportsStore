using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Controller
{
    public interface IDbUpdatable : IDbConnectable
    {
        void UserType(int id, string type);
        void UserEmail(int id, string email);
        void UserHireDate(int id, string date);
        void UserPassword(int id, string password);

        void CustomerFirstName(int id, string fName);
        void CustomerLastName(int id, string lName);
        void CustomerEmail(int id, string email);
        void CustomerDOB(int id, string dob);

        bool EditStock(int itemId, string name = "", string price = "", string quantity = "",
                       string itemType = "", string innerItemType = "", string color = "", string size = "");
    }
}

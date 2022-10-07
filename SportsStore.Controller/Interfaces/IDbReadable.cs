using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Controller
{ 
    public interface IDbReadable : IDbConnectable
    {
        bool CheckLastSessionExit();
        bool CheckAvailability(string email);
        bool CheckLogin(string email, string password);
        int CheckAuthorizationLevel();

        string GetUserType(int id);

        IEnumerable<object> GetTable(string s = "*");

        IEnumerable<object> GetStock(string s, string arg1);
        IEnumerable<object> GetCustomers(string arg1, string arg2, string arg3, string arg4, string arg5, string arg6);
        IEnumerable<object> GetSales(string arg1, string arg2);
        IEnumerable<object> GetLogs(string s, string arg1);

        List<string> GetList(string s);

        List<string> GetSalesStats();
        List<string> GetCustomerStats();

        IEnumerable<object> ItemSearch(string str);
        IEnumerable<object> CustomerSearch(string str);
        IEnumerable<object> UsersSearchName(string str);
        IEnumerable<object> UsersSearchId(string str);
        IEnumerable<object> LogsSearchId(string str);
        IEnumerable<object> LogsSearchName(string str);
    }
}

using SportsStore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Controller
{
    public interface IEntityHandalable : IDbConnectable
    {
        //Create
        void Add(params string[] args);
        void Generate();

        //Delete
        void Clear();
        void Remove(int id);

        //Read
        IEnumerable<object> GetTable(string s, params string[] args);
        List<string> GetList(string s, params string[] args);
        List<string> GetStats();
        IEnumerable<object> Search(string s, string arg = "");
        
        //Update
        bool Update(string s, params string[] args);

        //Checks
        bool CheckLastSessionExit();
        bool CheckAvailability(string email);
        bool CheckLogin(string email, string password);
        bool CheckLoggedIns();
        int CheckAuthorizationLevel();

        //On Start/End Handlers
        void OnStartProgram();
        void OnExitProgram();
    }
}

using SportsStore.Controller.CRUD;
using SportsStore.Enums;
using SportsStore.Model;
using SportsStore.Model.Costumers;
using SportsStore.Model.Items;
using SportsStore.Model.Users;
using SportStore.Controller.DbConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Controller
{
    public interface IDbConnectable
    {
        static StoreContext? Db { get ; set; }
        public int NoUserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Controller
{
    public interface IDbDeletable : IDbConnectable
    {
        void ClearLogs();
        void ClearLoggedIns();
        void RemoveUser(int id);
    }
}

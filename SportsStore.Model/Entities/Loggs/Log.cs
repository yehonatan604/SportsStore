using SportsStore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Model.Users
{
    public class Log
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public ActionTypes ActionType { get; set; }
    }
    public class LogsView
    {
        public int Id { get; set; }
        public int LogId { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string ActionType { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserType { get; set; }
    }
}

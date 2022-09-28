using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Model.Users
{
    public class Logged
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime DateTime { get; set; }
    }
}

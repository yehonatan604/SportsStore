using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Model.Users
{
    public class MessageBox
    {
        public int Id { get; set; }
        public User User { get; set; }
        public List<Message> Messages { get; set; }
        public byte Status { get; set; }
    }
}




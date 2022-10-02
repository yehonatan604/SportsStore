using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Model.Users
{
    public class Message
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string RecipentId { get; set; }
        public string RecipentFName { get; set; }
        public string RecipentLName { get; set; }
        public string SenderId { get; set; }
        public string SenderFname { get; set; }
        public string SenderLname { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public byte Status { get; set; }
    }
}

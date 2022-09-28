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
        public string Recipent { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public byte Status { get; set; }

        public Message(string title, string content, string recipent, string sender)
        {
            Title = title;
            Content = content;
            Date = DateTime.Now;
            Status = byte.MinValue;
            Recipent = recipent;
            Sender = sender;
        }
    }
}

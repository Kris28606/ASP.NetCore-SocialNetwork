using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Message : MyEntity
    {
        public int MessageId { get; set; }
        public int FromId { get; set; }
        public User FromUser { get; set; }
        public int ForId { get; set; }
        public User ForUser { get; set; }
        public String MessageText { get; set; }
        public DateTime Time { get; set; }
    }
}

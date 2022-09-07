using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Notification
    {
        public int FromWhoId { get; set; }
        public User FromWho { get; set; }
        public int ForWhoId { get; set; }
        public User ForWho { get; set; }
        public DateTime Date { get; set; }
    }
}

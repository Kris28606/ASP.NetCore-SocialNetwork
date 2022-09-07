using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CommentNotification : Notification
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public String Comment { get; set; }
    }
}

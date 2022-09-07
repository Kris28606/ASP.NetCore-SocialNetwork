using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LikeNotification : Notification
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public enum FollowStatus
    {
        Waiting,
        Confirmed,
        Rejected
    }

    public class FollowNotification : Notification
    {
        public FollowStatus Status { get; set; }
    }
}

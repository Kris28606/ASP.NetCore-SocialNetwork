using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class FollowNotificationDto : MyDto
    {
        public int FromWhoId { get; set; }
        public string FromWhoUsername { get; set; }
        public string FromWhoPicture { get; set; }
        public DateTime Date { get; set; }
    }
}

using SocialNetwork.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class LikeNotificationDto : MyDto
    {
        public UserDto FromWho { get; set; }
        public PostResponse Post { get; set; }
        public DateTime Date { get; set; }
    }
}

using SocialNetwork.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class CommentNotificationDto : MyDto
    {
        public String FromWhoUsername { get; set; }
        public int FromWhoId { get; set; }
        public String FromWhoPicture { get; set; }
        public int PostId { get; set; }
        public String PostPicture { get; set; }
        public DateTime Date { get; set; }
        public String Comment { get; set; }
    }
}

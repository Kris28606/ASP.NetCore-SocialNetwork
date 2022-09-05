using SocialNetwork.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class CommentResponse : MyDto
    {
        public UserDto User { get; set; }
        public int PostId { get; set; }
        public String CommentText { get; set; }
    }
}

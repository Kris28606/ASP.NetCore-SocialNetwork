using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public class Comment
    {
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public String CommentText { get; set; }
    }
}

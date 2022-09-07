using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public class Post : MyEntity
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public DateTime Date { get; set; }
        public String Description { get; set; }
        public String ImagePath { get; set; }
        public List<Reaction> Reactions { get; set; }
        public List<Comment> Comments { get; set; }
        [JsonIgnore]
        public List<LikeNotification> LikeNotifications { get; set; }
        [JsonIgnore]
        public List<CommentNotification> CommentNotifications { get; set; }
    }
}

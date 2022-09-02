using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum ReactionType
    {
        LIKE,
        DISLIKE
    }


    public class Reaction
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public ReactionType ReactionType { get; set; }
    }
}

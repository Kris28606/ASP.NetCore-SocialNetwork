using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public class Reaction
    {
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public int PostId { get; set; }
        [JsonIgnore]
        public Post Post { get; set; }
    }
}

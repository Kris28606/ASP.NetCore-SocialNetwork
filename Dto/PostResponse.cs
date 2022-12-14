using Domain;
using Dto;

namespace SocialNetwork.Dto
{
    public class PostResponse : MyDto
    {
        public int PostId { get; set; }
        public String Description { get; set; }
        public DateTime Datum { get; set; }
        public  String Ago { get; set; }
        public int UserId { get; set; }
        public String Username { get; set; }
        public String Name { get; set; }
        public String Picture { get; set; }
        public int NumberOfLikes { get; set; }
        public bool ILiked { get; set; } = false;
        public int NumberOfComments { get; set; }
    }
}

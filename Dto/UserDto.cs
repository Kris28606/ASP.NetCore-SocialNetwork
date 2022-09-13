using Dto;

namespace SocialNetwork.Dto
{
    public class UserDto : MyDto
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Username { get; set; }
        public String Token { get; set; }
        public String Email { get; set; }
        public String ProfilePicture { get; set; }
        public bool FollowingMe { get; set; } = false;
        public bool IFollow { get; set; } = false;
        public bool RequestSent { get; set; } = false;
    }
}

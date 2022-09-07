using Dto;

namespace SocialNetwork.Dto
{
    public class RegisterDto : MyDto
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String ProfilePicture { get; set; }

    }
}

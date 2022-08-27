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
    }
}

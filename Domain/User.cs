using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
    public class User : IdentityUser<int>, MyEntity
    {
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        public String ProfilePicture { get; set; }

        public List<Post> Posts { get; set; }

        public List<User> Followers { get; set; }
        public List<User> Following { get; set; }
        
        //public List<Reaction> Reactions { get; set; }
        [JsonIgnore]
        public List<Message> Received { get; set; }
        [JsonIgnore]
        public List<Message> Send { get; set; }
    }
}
using Dto;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Dto
{
    public class PostRequest : MyDto
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public String Description { get; set; }
    }
}

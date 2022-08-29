using Dto;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocialNetwork.Dto
{
    
    public class PostRequest : MyDto
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public String Description { get; set; }
        
        public String Picture { get; set; } 
        
    }
}

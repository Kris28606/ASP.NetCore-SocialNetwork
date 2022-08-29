using Domain;
using SocialNetwork.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class PostResponseMapper : GenericMapper<PostResponse, Post>
    {
        public PostResponse toDto(Post entity)
        {
            PostResponse pr = new PostResponse();
            pr.Description = entity.Description;
            pr.Datum = entity.Date;
            pr.Username = entity.User.UserName;
            pr.Name = entity.User.FirstName + " " + entity.User.LastName;
            pr.PostId = entity.PostId;
            DateTime now = DateTime.Now;
            TimeSpan days = now - pr.Datum;
            int minuti = (int)days.TotalMinutes;
            if(minuti<60)
            {
                pr.Ago = minuti + " minutes ago";
            } else if(minuti<=1440)
            {
                pr.Ago = minuti / 60 + " hours ago";
            } else
            {
                pr.Ago = minuti / 1440 + " days ago";
            }
            pr.Picture = entity.ImagePath;
            return pr;
        }

        public Post toEntity(PostResponse dto)
        {
            throw new NotImplementedException();
        }
    }
}

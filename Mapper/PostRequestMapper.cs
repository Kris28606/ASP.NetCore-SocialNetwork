using Domain;
using SocialNetwork.Dto;

namespace Mapper
{
    public class PostRequestMapper : GenericMapper<PostRequest, Post>
    {
        public PostRequest toDto(Post entity)
        {
            PostRequest pr = new PostRequest();
            pr.Description=entity.Description;
            pr.Date = entity.Date;
            pr.UserId = entity.UserId;
            pr.Id = entity.PostId;
            return pr;
        }

        public Post toEntity(PostRequest dto)
        {
            Post p = new Post();
            p.Description = dto.Description;
            p.Date = dto.Date;
            p.UserId = dto.UserId;
            p.PostId = dto.Id;
            return p;
        }
    }
}
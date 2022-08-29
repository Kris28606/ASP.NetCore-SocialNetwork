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
            pr.UserId = entity.UserId;
            pr.Id = entity.PostId;
            pr.Picture = entity.ImagePath;
            return pr;
        }

        public Post toEntity(PostRequest dto)
        {
            Post p = new Post();
            p.Description = dto.Description;
            p.UserId = dto.UserId;
            p.PostId = dto.Id;
            p.ImagePath = dto.Picture;
            return p;
        }
    }
}
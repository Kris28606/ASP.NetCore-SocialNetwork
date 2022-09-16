using BusinesLogicLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Domain;
using Mapper;
using SocialNetwork.Dto;

namespace BusinesLogicLayer.Implementation
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork unit;
        private readonly PostResponseMapper responseMapper;
        private readonly PostRequestMapper requestMapper;
        private readonly UserMapper userMapper;
        private readonly CommentResponseMapper commentMapper;

        public PostService(IUnitOfWork unit)
        {
            this.unit = unit;
            this.responseMapper = new PostResponseMapper();
            this.requestMapper = new PostRequestMapper();
            this.userMapper = new UserMapper();
            this.commentMapper = new CommentResponseMapper();
        }

        public bool Create(PostRequest entity)
        {
            User u = new User() { Id = entity.UserId };
            u = unit.UserRepository.SearchById(u);
            if (u != null)
            {
                try
                {
                    Post p = requestMapper.toEntity(entity);
                    DateTime now = DateTime.Now;
                    p.Date = now;
                    unit.PostRepository.Add(p);
                    unit.Save();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            };
            return false;
        }

        public List<PostResponse> GetAllMyPosts(int userId, string username)
        {
            try
            {
                List<Post> posts=unit.PostRepository.GetMyPosts(userId);
                User u = unit.UserRepository.SearchByUsername(new User { UserName = username });
                List<PostResponse> responses = new List<PostResponse>();
                posts.ForEach(p =>
                {
                    PostResponse response = responseMapper.toDto(p);
                    p.Reactions.ForEach(r =>
                    {
                        if (r.UserId == u.Id)
                        {
                            response.ILiked = true;
                        }
                    });
                    response.NumberOfLikes = p.Reactions.Count();
                    response.NumberOfComments = p.Comments.Count();
                    responses.Add(response);
                });
                return responses;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<PostResponse> GetAllForHome(int i, int numOfPosts)
        {
            int skip = numOfPosts / 2;
            skip = (skip - 1) * 2;
            List<Post> posts=unit.PostRepository.GetAllForHome(i, skip, 2);
            List<PostResponse> response = new List<PostResponse>();
            foreach(Post p in posts)
            {
                PostResponse pr = responseMapper.toDto(p);
                p.Reactions.ForEach(r =>
                {
                   if (r.UserId == i)
                   {
                       pr.ILiked = true;
                   }
                });
                pr.NumberOfLikes = p.Reactions.Count();
                pr.NumberOfComments = p.Comments.Count();
                response.Add(pr);
            }
            return response;
        }
    }
}

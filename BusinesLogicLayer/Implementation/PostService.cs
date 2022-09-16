using BusinesLogicLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Domain;
using Dto;
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

        public bool LikeIt(int postId, string username)
        {
            User u = new User { UserName = username };
            u= unit.UserRepository.SearchByUsername(u);
            unit.PostRepository.LikeIt(postId, u.Id);
            unit.Save();
            return true;
        }

        public bool UnlikeIt(int postId, string username)
        {
            User u = new User { UserName = username };
            u = unit.UserRepository.SearchByUsername(u);
            bool result=unit.PostRepository.UnlikeIt(postId, u.Id);
            if(result)
            {
                unit.Save();
                return true;
            }
            return false;
        }

        public List<UserDto> GetLikes(int postId, string user)
        {
            List<User> users = unit.PostRepository.GetLikes(postId);
            List<UserDto> usersDto = new List<UserDto>();
            users.ForEach(u =>
            {
                UserDto dto = userMapper.toDto(u);
                User ja = unit.UserRepository.SearchByUsername(new User { UserName=user});
                Notification not = new Notification
                {
                    ForWhoId = dto.Id,
                    FromWhoId = ja.Id
                };
                bool exist = unit.FollowNotificationRepository.ExistActiveFollow(not);
                if(exist)
                {
                    dto.RequestSent = true;
                }
                User pronadjen = u.Followers.Find(u => u.UserName == user);
                if (pronadjen != null) dto.IFollow = true;
                User pronadjen2 = u.Following.Find(u => u.UserName == user);
                if (pronadjen2 != null) dto.FollowingMe = true;
                usersDto.Add(dto);
            });
            return usersDto;
        }

        public List<CommentResponse> GetComments(int postId)
        {
            List<Comment> comments = unit.PostRepository.GetComments(postId);
            List<CommentResponse> commentsDto = new List<CommentResponse>();
            comments.ForEach(c =>
            {
                commentsDto.Add(commentMapper.toDto(c));
            });
            return commentsDto;
        }

        public CommentResponse PostComment(CommentRequest dto)
        {
            User u = new User { UserName=dto.Username };
            u = unit.UserRepository.SearchByUsername(u);
            Post p = new Post { PostId = dto.PostId };
            p = unit.PostRepository.SearchById(p);
            if(p.UserId==u.Id)
            {
                return null; 
            }
            Comment c = new Comment();
            c.CommentText = dto.CommentText;
            c.PostId = dto.PostId;
            c.User = u;
            c.DatumVreme = DateTime.Now;
            Comment result=unit.PostRepository.PostComment(c);
            if (result!=null)
            {
                unit.Save();
                return commentMapper.toDto(c);
            }
            return null;
        }
    }
}

using Domain;
using Dto;
using SocialNetwork.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.Interfaces
{
    public interface IPostService : IService<PostRequest>
    {
        public List<PostResponse> GetAllMyPosts(int userId);
        public List<PostResponse> GetAllForHome(int i);
        public bool LikeIt(int postId, string username);
        public bool UnlikeIt(int postId, string username);
        public List<UserDto> GetLikes(int postId,string user);
        public List<CommentResponse> GetComments(int postId);
        public CommentResponse PostComment(CommentRequest dto);
    }
}

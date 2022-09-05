using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        public List<Post> GetAllForHome(int id, int skip, int take);
        public List<Post> GetMyPosts(int id);
        public bool LikeIt(int postId, int userId);
        public bool UnlikeIt(int postId, int userId);
        public List<User> GetLikes(int postId);
        public List<Comment> GetComments(int postId);
        public Comment PostComment(Comment c);
    }
}

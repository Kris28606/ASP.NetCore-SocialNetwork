using DataAccessLayer.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Implementation
{
    public class PostRepository : IPostRepository
    {
        private readonly UserContext context;

        public PostRepository(UserContext context)
        {
            this.context = context;
        }

        public void Add(Post entity)
        {
            context.Add(entity);
        }

        public void Delete(Post entity)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Post> GetAllForHome(int i, int skip, int take)
        {
            User user = context.Users.Include(u=> u.Following).SingleOrDefault(u => u.Id == i);
            List<Post> posts = new List<Post>();
            user.Following.ForEach(u => {
                List<Post> p = context.Posts.Include(p => p.Reactions).Include(p=>p.Comments).Where(m => m.UserId == u.Id).ToList();
                posts.AddRange(p);
            });
            posts=posts.OrderByDescending(s => s.Date).ToList();
            return posts.Skip(skip).Take(take).ToList();

        }

        public List<Comment> GetComments(int postId)
        {
            List<Comment> comments= context.Comments.Include(c => c.User).Where(c => c.PostId == postId).ToList();
            comments = comments.OrderBy(s => s.DatumVreme).ToList();
            return comments;
        }

        public List<User> GetLikes(int postId)
        {
            Post p=context.Posts.Include(p => p.Reactions).SingleOrDefault(p => p.PostId == postId);
            List<User> users = new List<User>();
            p.Reactions.ForEach(r =>
            {
                User u = context.Users.Include(m=>m.Followers).Include(m=>m.Following).SingleOrDefault(u => u.Id == r.UserId);
                users.Add(u);
            });
            return users;
        }

        public List<Post> GetMyPosts(int id)
        {
            List<Post> posts=context.Posts.Include(p => p.Reactions).Include(p=> p.User).Include(p => p.Comments).Where(p => p.UserId == id).ToList();
            posts= posts.OrderByDescending(s => s.Date).ToList();
            return posts;
        }

        public bool LikeIt(int postId, int userId)
        {
            Reaction r = new Reaction
            {
                PostId = postId,
                UserId = userId
            };
            context.Reactions.Add(r);
            return true;
        }

        public Comment PostComment(Comment c)
        {
            try
            {
                return context.Comments.Add(c).Entity;
            } catch(Exception ex)
            {
                return null;
            }
            
        }

        public Post SearchById(Post entity)
        {
            return context.Posts.SingleOrDefault(s => s.PostId == entity.PostId);
        }

        public bool UnlikeIt(int postId, int userId)
        {
            Reaction r = context.Reactions.SingleOrDefault(r => r.UserId == userId && r.PostId == postId);
            if(r!=null)
            {
                context.Reactions.Remove(r);
                return true;
            }
            return false;
        }

        public void Update(Post entity)
        {
            throw new NotImplementedException();
        }
    }
}

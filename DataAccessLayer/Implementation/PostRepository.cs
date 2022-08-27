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

        public List<Post> GetAllForUser(Post entity)
        {
            User u = context.Users.SingleOrDefault(u => u.Id == entity.UserId);
            if(u==null)
            {
                throw new Exception("Ne postoji user.");
            }
            return u.Posts;
        }

        public Post SearchById(Post entity)
        {
            return context.Posts.SingleOrDefault(s => s.PostId == entity.PostId);
        }

        public void Update(Post entity)
        {
            throw new NotImplementedException();
        }
    }
}

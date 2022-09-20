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
    public class ReactionRepository : IReactionRepository
    {
        private readonly UserContext context;

        public ReactionRepository(UserContext context)
        {
            this.context = context;
        }

        public void Add(Reaction entity)
        {
            context.Add(entity);
        }

        public List<Reaction> GetAll()
        {
            throw new NotImplementedException();
        }

        public Reaction SearchById(Reaction entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Reaction entity)
        {
            throw new NotImplementedException();
        }

        public List<User> GetLikes(int postId)
        {
            Post p = context.Posts.Include(p => p.Reactions).SingleOrDefault(p => p.PostId == postId);
            List<User> users = new List<User>();
            p.Reactions.ForEach(r =>
            {
                User u = context.Users.Include(m => m.Followers).Include(m => m.Following).SingleOrDefault(u => u.Id == r.UserId);
                users.Add(u);
            });
            return users;
        }

        public bool UnlikeIt(Reaction r)
        {
            r = context.Reactions.SingleOrDefault(re => re.UserId == r.UserId && re.PostId == r.PostId);
            if (r != null)
            {
                context.Reactions.Remove(r);
                return true;
            }
            return false;
        }

        public void Delete(Reaction entity)
        {
            throw new NotImplementedException();
        }
    }
}

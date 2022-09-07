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
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;

        public UserRepository(UserContext context)
        {
            this.context = context;
        }

        public void Add(User entity)
        {
            context.Users.Add(entity);
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }

        public List<User> Search(string kriterijum, int id)
        {
            return context.Users.Include(u=>u.Following).Include(m=> m.Followers).Where(u => u.FirstName.Contains(kriterijum) && u.Id!=id).ToList();
        }

        public User SearchById(User entity)
        {
            return context.Users.Include(u=>u.Posts).SingleOrDefault(s => s.Id == entity.Id);
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public bool ChangePicture(User user)
        {
            User u = SearchById(user);
            if (u == null)
            {
                return false;
            }
            u.ProfilePicture = user.ProfilePicture;
            context.Users.Update(u);
            return true;
        }

        public User SearchByUsername(User user)
        {
            return context.Users.SingleOrDefault(u => u.UserName == user.UserName);
        }

        public bool Unfollow(string username, int id)
        {
            User user=context.Users.Include(u => u.Following).SingleOrDefault(u => u.UserName == username);
            User unfollowed = user.Following.SingleOrDefault(u => u.Id == id);
            if(unfollowed!=null)
            {
                user.Following.Remove(unfollowed);
                return true;
            }
            return false;
        }

        public bool Follow(string username, int id)
        {
            User user = context.Users.Include(u => u.Following).SingleOrDefault(u => u.UserName == username);
            User followed = user.Following.SingleOrDefault(u => u.Id == id);
            if (followed != null)
            {
                user.Following.Add(followed);
                return true;
            }
            return false;
        }
    }
}

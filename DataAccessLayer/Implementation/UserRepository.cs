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
            return context.Users.Include(u => u.Following).Include(m => m.Followers).Where(u => u.FirstName.Contains(kriterijum) && u.Id != id).ToList();
        }

        public User SearchById(User entity)
        {
            return context.Users.Include(u => u.Posts).Include(u => u.Followers).Include(u => u.Following).SingleOrDefault(s => s.Id == entity.Id);
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
            return context.Users.Include(u => u.Followers).Include(u => u.Following).SingleOrDefault(u => u.UserName == user.UserName);
        }

        public bool Unfollow(string username, int id)
        {
            User user = context.Users.Include(u => u.Following).SingleOrDefault(u => u.UserName == username);
            User unfollowed = user.Following.SingleOrDefault(u => u.Id == id);
            if (unfollowed != null)
            {
                user.Following.Remove(unfollowed);
                return true;
            }
            return false;
        }

        public bool AddFollower(int userId, int followId)
        {
            User user = context.Users.Include(u => u.Followers).SingleOrDefault(u => u.Id == userId);
            User followed = context.Users.SingleOrDefault(u => u.Id == followId);
            if (followed != null)
            {
                user.Followers.Add(followed);
                return true;
            }
            return false;
        }

        public List<User> GetRandomUsers(User entity)
        {
            List<User> users = new List<User>();
            List<User> allUsers = context.Users.Include(u => u.Followers).Include(u => u.Following).Where(u => u.Id != entity.Id).ToList();
            if (allUsers.Count == 1) return users;
            allUsers.ForEach(u =>
            {
                if (u.Followers.Find(u => u.Id == entity.Id) == null)
                {
                    users.Add(u);
                }
            });
            allUsers = users;
            users = new List<User>();
            if (allUsers.Count > 3)
            {
                Random ran = new Random();
                for (int i = 0; i < 3; i++)
                {
                    int broj = ran.Next(0, allUsers.Count);

                    if (users.Find(u => u.Id == allUsers[broj].Id) == null) {

                        users.Add(allUsers[broj]);
                        allUsers.Remove(allUsers[broj]);
                    } else
                    {
                        i--;
                    }
                }
            } else
            {
                return allUsers;
            }
            return users;
        }
    }
}

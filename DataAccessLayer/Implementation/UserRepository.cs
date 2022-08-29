using DataAccessLayer.Interfaces;
using Domain;
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

        public List<User> Search(string kriterijum)
        {
            return context.Users.Where(u => u.FirstName.Contains(kriterijum)).ToList();
        }

        public User SearchById(User entity)
        {
            return context.Users.SingleOrDefault(s => s.Id == entity.Id);
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}

using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public List<User> Search(string kriterijum, int id);
        public bool ChangePicture(User user);

        public User SearchById(User entity);

        public User SearchByUsername(User user);
        public bool Unfollow(string username, int id);
        public bool AddFollower(int userId, int followId);
        public List<User> GetRandomUsers(User entity);
    }
}

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
        public List<User> Search(string kriterijum);
        public bool ChangePicture(User user);

        public User SearchByUsername(User user);
        public List<User> GetInboxUsers(int userId);

    }
}

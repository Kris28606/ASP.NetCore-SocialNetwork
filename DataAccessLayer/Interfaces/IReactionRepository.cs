using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IReactionRepository : IRepository<Reaction>
    {
        public List<User> GetLikes(int postId);
        public bool UnlikeIt(Reaction r);
    }
}

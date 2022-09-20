using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        public List<Comment> GetComments(int postId);
        public Comment PostComment(Comment c);
    }
}

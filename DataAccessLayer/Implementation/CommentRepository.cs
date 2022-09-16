using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Implementation
{
    public class CommentRepository : ICommentRepository
    {
        private readonly UserContext context;

        public CommentRepository(UserContext context)
        {
            this.context = context;
        }
        public void Add(Comment entity)
        {
            context.Add(entity);
        }

        public void Delete(Comment entity)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Comment SearchById(Comment entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}

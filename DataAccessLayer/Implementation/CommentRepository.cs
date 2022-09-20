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

        public List<Comment> GetComments(int postId)
        {
            List<Comment> comments = context.Comments.Include(c => c.User).Where(c => c.PostId == postId).ToList();
            comments = comments.OrderBy(s => s.DatumVreme).ToList();
            return comments;
        }

        public Comment PostComment(Comment c)
        {
            try
            {
                return context.Comments.Add(c).Entity;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}

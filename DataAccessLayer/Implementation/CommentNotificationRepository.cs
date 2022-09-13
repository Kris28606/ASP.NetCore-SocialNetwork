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
    public class CommentNotificationRepository : ICommentNotificationRepository
    {
        private readonly UserContext context;

        public CommentNotificationRepository(UserContext context)
        {
            this.context = context;
        }
        public void Add(Notification entity)
        {
            context.Add(entity);
        }

        public void Delete(Notification entity)
        {
            throw new NotImplementedException();
        }

        public List<Notification> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Notification> GetAllForUser(User u)
        {
            List<Notification> list=context.CommentNotification.Include(c=> c.FromWho).Include(c=> c.Post).Where(n => n.ForWhoId == u.Id).ToList().OfType<Notification>().ToList();
            list= list.OrderByDescending(l => l.Date).ToList();
            return list;
        }

        public Notification SearchById(Notification entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Notification entity)
        {
            throw new NotImplementedException();
        }
    }
}

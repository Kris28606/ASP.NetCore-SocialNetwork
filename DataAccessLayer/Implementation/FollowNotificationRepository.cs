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
    public class FollowNotificationRepository : IFollowNotificationRepository
    {
        private readonly UserContext context;

        public FollowNotificationRepository(UserContext context)
        {
            this.context = context;
        }
        public void Add(Notification entity)
        {
            context.Add(entity);
        }

        public void ConfirmFollow(int userId, int followId)
        {
            FollowNotification not = context.FollowNotifications.SingleOrDefault(n => n.FromWhoId == followId && n.ForWhoId == userId);
            not.Status = FollowStatus.Confirmed;
            not.Date = DateTime.Now;
            context.Update(not);
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
            List<Notification> list= context.FollowNotifications.Include(n=> n.FromWho).Where(n => n.ForWhoId == u.Id).ToList().OfType<Notification>().ToList();
            list = list.OrderByDescending(l => l.Date).ToList();
            return list;
        }

        public Notification SearchById(Notification entity)
        {
            return context.FollowNotifications.SingleOrDefault(n => n.FromWhoId == entity.FromWhoId && n.ForWhoId == entity.ForWhoId);
        }

        public void Update(Notification entity)
        {
            throw new NotImplementedException();
        }
    }
}

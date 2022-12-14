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
            FollowNotification not = (FollowNotification)entity;
            context.Add(not);
        }

        public void Delete(Notification entity)
        {
            FollowNotification not=context.FollowNotifications.SingleOrDefault(n => n.FromWhoId == entity.FromWhoId && n.ForWhoId == entity.ForWhoId);
            context.Remove(not);
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

        public bool ExistActiveFollow(Notification entity)
        {
            List<FollowNotification> not = context.FollowNotifications.Where(n => n.FromWhoId == entity.FromWhoId && n.ForWhoId == entity.ForWhoId).ToList();
            bool postoji = false;
            not.ForEach(n =>
            {
                if (n.Status == FollowStatus.Waiting)
                {
                    postoji = true;
                }
            });
            return postoji;
        }
    }
}

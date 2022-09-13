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
    public class LikeNotificationRepository : ILikeNotificationsRepository
    {
        private readonly UserContext context;

        public LikeNotificationRepository(UserContext context)
        {
            this.context = context;
        }
        public void Add(Notification entity)
        {
            context.Add(entity);
        }

        public void Delete(Notification entity)
        {
            LikeNotification like = (LikeNotification)entity;
            LikeNotification get = context.LikeNotifications.SingleOrDefault(l => l.FromWhoId == like.FromWhoId && l.ForWhoId == like.ForWhoId && l.PostId == like.PostId);
            if (get != null)
            {
                context.LikeNotifications.Remove(get);
            }
        }

        public List<Notification> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Notification> GetAllForUser(User u)
        {
            List<Notification> list= context.LikeNotifications.Include(n=> n.FromWho).Include(n=>n.Post).Where(n => n.ForWhoId == u.Id).ToList().OfType<Notification>().ToList();
            list = list.OrderByDescending(l => l.Date).ToList();
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

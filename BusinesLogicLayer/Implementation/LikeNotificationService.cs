using BusinesLogicLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.Implementation
{
    public class LikeNotificationService : ILikeNotificationService
    {
        private readonly IUnitOfWork unit;

        public LikeNotificationService(UserContext context)
        {
            this.unit = new DataAccessLayer.UnitOfWork.UnitOfWork(context);
        }

        public bool Create(Notification entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteLikeNotification(int postId, string username)
        {
            LikeNotification not = new LikeNotification();
            User fromUser = new User { UserName = username };
            fromUser = unit.UserRepository.SearchByUsername(fromUser);
            Post likedPost = new Post { PostId = postId };
            likedPost = unit.PostRepository.SearchById(likedPost);
            not.FromWhoId = fromUser.Id;
            not.ForWhoId = likedPost.UserId;
            not.PostId = postId;
            unit.LikeNotificationsRepository.Delete(not);
            unit.Save();
        }

        public void SendLikeNotification(int postId, string username)
        {
            LikeNotification not = new LikeNotification();
            User fromUser = new User { UserName = username };
            fromUser = unit.UserRepository.SearchByUsername(fromUser);
            not.FromWhoId=fromUser.Id;
            not.PostId = postId;
            not.Date = DateTime.Now;
            Post likedPost = new Post { PostId = postId };
            likedPost = unit.PostRepository.SearchById(likedPost);
            not.ForWhoId = likedPost.UserId;
            unit.LikeNotificationsRepository.Add(not);
            unit.Save();
        }
    }
}

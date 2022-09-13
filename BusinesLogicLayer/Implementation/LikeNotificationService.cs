using BusinesLogicLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Domain;
using Dto;
using Mapper;
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
        private LikeNotificationMapper mapper;

        public LikeNotificationService(UserContext context)
        {
            this.unit = new DataAccessLayer.UnitOfWork.UnitOfWork(context);
            mapper = new LikeNotificationMapper();
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
            if (fromUser.Id == likedPost.UserId)
            {
                return;
            }
            not.FromWhoId = fromUser.Id;
            not.ForWhoId = likedPost.UserId;
            not.PostId = postId;
            unit.LikeNotificationsRepository.Delete(not);
            unit.Save();
        }

        public List<LikeNotificationDto> GetAllForUser(int id)
        {
            User u = new User { Id = id };
            List<LikeNotification> notif = unit.LikeNotificationsRepository.GetAllForUser(u).OfType<LikeNotification>().ToList();
            List<LikeNotificationDto> notifDto = new List<LikeNotificationDto>();
            notif.ForEach(n =>
            {
                notifDto.Add(mapper.toDto(n));
            });
            return notifDto;
        }

        public LikeNotificationDto SendLikeNotification(int postId, string username)
        {
            LikeNotification not = new LikeNotification();
            User fromUser = new User { UserName = username };
            fromUser = unit.UserRepository.SearchByUsername(fromUser);
            not.FromWhoId=fromUser.Id;
            not.PostId = postId;
            not.Date = DateTime.Now;
            Post likedPost = new Post { PostId = postId };
            likedPost = unit.PostRepository.SearchById(likedPost);
            if(fromUser.Id==likedPost.UserId)
            {
                return null;
            }
            not.ForWhoId = likedPost.UserId;
            unit.LikeNotificationsRepository.Add(not);
            unit.Save();
            return mapper.toDto(not);
        }
    }
}

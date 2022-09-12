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
    public class FollowNotificationService : IFollowNotificationService
    {
        private readonly IUnitOfWork unit;
        private FollowNotificationMapper mapper;

        public FollowNotificationService(UserContext context)
        {
            unit = new DataAccessLayer.UnitOfWork.UnitOfWork(context);
            mapper = new FollowNotificationMapper();
        }

        public void ConfirmFollow(int userId, int followId)
        {
            unit.FollowNotificationRepository.ConfirmFollow(userId, followId);
            unit.Save();
        }

        public bool Create(Notification entity)
        {
            throw new NotImplementedException();
        }

        public void CreateFollow(int id, string username)
        {
            User following = new User { UserName = username };
            User followed = new User { Id = id };
            following=unit.UserRepository.SearchByUsername(following);
            followed = unit.UserRepository.SearchById(followed);
            FollowNotification not = new FollowNotification { 
                Date=DateTime.Now,
                ForWho=followed,
                FromWho=following,
                Status=FollowStatus.Waiting
            };
            unit.LikeNotificationsRepository.Add(not);
            unit.Save();
        }

        public List<FollowNotificationDto> GetAllForUser(int id)
        {
            User u = new User { Id = id };
            List<FollowNotification> notif = unit.FollowNotificationRepository.GetAllForUser(u).OfType<FollowNotification>().ToList();
            List<FollowNotificationDto> notifDto = new List<FollowNotificationDto>();
            notif.ForEach(n =>
            {
                notifDto.Add(mapper.toDto(n));
            });
            return notifDto;
        }
    }
}

using BusinesLogicLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Domain;
using Dto;
using Mapper;

namespace BusinesLogicLayer.Implementation
{
    public class FollowNotificationService : IFollowNotificationService
    {
        private readonly IUnitOfWork unit;
        private FollowNotificationMapper mapper;

        public FollowNotificationService(IUnitOfWork unit)
        {
            this.unit = unit;
            mapper = new FollowNotificationMapper();
        }

        public void ConfirmFollow(int userId, int followId)
        {
            unit.FollowNotificationRepository.Delete(new FollowNotification { FromWhoId = followId, ForWhoId = userId });
            FollowNotification not = new FollowNotification
            {
                FromWhoId = followId,
                ForWhoId = userId,
                Status = FollowStatus.Confirmed,
                Date = DateTime.Now
            };
            unit.FollowNotificationRepository.Add(not);
            unit.Save();
        }

        public bool Create(Notification entity)
        {
            throw new NotImplementedException();
        }

        public FollowNotificationDto CreateFollow(int id, string username)
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
            unit.FollowNotificationRepository.Add(not);
            unit.Save();
            return mapper.toDto(not);
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

        public FollowNotificationDto SearchActiveFollow(int forId, int fromId)
        {
            FollowNotification not = new FollowNotification { 
                ForWhoId=forId,
                FromWhoId=fromId
            };

            not=(FollowNotification)unit.FollowNotificationRepository.SearchById(not);
            if(not.Status==FollowStatus.Waiting)
            {
                return mapper.toDto(not);
            } else
            {
                return null;
            }
        }
    }
}

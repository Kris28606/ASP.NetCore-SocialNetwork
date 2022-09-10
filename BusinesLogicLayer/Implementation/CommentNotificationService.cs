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
    public class CommentNotificationService : ICommentNotificationService
    {
        private readonly IUnitOfWork unit;
        private CommentNotificationMapper mapper;

        public CommentNotificationService(UserContext context)
        {
            unit = new DataAccessLayer.UnitOfWork.UnitOfWork(context);
            mapper = new CommentNotificationMapper();
        }
        public bool Create(Notification entity)
        {
            throw new NotImplementedException();
        }

        public List<CommentNotificationDto> GetAllForUser(int id)
        {
            User u = new User { Id = id };
            List<CommentNotification> notif = unit.CommentNotificationRepository.GetAllForUser(u).OfType<CommentNotification>().ToList();
            List<CommentNotificationDto> notifDto = new List<CommentNotificationDto>();
            notif.ForEach(n =>
            {
                notifDto.Add(mapper.toDto(n));
            });
            return notifDto;
        }

        public void SendCommentNotification(CommentResponse comment)
        {
            CommentNotification not = new CommentNotification();
            not.FromWhoId = comment.User.Id;
            not.PostId = comment.PostId;
            Post nov = new Post { PostId = comment.PostId };
            nov = unit.PostRepository.SearchById(nov);
            not.ForWhoId = nov.UserId;
            not.Comment = comment.CommentText;
            not.Date = DateTime.Now;
            unit.CommentNotificationRepository.Add(not);
            unit.Save();
        }
    }
}

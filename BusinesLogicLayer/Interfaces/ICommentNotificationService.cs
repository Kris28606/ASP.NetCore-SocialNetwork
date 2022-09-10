using Domain;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.Interfaces
{
    public interface ICommentNotificationService : INotificationService
    {
        public void SendCommentNotification(CommentResponse comment);
        public List<CommentNotificationDto> GetAllForUser(int id);
    }
}

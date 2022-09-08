using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.Interfaces
{
    public interface ILikeNotificationService : INotificationService
    {
        public void SendLikeNotification(int postId, string username);
        public void DeleteLikeNotification(int postId, string username);
        public List<LikeNotificationDto> GetAllForUser(int id);
    }
}

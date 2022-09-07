using BusinesLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        public IPostService PostService { get; set; }
        public IUserService UserService { get; set; }
        public IMessageService MessageService { get; set; }
        public INotificationService NotificationService { get; set; }
        public ILikeNotificationService LikeNotificationService { get; set; }
        public ICommentNotificationService CommentNotificationService { get; set; }
        public IFollowNotificationService FollowNotificationService { get; set; }
    }
}

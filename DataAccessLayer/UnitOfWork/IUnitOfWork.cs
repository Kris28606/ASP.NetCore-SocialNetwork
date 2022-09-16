using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; set; }
        public IPostRepository PostRepository { get; set; }
        public IMessageRepository MessageRepository { get; set; }
        public INotificationRepository NotificationRepository { get; set; }
        public ILikeNotificationsRepository LikeNotificationsRepository { get; set; }
        public ICommentNotificationRepository CommentNotificationRepository { get; set; }
        public IFollowNotificationRepository FollowNotificationRepository { get; set; }
        public IReactionRepository ReactionRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }
        public void Save();
    }
}

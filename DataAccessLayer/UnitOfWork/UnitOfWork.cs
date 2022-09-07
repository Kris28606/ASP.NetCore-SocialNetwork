using DataAccessLayer.Implementation;
using DataAccessLayer.Interfaces;
using Domain;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserContext context;

        public UnitOfWork(UserContext context)
        {
            this.UserRepository = new UserRepository(context);
            this.PostRepository = new PostRepository(context);
            this.MessageRepository = new MessageRepository(context);
            this.NotificationRepository = new NotificationRepository(context);
            this.LikeNotificationsRepository = new LikeNotificationRepository(context);
            this.CommentNotificationRepository = new CommentNotificationRepository(context);
            this.FollowNotificationRepository = new FollowNotificationRepository(context);
            this.context = context;
        }

        public IUserRepository UserRepository { get; set; }
        public IPostRepository PostRepository { get; set; }
        public IMessageRepository MessageRepository { get; set; }
        public INotificationRepository NotificationRepository { get; set; }
        public ILikeNotificationsRepository LikeNotificationsRepository { get; set; }
        public ICommentNotificationRepository CommentNotificationRepository { get; set; }
        public IFollowNotificationRepository FollowNotificationRepository { get; set; }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
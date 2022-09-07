using BusinesLogicLayer.Implementation;
using BusinesLogicLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Domain;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.UnitOfWork
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        public UnitOfWorkService(UserContext context)
        {
            PostService = new PostService(context);
            UserService = new UserService(context);
            MessageService = new MessageService(context);
            LikeNotificationService = new LikeNotificationService(context);
            CommentNotificationService= new CommentNotificationService(context);
            FollowNotificationService = new FollowNotificationService(context);
            NotificationService=new NotificationService(context);
        }
        public IPostService PostService { get; set; }
        public IUserService UserService { get; set; }
        public IMessageService MessageService { get; set; }
        public INotificationService NotificationService { get; set; }
        public ILikeNotificationService LikeNotificationService { get; set; }
        public ICommentNotificationService CommentNotificationService { get; set; }
        public IFollowNotificationService FollowNotificationService { get; set; }
        
    }
}

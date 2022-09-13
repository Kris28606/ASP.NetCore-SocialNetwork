using Dto;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Dto;
using SocialNetwork.HubModel;
using WebApi2.Auth;

namespace SocialNetwork.HubConfig
{
    public class MyHub : Hub
    {
        private static List<UserInfo> usersChat = new List<UserInfo>();
        private static List<UserInfo> usersNotification = new List<UserInfo>();

        //public async Task LogIn(LoginDto dto)
        //{
        //    UserDto user = await jwt.AuthentificationAsync(dto.Username, dto.Password);
        //    if (user != null)
        //    {
        //        UserInfo u = new UserInfo();
        //        u.ConnectionId = Context.ConnectionId;
        //        u.UserId = user.Id;
        //        u.Username = user.Username;
        //        users.Add(u);

        //        await Clients.Client(Context.ConnectionId).SendAsync("logInSuccessful", user);
        //    } else
        //    {
        //        await Clients.Client(Context.ConnectionId).SendAsync("logInFail");
        //    }
            
        //}

        public async Task Chat(UserDto user)
        {
            UserInfo uInfo = new UserInfo
            {
                ConnectionId = Context.ConnectionId,
                UserId = user.Id,
                Username = user.Username
            };
            bool postoji = false;
            usersChat.ForEach(u =>
            {
                if (u.UserId == user.Id)
                {
                    u.ConnectionId = uInfo.ConnectionId;
                    postoji = true;
                }
            });
            if(!postoji)
            {
                usersChat.Add(uInfo);
            }
        }

        public async Task SendMessage(MessageDto message)
        {
            try
            {
                UserInfo u = usersChat.SingleOrDefault(u => u.UserId == message.ForId);
                if(u!=null)
                {
                    await Clients.Client(u.ConnectionId).SendAsync("receivedMessage", message);
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        public async Task Notifications(UserDto dto)
        {
            UserInfo uInfo = new UserInfo
            {
                ConnectionId = Context.ConnectionId,
                UserId = dto.Id,
                Username = dto.Username
            };
            bool postoji = false;
            usersNotification.ForEach(u =>
            {
                if (u.UserId == dto.Id)
                {
                    u.ConnectionId = uInfo.ConnectionId;
                    postoji = true;
                }
            });
            if (!postoji)
            {
                usersNotification.Add(uInfo);
            }
        }

        public async Task SendLikeNotification(LikeNotificationDto notification, int id)
        {
            UserInfo u = usersNotification.SingleOrDefault(u => u.UserId == id);
            if (u != null)
            {
                await Clients.Client(u.ConnectionId).SendAsync("likeNotification", notification);
            }
        }

        public async Task SendCommentNotification(CommentNotificationDto notification, int id)
        {
            UserInfo u = usersNotification.SingleOrDefault(u => u.UserId == id);
            if (u != null)
            {
                await Clients.Client(u.ConnectionId).SendAsync("commentNotification", notification);
            }
        }

        public async Task SendFollowNotification(FollowNotificationDto notification, int id)
        {
            UserInfo u = usersNotification.SingleOrDefault(u => u.UserId == id);
            if (u != null)
            {
                await Clients.Client(u.ConnectionId).SendAsync("followNotification", notification);
            }
        }
    }
}

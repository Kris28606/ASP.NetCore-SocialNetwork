using Domain;
using Dto;
using SocialNetwork.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.Interfaces
{
    public interface IMessageService : IService<Message>
    {
        public List<UserDto> GetInboxUsers(int userId);
        public List<Message> GetChat(int fromId, int forId);
        public MessageDto SendMessage(MessageDto mess);
    }
}

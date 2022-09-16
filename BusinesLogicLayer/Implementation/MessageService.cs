using BusinesLogicLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Domain;
using Dto;
using Mapper;
using SocialNetwork.Dto;

namespace BusinesLogicLayer.Implementation
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork unit;
        private UserMapper userMapper;
        private MessageMapper messageMapper;

        public MessageService(IUnitOfWork unit)
        {
            this.unit = unit;
            userMapper = new UserMapper();
            messageMapper=new MessageMapper();
        }
        public bool Create(Message entity)
        {
            throw new NotImplementedException();
        }

        public List<Message> GetChat(int fromId, int forId)
        {
            return unit.MessageRepository.GetChat(forId, fromId);
        }

        public List<UserDto> GetInboxUsers(int userId)
        {
            List<User> users= unit.MessageRepository.GetInboxUsers(userId);
            List<UserDto> usersDto = new List<UserDto>();
            if(users.Count()==0)
            {
                return usersDto;
            }
            users.ForEach(u =>
            {
                usersDto.Add(userMapper.toDto(u));
            });
            return usersDto;
        }

        public MessageDto SendMessage(MessageDto mess)
        {
            Message m = messageMapper.toEntity(mess);
            m = unit.MessageRepository.Send(m);
            if(m!=null)
            {
                unit.Save();
                return messageMapper.toDto(m);
            } else
            {
                return null;
            }
        }
    }
}

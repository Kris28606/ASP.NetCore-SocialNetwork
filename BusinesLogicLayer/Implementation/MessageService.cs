using BusinesLogicLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Domain;
using Mapper;
using SocialNetwork.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.Implementation
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork unit;
        private UserMapper userMapper;

        public MessageService(UserContext context)
        {
            this.unit = new DataAccessLayer.UnitOfWork.UnitOfWork(context);
            userMapper = new UserMapper();
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
    }
}

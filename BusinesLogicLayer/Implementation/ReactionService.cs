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
    public class ReactionService : IReactionService
    {
        private readonly IUnitOfWork unit;
        private UserMapper userMapper;

        public ReactionService(IUnitOfWork unit)
        {
            this.unit = unit;
            userMapper = new UserMapper();
        }

        public bool Create(UserDto entity)
        {
            throw new NotImplementedException();
        }

        public List<UserDto> GetLikes(int postId, string username)
        {
            List<User> users = unit.PostRepository.GetLikes(postId);
            List<UserDto> usersDto = new List<UserDto>();
            users.ForEach(u =>
            {
                UserDto dto = userMapper.toDto(u);
                User ja = unit.UserRepository.SearchByUsername(new User { UserName = username });
                Notification not = new Notification
                {
                    ForWhoId = dto.Id,
                    FromWhoId = ja.Id
                };
                bool exist = unit.FollowNotificationRepository.ExistActiveFollow(not);
                if (exist)
                {
                    dto.RequestSent = true;
                }
                User pronadjen = u.Followers.Find(u => u.UserName == username);
                if (pronadjen != null) dto.IFollow = true;
                User pronadjen2 = u.Following.Find(u => u.UserName == username);
                if (pronadjen2 != null) dto.FollowingMe = true;
                usersDto.Add(dto);
            });
            return usersDto;
        }

        public void LikeIt(int postId, string username)
        {
            User u = new User { UserName = username };
            u = unit.UserRepository.SearchByUsername(u);
            Reaction r = new Reaction
            {
                PostId = postId,
                UserId = u.Id
            };
            unit.ReactionRepository.Add(r);
            unit.Save();
        }

        public bool UnlikeIt(int postId, string username)
        {
            User u = new User { UserName = username };
            u = unit.UserRepository.SearchByUsername(u);
            bool result = unit.PostRepository.UnlikeIt(postId, u.Id);
            if (result)
            {
                unit.Save();
                return true;
            }
            return false;
        }
    }
}

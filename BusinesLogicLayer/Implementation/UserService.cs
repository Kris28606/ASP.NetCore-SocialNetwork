using BusinesLogicLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Domain;
using SocialNetwork.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapper;
using Dto;

namespace BusinesLogicLayer.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unit;
        private readonly UserMapper mapper;
        public UserService(UserContext context)
        {
            this.unit = new DataAccessLayer.UnitOfWork.UnitOfWork(context);
            mapper = new UserMapper();
        }

        public bool Create(UserDto entity)
        {
            throw new NotImplementedException();
        }

        public List<UserDto> Search(string kriterijum, int id)
        {
            List<User> users=unit.UserRepository.Search(kriterijum, id);
            List<UserDto> usersDto = new List<UserDto>();
            if(users.Count()==0)
            {
                return usersDto;
            }
            // da li ih ja pratim
            // da li oni mene prate
            users.ForEach(u => {
                UserDto dto = mapper.toDto(u);
                User pronadjen=u.Followers.Find(u => u.Id == id);
                if(pronadjen!=null)  dto.IFollow = true;
                User pronadjen2 = u.Following.Find(u => u.Id == id);
                if (pronadjen2 != null) dto.FollowingMe = true;
                usersDto.Add(dto);
            });
            return usersDto;
        }

        public UserDto UcitajUsera(int id, string username)
        {
            User u = new User { Id = id };
            User ja = new User { UserName = username };
            ja = unit.UserRepository.SearchByUsername(ja);
            u=unit.UserRepository.SearchById(u);
            FollowNotification not = (FollowNotification)unit.FollowNotificationRepository.SearchById(new FollowNotification { FromWhoId = ja.Id, ForWhoId = id });
            if(u!=null)
            {
               UserDto user=mapper.toDto(u);
                ja.Followers.ForEach(f =>
                {
                    if (f.Id == u.Id)
                    {
                        user.FollowingMe = true;
                    }
                });
                ja.Following.ForEach(f =>
                {
                    if (f.Id == u.Id)
                    {
                        user.IFollow = true;
                    }
                });
                if(not.Status==FollowStatus.Waiting)
                {
                    user.RequestSent = true;
                }
                return user;
            }
            return null;
        }

        public bool ChangePicture(UserDto user)
        {
            User u = mapper.toEntity(user);
            if (u.ProfilePicture == null || u.ProfilePicture == "")
            {
                return false;
            }
            bool result = unit.UserRepository.ChangePicture(u);
            if (result)
            {
                unit.Save();
            }
            return result;
        }

        public UserDto UcitajUseraByUsername(string username)
        {
            User u = new User { UserName = username };
            u = unit.UserRepository.SearchByUsername(u);
            if (u != null)
            {
                return mapper.toDto(u);
            }
            return null;
        }

        public bool Unfollow(string username, int id)
        {
            bool result=unit.UserRepository.Unfollow(username, id);
            if(result)
            {
                unit.Save();
            }
            return result;
        }

        public bool AddFollower(int userId, int followId)
        {
            bool result = unit.UserRepository.AddFollower(userId, followId);
            if(result)
            {
                unit.Save();
            }
            return result;
        }
    }
}

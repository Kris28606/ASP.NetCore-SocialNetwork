using BusinesLogicLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Domain;
using SocialNetwork.Dto;
using Mapper;

namespace BusinesLogicLayer.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unit;
        private readonly UserMapper mapper;
        public UserService(IUnitOfWork unit)
        {
            this.unit = unit;
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
                Notification not = new Notification { FromWhoId = id, ForWhoId = dto.Id };
                bool exist = unit.FollowNotificationRepository.ExistActiveFollow(not);
                if(exist)
                {
                    dto.RequestSent = true;
                }
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
            if(u!=null)
            {
               bool not = unit.FollowNotificationRepository.ExistActiveFollow(new Notification { FromWhoId = ja.Id, ForWhoId = id });
               UserDto user=mapper.toDto(u);
                if(not)
                {
                    user.RequestSent = true;
                }
                ja.Followers.ForEach(f =>
                {
                    if (f.Id == u.Id)
                    {
                        user.FollowingMe = true;
                    }
                });
                if (!user.RequestSent)
                {
                    ja.Following.ForEach(f =>
                    {
                        if (f.Id == u.Id)
                        {
                            user.IFollow = true;
                        }
                    });
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

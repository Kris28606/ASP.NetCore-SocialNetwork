using Domain;
using SocialNetwork.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class UserMapper : GenericMapper<UserDto, User>
    {
        public UserDto toDto(User entity)
        {
            UserDto dto = new UserDto();
            dto.FirstName = entity.FirstName;
            dto.LastName = entity.LastName;
            dto.Username = entity.UserName;
            dto.Id = entity.Id;
            dto.Email = entity.Email;
            return dto;
        }

        public User toEntity(UserDto dto)
        {
            User u = new User();
            u.FirstName = dto.FirstName;
            u.LastName = dto.LastName;
            u.Email = dto.Email;
            u.Id = dto.Id;
            u.UserName = dto.Username;
            return u;
        }
    }
}

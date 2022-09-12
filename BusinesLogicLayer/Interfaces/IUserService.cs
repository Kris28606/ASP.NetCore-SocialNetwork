using SocialNetwork.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.Interfaces
{
    public interface IUserService : IService<UserDto>
    {
        public UserDto UcitajUsera(int id, string username);
        public List<UserDto> Search(string kriterijum, int id);
        public bool ChangePicture(UserDto user);
        public UserDto UcitajUseraByUsername(String username);
        public bool Unfollow(String username, int id);
        public bool AddFollower(int userId, int followId);
    }
}

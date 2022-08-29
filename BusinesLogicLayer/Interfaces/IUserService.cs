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
        public UserDto UcitajUsera(int id);
        public List<UserDto> Search(string kriterijum);
    }
}

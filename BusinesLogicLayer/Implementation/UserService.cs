using BusinesLogicLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Domain;
using SocialNetwork.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.UnitOfWork;
using Mapper;

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

        public List<UserDto> Search(string kriterijum)
        {
            List<User> users=unit.UserRepository.Search(kriterijum);
            List<UserDto> usersDto = new List<UserDto>();
            if(users.Count()==0)
            {
                return usersDto;
            }
            users.ForEach(u => usersDto.Add(mapper.toDto(u)));
            return usersDto;
        }

        public UserDto UcitajUsera(int id)
        {
            User u = new User { Id = id };
            u=unit.UserRepository.SearchById(u);
            if(u!=null)
            {
                return mapper.toDto(u);
            }
            return null;
        }
    }
}

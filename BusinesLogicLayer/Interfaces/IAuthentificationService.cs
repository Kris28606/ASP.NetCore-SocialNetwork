using Microsoft.AspNetCore.Identity;
using SocialNetwork.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.Interfaces
{
    public interface IAuthentificationService
    {
        public Task<UserDto> LogIn(LoginDto dto);
        public Task<IdentityResult> Register(RegisterDto dto);
    }
}

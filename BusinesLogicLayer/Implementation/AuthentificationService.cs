using BusinesLogicLayer.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;
using SocialNetwork.Dto;
using WebApi2.Auth;

namespace BusinesLogicLayer.Implementation
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly JwtAuthentification jwt;
        private readonly UserManager<User> manager;

        public AuthentificationService(JwtAuthentification jwt, UserManager<User> manager)
        {
            this.jwt = jwt;
            this.manager = manager;
        }

        public async Task<UserDto> LogIn(LoginDto dto)
        {
            return await jwt.AuthentificationAsync(dto.Username, dto.Password);
        }

        public async Task<IdentityResult> Register(RegisterDto dto)
        {
            var newUser = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Username,
                ProfilePicture = dto.ProfilePicture
            };

            return await manager.CreateAsync(newUser, dto.Password);
        }
    }
}

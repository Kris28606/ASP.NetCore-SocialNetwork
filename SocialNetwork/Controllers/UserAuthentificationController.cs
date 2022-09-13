using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dto;
using WebApi2.Auth;

namespace SocialNetwork.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserAuthentificationController : ControllerBase
    {
        private readonly JwtAuthentification jwt;
        private readonly UserManager<User> manager;

        public UserAuthentificationController(JwtAuthentification jwt, UserManager<User> manager)
        {
            this.jwt = jwt;
            this.manager = manager;
        }


        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> LogIn([FromBody] LoginDto dto)
        {
            UserDto user = await jwt.AuthentificationAsync(dto.Username, dto.Password);
            if (user == null) return Unauthorized();
            return Ok(user);
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto dto)
        {
            var newUser = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Username,
                ProfilePicture = dto.ProfilePicture
            };

            var result = await manager.CreateAsync(newUser, dto.Password);
            if (result.Succeeded)
            {   
                return Ok();
            } else
            {
                return BadRequest("Neuspesno!"+result.Errors);
            }
        }
    }
}

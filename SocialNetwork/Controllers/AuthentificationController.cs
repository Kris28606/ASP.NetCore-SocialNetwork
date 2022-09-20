using BusinesLogicLayer.Interfaces;
using Domain;
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
        private readonly IAuthentificationService authService;
        private readonly UserManager<User> manager;

        public UserAuthentificationController(IAuthentificationService authService, UserManager<User> manager)
        {
            this.authService = authService;
            this.manager = manager;
        }


        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> LogIn([FromBody] LoginDto dto)
        {
            UserDto user = await authService.LogIn(dto);
            if (user == null) return Unauthorized();
            return Ok(user);
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto dto)
        {
            var result = await authService.Register(dto);
            if (result.Succeeded)
            {   
                return Ok();
            } else
            {
                return BadRequest("Neuspesno! ");
            }
        }
    }
}

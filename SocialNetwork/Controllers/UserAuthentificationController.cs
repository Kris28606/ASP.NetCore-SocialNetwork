using BusinesLogicLayer.Interfaces;
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
        private readonly IAuthentificationService authService;

        public UserAuthentificationController(IAuthentificationService authService)
        {
            this.authService = authService;
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
            var result=authService.Register(dto);
            if (result.IsCompletedSuccessfully)
            {   
                return Ok();
            } else
            {
                return BadRequest("Neuspesno!");
            }
        }
    }
}

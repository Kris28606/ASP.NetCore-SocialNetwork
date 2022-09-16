using BusinesLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dto;
using System.Text;

namespace SocialNetwork.Controllers
{
    [Route("user/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly IUnitOfWorkService unit;
        private readonly IUserService service;
        private readonly IPostService postService;
        private readonly IFollowNotificationService followService;

        public UserController(IUserService service, IPostService postService, IFollowNotificationService followService)
        {
            this.service = service;
            this.postService = postService;
            this.followService = followService;
        }

        [Authorize]
        [HttpGet]
        [Route("one/{id}/{username}")]
        public IActionResult GetUser([FromRoute(Name = "id")] int id, [FromRoute(Name = "username")] string username)
        {
            try
            {
                ///UserDto u = unit.UserService.UcitajUsera(id, username);
                UserDto u = service.UcitajUsera(id, username);
                if (u != null)
                {
                    return Ok(u);
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{username}")]
        public IActionResult GetUser([FromRoute(Name = "username")] string username)
        {
            try
            {
                UserDto u = service.UcitajUseraByUsername(username);
                if (u != null)
                {
                    return Ok(u);
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet]
        [Route("all/{id}/{username}")]
        public IActionResult GetAllMyPosts([FromRoute(Name = "id")] int id, [FromRoute(Name="username")] string username)
        {
            List<PostResponse> lista = postService.GetAllMyPosts(id, username);
            if (lista == null)
            {
                return BadRequest("Greska");
            }
            return Ok(lista);
        }

        [Authorize]
        [HttpPost]
        [Route("search/{id}")]
        public async Task<IActionResult> search([FromRoute(Name = "id")] int id)
        {
            var request = HttpContext.Request;
            var kriterijum = "";

            using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                kriterijum = await reader.ReadLineAsync();
            }
            kriterijum = kriterijum.Remove(0, 1);
            kriterijum = kriterijum.Remove(kriterijum.Length - 1);
            return Ok(service.Search(kriterijum, id));
        }

        [Authorize]
        [HttpPost]
        [Route("changePicture")]
        public IActionResult changeProfilePicture([FromBody] UserDto u)
        {
            try
            {
                if (service.ChangePicture(u))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("unfollow/{username}/{unfollowId}")]
        public IActionResult Unfollow([FromRoute(Name = "username")] string username, [FromRoute(Name = "unfollowId")] int id)
        {
            try
            {
                if (service.Unfollow(username, id))
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("follow/{userId}/{followId}")]
        public IActionResult Follow([FromRoute(Name = "userId")] int userId, [FromRoute(Name = "followId")] int followId)
        {
            try
            {
                if (service.AddFollower(userId, followId))
                {
                    followService.ConfirmFollow(userId, followId);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
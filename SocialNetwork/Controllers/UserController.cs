using BusinesLogicLayer.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dto;
using System.Text;

namespace SocialNetwork.Controllers
{
    [Route("user/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWorkService unit;

        public UserController(IUnitOfWorkService unit)
        {
            this.unit = unit;
        }

        [Authorize]
        [HttpGet]
        [Route("one/{id}/{username}")]
        public IActionResult GetUser([FromRoute(Name = "id")] int id, [FromRoute(Name = "username")] string username)
        {
            try
            {
                UserDto u = unit.UserService.UcitajUsera(id, username);
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
                UserDto u = unit.UserService.UcitajUseraByUsername(username);
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
            List<PostResponse> lista = unit.PostService.GetAllMyPosts(id, username);
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
            return Ok(unit.UserService.Search(kriterijum, id));
        }

        [Authorize]
        [HttpPost]
        [Route("changePicture")]
        public IActionResult changeProfilePicture([FromBody] UserDto u)
        {
            try
            {
                if (unit.UserService.ChangePicture(u))
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
                if (unit.UserService.Unfollow(username, id))
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
                if (unit.UserService.AddFollower(userId, followId))
                {
                    unit.FollowNotificationService.ConfirmFollow(userId, followId);
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
using BusinesLogicLayer.Interfaces;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dto;

namespace SocialNetwork.Controllers
{
    [Route("like/")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly IReactionService reactionService;
        private readonly ILikeNotificationService likeService;

        public LikeController(IReactionService reactionService, ILikeNotificationService likeService)
        {
            this.reactionService = reactionService;
            this.likeService = likeService;
        }

        [Authorize]
        [HttpPost]
        [Route("like/{postId}/{user}")]
        public IActionResult LikeIt([FromRoute(Name = "postId")] int postId, [FromRoute(Name = "user")] string user)
        {
            try
            {
                reactionService.LikeIt(postId, user);
                LikeNotificationDto dto = likeService.SendLikeNotification(postId, user);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpDelete]
        [Route("unlike/{postId}/{username}")]
        public IActionResult UnlikeIt([FromRoute(Name = "postId")] int postId, [FromRoute(Name = "username")] string username)
        {
            try
            {
                bool result = reactionService.UnlikeIt(postId, username);
                if (result)
                {
                    likeService.DeleteLikeNotification(postId, username);
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
        [HttpGet]
        [Route("likes/{postId}/{user}")]
        public IActionResult GetLikes([FromRoute(Name = "postId")] int postId, [FromRoute(Name = "user")] string user)
        {
            try
            {
                List<UserDto> users = reactionService.GetLikes(postId, user);
                if (users != null)
                {
                    return Ok(users);
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

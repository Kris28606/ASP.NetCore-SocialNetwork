using BusinesLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SocialNetwork.Controllers
{
    [Route("notification/")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ILikeNotificationService likeService;
        private readonly IFollowNotificationService followService;
        private readonly ICommentNotificationService commentService;

        public NotificationController(ILikeNotificationService likeService, IFollowNotificationService followService, ICommentNotificationService commentService)
        {
            this.likeService = likeService;
            this.followService = followService;
            this.commentService = commentService;
        }

        [Authorize]
        [HttpGet]
        [Route("like/{id}")]
        public IActionResult GetLikeNotifications([FromRoute(Name ="id")] int id)
        {
            return Ok(likeService.GetAllForUser(id));
        }

        [Authorize]
        [HttpGet]
        [Route("comment/{id}")]
        public IActionResult GetCommentNotifications([FromRoute(Name = "id")] int id)
        {
            return Ok(commentService.GetAllForUser(id));
        }

        [Authorize]
        [HttpGet]
        [Route("follow/{id}")]
        public IActionResult GetFollowNotifications([FromRoute(Name = "id")] int id)
        {
            return Ok(followService.GetAllForUser(id));
        }

        [Authorize]
        [HttpPost]
        [Route("follow/{id}/{username}")]
        public IActionResult CreateFollowNotification([FromRoute(Name="id")] int id, [FromRoute(Name ="username")] string username)
        {
            try
            {
                return Ok(followService.CreateFollow(id, username));
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}

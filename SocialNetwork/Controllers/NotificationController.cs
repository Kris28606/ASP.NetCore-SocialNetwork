using BusinesLogicLayer.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SocialNetwork.Controllers
{
    [Route("notification/")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IUnitOfWorkService unit;

        public NotificationController(IUnitOfWorkService unit)
        {
            this.unit = unit;
        }

        [Authorize]
        [HttpGet]
        [Route("like/{id}")]
        public IActionResult GetLikeNotifications([FromRoute(Name ="id")] int id)
        {
            return Ok(unit.LikeNotificationService.GetAllForUser(id));
        }

        [Authorize]
        [HttpGet]
        [Route("comment/{id}")]
        public IActionResult GetCommentNotifications([FromRoute(Name = "id")] int id)
        {
            return Ok(unit.CommentNotificationService.GetAllForUser(id));
        }

        [Authorize]
        [HttpGet]
        [Route("follow/{id}")]
        public IActionResult GetFollowNotifications([FromRoute(Name = "id")] int id)
        {
            return Ok(unit.FollowNotificationService.GetAllForUser(id));
        }

        [Authorize]
        [HttpPost]
        [Route("follow/{id}/{username}")]
        public IActionResult CreateFollowNotification([FromRoute(Name="id")] int id, [FromRoute(Name ="username")] string username)
        {
            try
            {
                return Ok(unit.FollowNotificationService.CreateFollow(id, username));
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}

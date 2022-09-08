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
    }
}

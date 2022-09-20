using BusinesLogicLayer.Interfaces;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SocialNetwork.Controllers
{
    [Route("comment/")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService service;
        private readonly ICommentNotificationService notifService;

        public CommentController(ICommentService service, ICommentNotificationService notifService)
        {
            this.service = service;
            this.notifService = notifService;
        }

        [Authorize]
        [HttpGet]
        [Route("comments/{postId}")]
        public IActionResult GetComments([FromRoute(Name = "postId")] int postId)
        {
            try
            {
                List<CommentResponse> comments = service.GetComments(postId);
                if (comments != null)
                {
                    return Ok(comments);
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
        [Route("comment")]
        public IActionResult PostComment([FromBody] CommentRequest com)
        {
            try
            {
                CommentResponse result = service.PostComment(com);
                if (result != null)
                {
                    notifService.SendCommentNotification(result);
                    return Ok(result);
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

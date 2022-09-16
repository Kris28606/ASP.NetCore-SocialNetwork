
using BusinesLogicLayer.Interfaces;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dto;

namespace SocialNetwork.Controllers
{
    [Route("post/")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;
        private readonly ILikeNotificationService likeService;
        private readonly ICommentNotificationService commentService;

        public PostController(IPostService postService, ILikeNotificationService likeService, ICommentNotificationService commentService)
        {
            this.postService = postService;
            this.likeService = likeService;
            this.commentService = commentService;
        }


        [Authorize]
        [HttpPost]
        [Route("new")]
        public IActionResult CreateNew([FromBody]PostRequest pr)
        {
            
            bool result = postService.Create(pr);
            if (result)
            {
                return Ok("Uspesno ste sacuvali post!");
            }
            return BadRequest("Post ne moze da se sacuva!");
        }

        [Authorize]
        [HttpGet]
        [Route("all/{id}/{numOfPosts}")]
        public IActionResult GetPostsForHomePage([FromRoute(Name = "id")] int id, [FromRoute(Name = "numOfPosts")] int numOfPosts)
        {
            List<PostResponse> result= postService.GetAllForHome(id, numOfPosts);
            return Ok(result);

        }

        [Authorize]
        [HttpGet]
        [Route("comments/{postId}")]
        public IActionResult GetComments([FromRoute(Name = "postId")] int postId)
        {
            try
            {
                List<CommentResponse> comments = postService.GetComments(postId);
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
                CommentResponse result = postService.PostComment(com);
                if(result!=null)
                {
                    commentService.SendCommentNotification(result);
                    return Ok(result);
                }
                return BadRequest();
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}


using BusinesLogicLayer.Interfaces;
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
        private readonly ICommentNotificationService commentService;

        public PostController(IPostService postService, ILikeNotificationService likeService, ICommentNotificationService commentService)
        {
            this.postService = postService;
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
    }
}

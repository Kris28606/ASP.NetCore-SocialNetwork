
using BusinesLogicLayer.UnitOfWork;
using Domain;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dto;

namespace SocialNetwork.Controllers
{
    [Route("post/")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IUnitOfWorkService unit;

        public PostController(IUnitOfWorkService unit)
        {
            this.unit = unit;
        }


        [Authorize]
        [HttpPost]
        [Route("new")]
        public IActionResult CreateNew([FromBody]PostRequest pr)
        {
            
            bool result = unit.PostService.Create(pr);
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
            List<PostResponse> result=unit.PostService.GetAllForHome(id, numOfPosts);
            return Ok(result);

        }

        [Authorize]
        [HttpPost]
        [Route("like/{postId}/{user}")]
        public IActionResult LikeIt([FromRoute(Name ="postId")] int postId, [FromRoute(Name ="user")] string user)
        {
            try
            {
                bool result = unit.PostService.LikeIt(postId, user);
                if(result)
                {
                    return Ok();
                }
                return BadRequest();
            } catch(Exception ex)
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
                bool result = unit.PostService.UnlikeIt(postId, username);
                if (result)
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
        [HttpGet]
        [Route("likes/{postId}/{user}")]
        public IActionResult GetLikes([FromRoute(Name ="postId")] int postId, [FromRoute(Name = "user")] string user)
        {
            try
            {
                List<UserDto> users = unit.PostService.GetLikes(postId, user);
                if(users!=null)
                {
                    return Ok(users);
                }
                return BadRequest();
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("comments/{postId}")]
        public IActionResult GetComments([FromRoute(Name = "postId")] int postId)
        {
            try
            {
                List<CommentResponse> comments = unit.PostService.GetComments(postId);
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
                CommentResponse result = unit.PostService.PostComment(com);
                if(result!=null)
                {
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

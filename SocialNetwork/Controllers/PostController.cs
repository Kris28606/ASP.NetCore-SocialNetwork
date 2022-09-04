﻿
using BusinesLogicLayer.UnitOfWork;
using Domain;
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
        [Route("all/{id}")]
        public IActionResult GetPostsForHomePage([FromRoute(Name = "id")] int id)
        {
            List<PostResponse> result=unit.PostService.GetAllForHome(id);
            return Ok(result);

        }

        //[Authorize]
        [HttpPost]
        [Route("like/{postId}/{username}")]
        public IActionResult LikeIt([FromRoute(Name ="postId")] int postId, [FromRoute(Name ="username")] string user)
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
    }
}


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
        private readonly UserManager<User> manager;
        private readonly IUnitOfWorkService unit;

        public PostController(UserManager<User> manager, IUnitOfWorkService unit)
        {
            this.manager = manager;
            this.unit = unit;
        }


        [Authorize]
        [HttpPost]
        [Route("new")]
        public IActionResult CreateNew([FromBody] PostRequest dto)
        {
            bool result=unit.PostService.Create(dto);
            if (result)
            {
                return Ok("Uspesno ste sacuvali post!");
            }
            return BadRequest("Post ne moze da se sacuva!");
        }

        [Authorize]
        [HttpGet]
        [Route("all/{id}")]
        public IActionResult GetAllMyPosts([FromRoute(Name ="id")] int id)
        {
            List<PostResponse> lista=unit.PostService.GetAllMyPosts(id);
            if(lista==null)
            {
                return BadRequest("Greska");
            }
            return Ok(lista);
        }

    }
}

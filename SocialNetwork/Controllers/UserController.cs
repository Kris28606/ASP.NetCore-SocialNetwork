using BusinesLogicLayer.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dto;

namespace SocialNetwork.Controllers
{
    [Route("user/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWorkService unit;

        public UserController(IUnitOfWorkService unit)
        {
            this.unit = unit;
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser([FromRoute(Name ="id")] int id)
        {
            try
            {
                UserDto u=unit.UserService.UcitajUsera(id);
                if(u!=null)
                {
                    return Ok(u);
                }
                return NotFound();

            } catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet]
        [Route("all/{id}")]
        public IActionResult GetAllMyPosts([FromRoute(Name = "id")] int id)
        {
            List<PostResponse> lista = unit.PostService.GetAllMyPosts(id);
            if (lista == null)
            {
                return BadRequest("Greska");
            }
            return Ok(lista);
        }
    }
}

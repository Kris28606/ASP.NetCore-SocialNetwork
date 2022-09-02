using BusinesLogicLayer.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dto;
using System.Text;

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
        [Route("one/{id}")]
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
        [Route("{username}")]
        public IActionResult GetUser([FromRoute(Name = "username")] string username)
        {
            try
            {
                UserDto u = unit.UserService.UcitajUseraByUsername(username);
                if (u != null)
                {
                    return Ok(u);
                }
                return NotFound();

            }
            catch (Exception ex)
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

        [Authorize]
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> search()
        {
            var request = HttpContext.Request;
            var kriterijum = "";

            using(StreamReader reader=new StreamReader(request.Body,Encoding.UTF8, true, 1024, true))
            {
                kriterijum = await reader.ReadLineAsync();
            }
            kriterijum = kriterijum.Remove(0, 1);
            kriterijum = kriterijum.Remove(kriterijum.Length - 1);
            return Ok(unit.UserService.Search(kriterijum));
        }

        [Authorize]
        [HttpPost]
        [Route("changePicture")]
        public IActionResult changeProfilePicture([FromBody]UserDto u)
        {
            try
            {
                if(unit.UserService.ChangePicture(u))
                {
                    return Ok();
                } else
                {
                    return BadRequest();
                }
                
            } catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
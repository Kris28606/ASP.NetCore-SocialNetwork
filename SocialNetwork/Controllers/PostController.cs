
using BusinesLogicLayer.UnitOfWork;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dto;
using SocialNetwork.PictureUpload;

namespace SocialNetwork.Controllers
{
    [Route("post/")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IUnitOfWorkService unit;
        private readonly IWebHostEnvironment env;

        public PostController(IUnitOfWorkService unit, IWebHostEnvironment env)
        {
            this.unit = unit;
            this.env = env;
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
        [HttpPost]
        [Route("upload")]
        public IActionResult UploadPicture()
        {
            var file = HttpContext.Request.Form.Files[0];
            try
            {
                Uploader u = new Uploader(env);
                return Ok(u.Upload2((FormFile)file));

            }catch(Exception ex)
            {
                return BadRequest();
            }
            //var baseUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host +
            //    HttpContext.Request.PathBase;
            //Uploader u = new Uploader(env);
            //try
            //{
            //    string result = u.SaveAndCreatePath((FormFile)file, baseUrl);
            //    return Ok(result);
            //} catch(Exception e)
            //{
            //    return BadRequest();
            //}
            
        }

        [Authorize]
        [HttpGet]
        [Route("all/{id}")]
        public IActionResult GetPostsForHomePage([FromRoute(Name = "id")] int id)
        {
            List<PostResponse> result=unit.PostService.GetAllForHome(id);
            return Ok(result);

        }
    }
}

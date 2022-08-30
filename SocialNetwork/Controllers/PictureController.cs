using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.PictureUpload;

namespace SocialNetwork.Controllers
{
    [Route("picture/")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private readonly IWebHostEnvironment env;

        public PictureController(IWebHostEnvironment env)
        {
            this.env = env;
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

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}

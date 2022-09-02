using BusinesLogicLayer.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SocialNetwork.Controllers
{
    [Route("message/")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IUnitOfWorkService unit;

        public MessageController(IUnitOfWorkService unit)
        {
            this.unit = unit;
        }


        [Authorize]
        [HttpGet]
        [Route("{for}/{from}")]
        public IActionResult GetMessages([FromRoute(Name = "for")] int forId, [FromRoute(Name = "from")] int fromId)
        {
            try
            {
                return Ok(unit.MessageService.GetChat(fromId, forId));
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("inbox/{id}")]
        public IActionResult GetInboxUsers([FromRoute(Name ="id")] int id){
            try
            {
                return Ok(unit.MessageService.GetInboxUsers(id));   
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

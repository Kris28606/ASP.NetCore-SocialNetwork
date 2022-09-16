using BusinesLogicLayer.Interfaces;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SocialNetwork.Controllers
{
    [Route("message/")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }


        [Authorize]
        [HttpGet]
        [Route("{for}/{from}")]
        public IActionResult GetMessages([FromRoute(Name = "for")] int forId, [FromRoute(Name = "from")] int fromId)
        {
            try
            {
                return Ok(messageService.GetChat(fromId, forId));
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
                return Ok(messageService.GetInboxUsers(id));   
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> SendMeesage([FromBody]MessageDto mess)
        {
            try
            {
                MessageDto m = messageService.SendMessage(mess);
                if (m != null)
                {
                    return Ok(m);
                } else
                {
                    return BadRequest();
                }
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

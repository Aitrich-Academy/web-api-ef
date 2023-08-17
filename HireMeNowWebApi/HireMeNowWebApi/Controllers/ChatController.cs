using HireMeNowWebApi.HubConfig;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HireMeNowWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hub;
        private readonly IChatRepository _chatRepository;
        public ChatController( IChatRepository chatREpository)
        {
    
            _chatRepository=chatREpository;
        }

        [HttpGet]
        [Route("getMessages")]
        public async Task<IActionResult> GetMessagesByGroupAsync(string groupName)
        {
                var messages= await _chatRepository.GetMessagesByGroupName(groupName);
            return Ok(messages);
        }

        [HttpGet]
        [Route("getMessageGroup")]
        public async Task<IActionResult> GetMessageGroupByUserId(Guid? UserId,string? email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var messageGroups = await _chatRepository.GetAllMessageGroupsByEmail(email);
                return Ok(messageGroups);
            }
            if (UserId!=null)
            {
                var messageGroups = await _chatRepository.GetAllMessageGroupsByUserId(UserId.Value);
                return Ok(messageGroups);
            }
            return NoContent();
        }

    }
}

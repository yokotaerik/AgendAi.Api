using MarcAI.Application.Dtos.Messages;
using Microsoft.AspNetCore.Mvc;

namespace MarcAI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly Application.Interfaces.IMessageService _messageService;

        public MessageController(Application.Interfaces.IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("chat")]
        public async Task<IActionResult> GetChat()
        {
            var messages = await _messageService.GetChats();
            return Ok(messages);
        }

        [HttpGet("conversation/{user1Id}/{user2Id}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetConversation(string user1Id, string user2Id)
        {
            var messages = await _messageService.GetMessagesBetweenUsersAsync(user1Id, user2Id);
            return Ok(messages);
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> SendMessage([FromBody] SendMessageRequest request)
        {
            var message = await _messageService.SendMessageAsync(
                request.Content,
                request.SenderId,
                request.ReceiverId);

            return Ok();
        }

    }

    public class SendMessageRequest
    {
        public string Content { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }
}

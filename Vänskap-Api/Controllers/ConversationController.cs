using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vänskap_Api.Models.Dtos.Conversation;
using Vänskap_Api.Service.IService;

namespace Vänskap_Api.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;

        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpPost]
        public async Task<IActionResult> StartConversation(List<string> userNames)
        {
            var result = await _conversationService.StartConversation(userNames);
            if (!result) return BadRequest();

            return Ok("Success");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> SeeAllConversations()
        {
            var result = await _conversationService.SeeAllConversations();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SeeConversationDto>> SeeConversation(int id)
        {
            var result = await _conversationService.SeeConversation(id);
            if (result == null) return BadRequest("Could not find or access the conversation.");

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditConversationTitle(int id, string title)
        {
            var result = await _conversationService.EditConversationTitle(id, title);
            if (!result) return BadRequest();

            return Ok(result);
        }

        [HttpDelete("removeparticipants{id}")]
        public async Task<IActionResult> RemoveConversationParticipants(int id, List<string> userNames)
        {
            var result = await _conversationService.RemoveConversationParticipants(id, userNames);
            if (!result) return BadRequest();

            return Ok(result);
        }

        // In this endpoint it is optional to add the second paramter because if the user has atlest one host
        // or is not host themselves they should be able to leave without the second paramter but if they are
        // the alone host they have to provide 1 or more hosts before they leave.
        [HttpDelete("removeuser/{id}")]
        public async Task<IActionResult> RemoveUrselfFromConversation(int id, List<string?> userNames)
        {
            var result = await _conversationService.RemoveUrselfFromConversation(id, userNames);
            if (!result) return BadRequest();

            return Ok(result);
        }

        [HttpPost("messages")]
        public async Task<IActionResult> SendMessage(string content, int id)
        {
            var result = await _conversationService.SendMessage(content, id);
            if (!result) return BadRequest("User not part of a conversation.");

            return Ok("Success");
        }
    }
}

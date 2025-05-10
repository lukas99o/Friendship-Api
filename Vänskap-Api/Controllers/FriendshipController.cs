using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vänskap_Api.Models.Dtos.User;
using Vänskap_Api.Service.IService;

namespace Vänskap_Api.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [ApiController]
    [Route("api/[controller]")]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;

        public FriendshipController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        [HttpGet("friends")]
        public async Task<ActionResult<IEnumerable<string>>> SeeFriendList()
        {
            return Ok(await _friendshipService.SeeFriendList());
        }

        [HttpDelete("friends/{id}")]
        public async Task<IActionResult> RemoveFriend(string userName)
        {
            var result = await _friendshipService.RemoveFriend(userName);
            if (!result) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("friendrequests")]
        public async Task<IActionResult> SendFriendRequest(string userName)
        {
            var result = await _friendshipService.SendFriendRequest(userName);
            if (!result) return BadRequest($"Could not find user with username: {userName}.");

            return Ok("Success");
        }

        [HttpPost("friendrequests/{id}")]
        public async Task<IActionResult> AcceptFriendRequest(int id)
        {
            var result = await _friendshipService.AcceptFriendRequest(id);
            if (!result) return BadRequest("No friend request with the given id.");

            return Ok("Accepted friend request.");
        }

        [HttpDelete("friendrequests/{id}")]
        public async Task<IActionResult> DeclineFriendRequest(int id)
        {
            var result = await _friendshipService.DeclineFriendRequest(id);
            if (!result) return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("friendrequests/regretrequest/{id}")]
        public async Task<IActionResult> RemoveFriendRequest(int id)
        {
            var result = await _friendshipService.RemoveFriendRequest(id);
            if (!result) return BadRequest();

            return Ok(result);
        }

        [HttpGet("friendrequests")]
        public async Task<ActionResult<FriendRequestDto>> SeeFriendRequests()
        {
            var (incoming, outgoing) = await _friendshipService.SeeFriendRequests();

            return Ok(new FriendRequestDto
            {
                IncomingRequests = incoming,
                OutgoingRequests = outgoing
            });
        }
    }
}

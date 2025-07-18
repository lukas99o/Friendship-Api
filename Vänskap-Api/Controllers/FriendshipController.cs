﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vänskap_Api.Models.Dtos.Friend;
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

        [HttpPost("sendfriendrequest")]
        public async Task<IActionResult> SendFriendRequest(string userName)
        {
            var result = await _friendshipService.SendFriendRequest(userName);
            if (!result) return BadRequest($"Could not find user with username: {userName}.");

            return Ok("Success");
        }

        [HttpPost("acceptfriendrequest/{id}")]
        public async Task<IActionResult> AcceptFriendRequest(int id)
        {
            var result = await _friendshipService.AcceptFriendRequest(id);
            if (!result) return BadRequest("No friend request with the given id.");

            return Ok("Accepted friend request.");
        }

        [HttpGet("friends")]
        public async Task<ActionResult<IEnumerable<string>>> SeeFriendList()
        {
            return Ok(await _friendshipService.SeeFriendList());
        }

        [HttpGet("friendrequests")]
        public async Task<ActionResult<GetFriendRequestsDto>> SeeFriendRequests()
        {
            return Ok(await _friendshipService.SeeFriendRequests());
        }

        [HttpDelete("removefriend/{id}")]
        public async Task<IActionResult> RemoveFriend(string userName)
        {
            var result = await _friendshipService.RemoveFriend(userName);
            if (!result) return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("declinefriendrequest/{id}")]
        public async Task<IActionResult> DeclineFriendRequest(int id)
        {
            var result = await _friendshipService.DeclineFriendRequest(id);
            if (!result) return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("regretfriendrequest/{id}")]
        public async Task<IActionResult> RemoveFriendRequest(int id)
        {
            var result = await _friendshipService.RemoveFriendRequest(id);
            if (!result) return BadRequest();

            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using PetHospital.Business.Interfaces;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;

namespace PetHospital.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _userService.GetUserInfoAsync(userId);
            return Ok(result);
        }

        [HttpPatch("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePassword([FromBody]ChangePasswordRequest newPassword)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _userService.ChangePasswordAsync(userId, newPassword);
            return Ok(result);
        }

        [HttpDelete("[action]")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _userService.DeleteAsync(userId);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserInfo(UserRequest userRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _userService.UpdateAsync(userId, userRequest);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}

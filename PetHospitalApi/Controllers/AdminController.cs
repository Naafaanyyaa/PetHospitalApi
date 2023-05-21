using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHospital.Business.Interfaces;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;
using PetHospital.Data.Enums;

namespace PetHospital.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService, IUserService userService, IClinicService clinicService)
        {
            _adminService = adminService;
        }

        [HttpPatch("[action]/{userId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> BanUser(string userId)
        {
            var result = await _adminService.BanUser(userId);
            return StatusCode(StatusCodes.Status201Created, result);
        }
        [HttpPatch("[action]/{clinicId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ClinicResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> BanClinic(string clinicId)
        {
            var result = await _adminService.BanClinic(clinicId);
            return StatusCode(StatusCodes.Status201Created, result);
        }
        [HttpPatch("[action]")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> ChangeRole([FromBody] ChangeRoleRequest request)
        {
            var result = await _adminService.ChangeRole(request.userId, request.Role);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpGet("[action]")]
        [Authorize(Roles = "Admin, Doctor")]
        [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserList([FromQuery] UserAllRequest request)
        {
            var result = await _adminService.GetUserList(request);
            return StatusCode(StatusCodes.Status201Created, result);
        }
        [HttpGet("[action]/{userName}")]
        [Authorize(Roles = "Doctor, User")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserByUserName(string userName)
        {
            var result = await _adminService.GetUserByUserName(userName);
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}

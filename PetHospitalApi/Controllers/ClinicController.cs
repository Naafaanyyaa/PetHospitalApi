using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHospital.Business.Interfaces;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;
using System.Security.Claims;

namespace PetHospital.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicService _clinicService;

        public ClinicController(IClinicService clinic)
        {
            _clinicService = clinic;
        }

        [HttpGet("[action]/{clinicId}")]
        [Authorize(Roles = "HospitalHost")]
        [ProducesResponseType(typeof(IEnumerable<ClinicResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetClinicById(string clinicId)
        {
            var result = await _clinicService.GetClinicById(clinicId);
            return Ok(result);
        }


        [HttpPost("[action]")]
        [Authorize(Roles = "HospitalHost")] 
        [ProducesResponseType(typeof(IEnumerable<ClinicResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromQuery] ClinicRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _clinicService.CreateAsync(request, userId);
            return Ok(result);
        }

        [HttpDelete("[action]/{hospitalId}")]
        [Authorize(Roles = "HospitalHost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(string hospitalId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _clinicService.DeleteByIdAsync(userId, hospitalId);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut("[action]/{hospitalId}")]
        [Authorize(Roles = "HospitalHost")]
        [ProducesResponseType(typeof(ClinicResponse),StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(string hospitalId,[FromQuery] ClinicRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _clinicService.UpdateByIdAsync(userId, hospitalId, request);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(List<ClinicResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetClinicByRequest([FromQuery] ClinicAllRequest request)
        {
            var result = await _clinicService.GetAllClinicsByRequest(request);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "HospitalHost")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorRegistrationRequest request, string clinicId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _clinicService.RegisterDoctor(userId, clinicId, request);
            return StatusCode(StatusCodes.Status201Created, result);
        }
        //TODO: 2) statistics with animal and users
    }
}

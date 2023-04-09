using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHospital.Business.Interfaces;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;

namespace PetHospital.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly ILoginService _loginService;

        public AuthenticationController(IRegistrationService registrationService, ILoginService loginService)
        {
            _registrationService = registrationService;
            _loginService = loginService;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RegistrationResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequest request)
        {
            var result = await _registrationService.Registration(request);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthorizeResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequest request)
        {
            var result = await _loginService.Login(request);
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}

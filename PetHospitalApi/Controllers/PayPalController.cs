using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using PetHospital.Business.Exceptions;
using PetHospital.Business.Interfaces;
using PetHospital.Data.Enums;


namespace PetHospital.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PayPalController : ControllerBase
    {
        private readonly IPayPalService _payPalService;

        public PayPalController(IPayPalService payPal)
        {
            _payPalService = payPal;
        }

        [HttpGet("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePayment(SubscriptionType type)
        {
            var result = await _payPalService.CreatePayment(type);

            foreach (var link in result.links)
            {
                if (link.rel.Equals("approval_url"))
                {
                    return StatusCode(StatusCodes.Status201Created, link.href);
                }
            }

            return NotFound();
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Payment), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExecutePayment(string paymentId, string token, string payerId)
        {
            Payment result = await _payPalService.ExecutePayment(payerId, paymentId);

            return Ok(result);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> CancelPayment()
        {
            throw new NotFoundException("Something is wrong with your payment.");
        }
    }
}
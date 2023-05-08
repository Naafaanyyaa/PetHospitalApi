using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using PetHospital.Business.Exceptions;
using PetHospital.Business.Interfaces;
using PetHospital.Data.Enums;
using PetHospital.Data.Interfaces;


namespace PetHospital.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PayPalController : ControllerBase
    {
        private readonly IPayPalService _payPalService;
        private readonly ISubscriptionService _subscriptionService;

        public PayPalController(IPayPalService payPal, ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
            _payPalService = payPal;
        }

        [HttpGet("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePayment(SubscriptionType type)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _payPalService.CreatePayment(type);

            foreach (var link in result.links)
            {
                if (link.rel.Equals("approval_url"))
                {
                    await _subscriptionService.AddSubscriptionAsync(userId, type);

                    return StatusCode(StatusCodes.Status201Created, link.href);
                }
            }

            return NotFound();
        }

        [HttpGet("[action]")]
        [Authorize]
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
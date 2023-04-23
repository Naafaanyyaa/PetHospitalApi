using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHospital.Business.Interfaces;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;

namespace PetHospital.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AnimalController : ControllerBase
    {
       private readonly IAnimalService _animalService;

       public AnimalController(IAnimalService animalService)
       {
           _animalService = animalService;
       }

       [HttpGet("[action]")]
       [Authorize]
       [ProducesResponseType(typeof(List<ClinicResponse>), StatusCodes.Status200OK)]
       public async Task<IActionResult> GetAnimalByRequest([FromQuery] AnimalAllRequest request)
       {
           var result = await _animalService.GetAllPetsByRequest(request);
           return StatusCode(StatusCodes.Status200OK, result);
       }

       [HttpGet("[action]/{animalId}")]
       [Authorize]
       [ProducesResponseType(typeof(ClinicResponse), StatusCodes.Status200OK)]
       public async Task<IActionResult> GetPetById(string animalId)
       { 
           var result = await _animalService.GetPetById(animalId);
           return StatusCode(StatusCodes.Status200OK, result);
       }

       [HttpPost("[action]")]
       [Authorize(Roles = "Doctor, User")]
       [ProducesResponseType(typeof(ClinicResponse), StatusCodes.Status200OK)]
       public async Task<IActionResult> CreateAnimal([FromForm] AnimalRequest request, string? clinicId, string userId)
       {
           var result = await _animalService.CreateAsync(request, userId, clinicId, Request.Form.Files, Directory.GetCurrentDirectory());
           return StatusCode(StatusCodes.Status201Created, result);
       }

       [HttpDelete("[action]/{animalId}")]
       [Authorize(Roles = "User")]
       [ProducesResponseType(StatusCodes.Status200OK)]
       public async Task<IActionResult> DeleteById(string animalId)
       {
           var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
           await _animalService.DeleteByIdAsync(userId, animalId);
           return StatusCode(StatusCodes.Status200OK);
       }

       [HttpPut("[action]/{animalId}")]
       [Authorize(Roles = "User")]
       [ProducesResponseType(StatusCodes.Status200OK)]
       public async Task<IActionResult> UpdateById(string animalId, [FromBody] AnimalRequest request)
       {
           var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
           var result = await _animalService.UpdateByIdAsync(userId, animalId, request);
           return StatusCode(StatusCodes.Status200OK, result);
       }

       [HttpPut("[action]/{animalId}")]
       [Authorize(Roles = "Doctor")]
       [ProducesResponseType(StatusCodes.Status200OK)]
       public async Task<IActionResult> AddExistingAnimalToClinic(string animalId, string clinicId)
       { 
           var result = await _animalService.AddExistingAnimalToClinic(animalId, clinicId);
           return StatusCode(StatusCodes.Status200OK, result);
       }
    }
}

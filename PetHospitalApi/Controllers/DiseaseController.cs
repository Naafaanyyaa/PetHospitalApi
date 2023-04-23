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
    public class DiseaseController : ControllerBase
    {
        private readonly IDiseaseService _diseaseService;

        public DiseaseController(IDiseaseService diseaseService)
        {
            _diseaseService = diseaseService;
        }

        [HttpGet("[action]/{animalId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<DiseaseResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDiseaseList(string animalId)
        {
            var result = await _diseaseService.GetDiseaseList(animalId);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(DiseaseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDiseaseById(string diseaseId)
        {
            var result = await _diseaseService.GetDiseaseById(diseaseId);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPost("[action]/{animalId}")]
        [Authorize]
        [ProducesResponseType(typeof(DiseaseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddDisease(string animalId, [FromBody] DiseaseRequest request)
        {
            var result = await _diseaseService.AddDisease(animalId, request);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(DiseaseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateDisease(string diseaseId, [FromBody] DiseaseRequest request)
        {
            var result = await _diseaseService.UpdateDisease(diseaseId, request);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpDelete("[action]")]
        [Authorize]
        [ProducesResponseType(typeof(DiseaseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteDisease(string diseaseId)
        {
            await _diseaseService.DeleteDisease(diseaseId);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}

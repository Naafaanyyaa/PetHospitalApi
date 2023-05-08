using Microsoft.AspNetCore.Http;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;

namespace PetHospital.Business.Interfaces
{
    public interface IAnimalService
    {
        Task<List<AnimalResponse>> GetAllPetsByRequest(AnimalAllRequest request);
        Task<AnimalResponse> GetPetById(string requestId);
        Task<AnimalResponse> CreateAsync(AnimalRequest request, IFormFileCollection files, string directoryToSave);
        Task DeleteByIdAsync(string userId, string animalId);
        Task<AnimalResponse> UpdateByIdAsync(string userid, string animalId, AnimalUpdateRequest request);
        Task<AnimalResponse> AddExistingAnimalToClinic(AddExistingAnimalRequest addExistingAnimalRequest);
    }
}

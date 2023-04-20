using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;

namespace PetHospital.Business.Interfaces
{
    public interface IAnimalService
    {
        Task<List<AnimalResponse>> GetAllPetsByRequest(AnimalAllRequest request);
        Task<AnimalResponse> GetPetById(string requestId);
        Task<AnimalResponse> CreateAsync(AnimalRequest request, string userId, string? clinicId, IFormFileCollection files, string directoryToSave);
        Task DeleteByIdAsync(string userId, string animalId);
        Task<AnimalResponse> UpdateByIdAsync(string userid, string animalId, AnimalRequest request);
        Task<AnimalResponse> AddExistingAnimalToClinic(string animalId, string clinicId);

        //TODO: to be able to add diseases 4 pet
    }
}

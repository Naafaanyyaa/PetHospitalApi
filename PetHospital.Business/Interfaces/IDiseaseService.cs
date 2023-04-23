using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;

namespace PetHospital.Business.Interfaces
{
    public interface IDiseaseService
    {
        Task<List<DiseaseResponse>> GetDiseaseList(string animalId);
        Task<DiseaseResponse> GetDiseaseById(string diseaseId);
        Task<DiseaseResponse> UpdateDisease(string diseaselId, DiseaseRequest request);
        Task DeleteDisease(string diseaseId);
        Task<DiseaseResponse> AddDisease(string animalId, DiseaseRequest request);
    }
}

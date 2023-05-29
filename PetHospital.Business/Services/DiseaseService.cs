using System.Linq.Expressions;
using AutoMapper;
using PetHospital.Business.Exceptions;
using PetHospital.Business.Interfaces;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;
using PetHospital.Data.Entities;
using PetHospital.Data.Interfaces;

namespace PetHospital.Business.Services
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IDiseasesRepository _diseasesRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly IMapper _mapper;

        public DiseaseService(IDiseasesRepository diseasesRepository, IMapper mapper, IAnimalRepository animalRepository)
        {
            _diseasesRepository = diseasesRepository;
            _mapper = mapper;
            _animalRepository = animalRepository;
        }

        public async Task<List<DiseaseResponse>> GetDiseaseList(string animalId)
        {
            var source = await _diseasesRepository.GetAsync(d => d.AnimalId == animalId, includes: new List<Expression<Func<Diseases, object>>>());

            var result = _mapper.Map<List<Diseases>, List<DiseaseResponse>>(source);
            return result;
        }

        public async Task<DiseaseResponse> GetDiseaseById(string diseaseId)
        {
            var disease = await _diseasesRepository.GetByIdAsync(diseaseId);

            if (disease == null)
            {
                throw new NotFoundException("Disease is not found");
            }

            var result = _mapper.Map<Diseases, DiseaseResponse>(disease);
            return result;
        }
        public async Task<DiseaseResponse> AddDisease(string animalId, DiseaseRequest request)
        {
            var animal = await _animalRepository.GetByIdAsync(animalId);

            if (animal == null)
            {
                throw new NotFoundException("Animal is not found");
            }
            var disease = _mapper.Map<DiseaseRequest, Diseases>(request);

            disease.AnimalId = animalId;
            disease.CreatedDate = DateTime.Now;

            await _diseasesRepository.AddAsync(disease);

            var result = _mapper.Map<Diseases, DiseaseResponse>(disease);

            return result;
        }

        public async Task<DiseaseResponse> UpdateDisease(string diseaseId, DiseaseRequest request)
        {
            var previousDisease = await _diseasesRepository.GetByIdAsync(diseaseId);

            if (previousDisease == null)
            {
                throw new NotFoundException("Disease is not found");
            }

            var disease = _mapper.Map<DiseaseRequest, Diseases>(request, previousDisease);
            disease.LastModifiedDate = DateTime.Now;

            await _diseasesRepository.UpdateAsync(disease);

            var result = _mapper.Map<Diseases, DiseaseResponse>(disease);

            return result;
        }

        public async Task DeleteDisease(string diseaseId)
        {
            await _diseasesRepository.DeleteById(diseaseId);
        }
    }
}

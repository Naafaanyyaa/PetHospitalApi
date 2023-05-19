using PetHospital.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PetHospital.Business.Exceptions;
using PetHospital.Data.Entities;
using PetHospital.Data.Entities.Identity;
using PetHospital.Data.Interfaces;

namespace PetHospital.Business.Services
{
    public class IoTDiseaseService : IIoTDiseaseService
    {
        private readonly IDiseasesRepository _diseasesRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public IoTDiseaseService(IDiseasesRepository diseasesRepository, IMapper mapper, IAnimalRepository animalRepository, UserManager<User> userManager)
        {
            _diseasesRepository = diseasesRepository;
            _mapper = mapper;
            _animalRepository = animalRepository;
            _userManager = userManager;
        }

        public async Task AddDiseaseFromIot(DiseaseIoTRequest diseaseRequest)
        {
            var animal = await _animalRepository.GetByIdAsync(diseaseRequest.AnimalId);

            if (animal == null)
            {
                throw new NotFoundException("Animal is not found");
            }

            var indicators = $"Blood oxygen: {diseaseRequest.HealthRequest.BloodOxygen};\nHeart rate: {diseaseRequest.HealthRequest.HeartRate};\nPressure {diseaseRequest.HealthRequest.Pressure};\nTemperature {diseaseRequest.HealthRequest.Temperature}";

            var requestToDb = _mapper.Map<DiseaseIoTRequest, Diseases>(diseaseRequest);

            requestToDb.DiseaseDescription += $"\n{indicators}";
            requestToDb.AnimalId = diseaseRequest.AnimalId;
            requestToDb.CreatedDate = DateTime.Now;

            await _diseasesRepository.AddAsync(requestToDb);
        }
    }
}

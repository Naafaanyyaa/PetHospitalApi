using System.Linq.Expressions;
using System.Net.Http.Headers;
using AutoMapper;
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PetHospital.Business.Exceptions;
using PetHospital.Business.Infrastructure.Expressions;
using PetHospital.Business.Interfaces;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;
using PetHospital.Data.Entities;
using PetHospital.Data.Entities.Identity;
using PetHospital.Data.Interfaces;


namespace PetHospital.Business.Services
{
    internal class AnimalService : IAnimalService
    {
        private readonly IMapper _mapper;
        private readonly IAnimalRepository _animalRepository;
        private readonly IClinicRepository _clinicRepository;
        private readonly UserManager<User> _userManager;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _storageAccount = "pethospitalblob";
        private readonly string _accessKey = "0rq2U882SOxsdCCifpFhtSPJThnzwywXDE6xA6P9vwuY3H5Pu6DgLvG8z/QoKvn1z2F7mqQw1jnl+AStcjsgvQ==";
        public AnimalService(IMapper mapper, IAnimalRepository animalRepository, UserManager<User> userManager, IClinicRepository clinicRepository)
        {
            _mapper = mapper;
            _animalRepository = animalRepository;
            _userManager = userManager;
            _clinicRepository = clinicRepository;
            var credential = new StorageSharedKeyCredential(_storageAccount, _accessKey);
            var blobUri = $"https://{_storageAccount}.blob.core.windows.net";
            _blobServiceClient = new BlobServiceClient(new Uri(blobUri), credential);
        }

        public async Task<List<AnimalResponse>> GetAllPetsByRequest(AnimalAllRequest request)
        {
            var predicate = CreateFilterPredicate(request);
            var source = await _animalRepository.GetAsync(predicate, includes: new List<Expression<Func<Animal, object>>>()
            {
                x => x.User,
                x => x.Photos,
                x => x.Diseases
            });

            var result = _mapper.Map<List<Animal>, List<AnimalResponse>>(source);
            return result;
        }

        public async Task<AnimalResponse> GetPetById(string requestId)
        {
            var source = await _animalRepository.GetByIdAsync(requestId);

            if (source == null)
                throw new NotFoundException("Animal is not found");

            var result = _mapper.Map<Animal, AnimalResponse>(source);
            return result;
        }

        public async Task<AnimalResponse> CreateAsync(AnimalRequest request, IFormFileCollection files, string directoryToSave)
        {
            var owner = await _userManager.FindByIdAsync(request.UserId);

            if (owner == null)
            {
                throw new NotFoundException("User is not found");
            }

            var date = DateTime.Now;

            var animal = _mapper.Map<AnimalRequest, Animal>(request);
            
            animal.UserId = owner.Id;
            animal.CreatedDate = date;
            animal.User = owner;


            if (!request.ClinicId.IsNullOrEmpty())
            {
                var clinic = await _clinicRepository.GetByIdAsync(request.ClinicId);

                if (clinic == null)
                {
                    throw new NotFoundException("Clinic is not found");
                }

                AnimalClinic animalClinic = new AnimalClinic()
                {
                    ClinicId = request.ClinicId,
                    AnimalId = animal.Id,
                    CreatedDate = date
                };

                animal.AnimalClinic = new List<AnimalClinic>()
                {
                    animalClinic
                };
            }

            var response = await SavePhoto(files, directoryToSave, animal);

            await _animalRepository.AddAsync(response);

            var result = _mapper.Map<Animal, AnimalResponse>(animal);

            return result;
        }

        public async Task DeleteByIdAsync(string userId, string animalId)
        {
            var animal = await _animalRepository.GetByIdAsync(animalId);

            if (animal == null)
            {
                throw new NotFoundException("Animal is not found");
            }

            if (userId != animal.UserId)
            {
                throw new ValidationException("This animal is not belong to this user");
            }

            await _animalRepository.DeleteAsync(animal);   
        }

        //TODO: change animalRequest
        public async Task<AnimalResponse> UpdateByIdAsync(string userId, string animalId, AnimalUpdateRequest request)
        {
            var animal = await _animalRepository.GetByIdAsync(animalId);

            if (animal == null)
            {
                throw new NotFoundException("Animal is not found");
            }

            if (animal.UserId != userId)
            {
                throw new ValidationException("Doesn't belong to this user");
            }

            var updateModel = _mapper.Map<AnimalUpdateRequest, Animal>(request, animal);

            updateModel.LastModifiedDate = DateTime.Now;

            await _animalRepository.UpdateAsync(updateModel);

            var result = _mapper.Map<Animal, AnimalResponse>(updateModel);

            return result;
        }

        public async Task<AnimalResponse> AddExistingAnimalToClinic(AddExistingAnimalRequest addExistingAnimalRequest)
        {
            var clinic = await _clinicRepository.GetByIdAsync(addExistingAnimalRequest.ClinicId);

            if (clinic == null)
            {
                throw new NotFoundException("Clinic is not found");
            }

            var animal = await _animalRepository.GetByIdAsync(addExistingAnimalRequest.AnimalId);

            if (animal.AnimalClinic.Exists(x => (x.ClinicId == addExistingAnimalRequest.ClinicId && x.AnimalId == addExistingAnimalRequest.AnimalId)))
            {
                throw new ValidationException("This animal has already been declared");
            }

            await _animalRepository.UpdateAsync(animal, clinic);

            var result = _mapper.Map<Animal, AnimalResponse>(animal);

            return result;
        }

        public async Task<List<AnimalResponse>> GetUserPetsList(string userId)
        {
            var animalList = await _animalRepository.GetAsync(animal => animal.UserId == userId);

            var result = _mapper.Map<List<Animal>, List<AnimalResponse>>(animalList);

            return result;
        }

        private Expression<Func<Animal, bool>>? CreateFilterPredicate(AnimalAllRequest request)
        {
            Expression<Func<Animal, bool>>? predicate = null;

            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                Expression<Func<Animal, bool>> searchStringExpression = x =>
                    x.AnimalDescription != null && x.AnimalDescription.Contains(request.SearchString) ||
                    x.AnimalName.Contains(request.SearchString);

                predicate = ExpressionsHelper.And(predicate, searchStringExpression);
            }

            if (request.Status.HasValue && Enum.IsDefined(request.Status.Value))
            {
                Expression<Func<Animal, bool>> statusPredicate = x => x.AnimalType == request.Status.Value;
                predicate = ExpressionsHelper.And(predicate, statusPredicate);
            }

            if (!string.IsNullOrWhiteSpace(request.UserId))
            {
                Expression<Func<Animal, bool>> userIdPredicate = x => x.UserId == request.UserId;
                predicate = ExpressionsHelper.And(predicate, userIdPredicate);
            }

            if (request.StartDate.HasValue && request.EndDate.HasValue && request.StartDate < request.EndDate)
            {
                Expression<Func<Animal, bool>> dateExpression = x => x.CreatedDate > request.StartDate.Value
                                                                     && x.CreatedDate < request.EndDate.Value;
                predicate = ExpressionsHelper.And(predicate, dateExpression);
            }

            return predicate;
        }

        private async Task<Animal> SavePhoto(IFormFileCollection files, string directoryToSave, Animal animal)
        {
            if (files?.Any() == true)
            {
                var blobUris = new List<Uri>();

                var blobContainerName = "photos";
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);

                foreach (var file in files)
                {
                    var blobName = Path.Combine(animal.Id, file.FileName);
                    var blobClient = blobContainerClient.GetBlobClient(blobName);
                    await blobClient.UploadAsync(file.OpenReadStream());

                    var blobUri = blobClient.Uri;
                    blobUris.Add(blobUri);
                }
                animal.Photos = new List<Photo>();

                animal.Photos.Add(new Photo()
                {
                    PhotoPath = blobUris[0].ToString(),
                    AnimalId = animal.Id,
                });

            }

            return animal;
        }
    }
}

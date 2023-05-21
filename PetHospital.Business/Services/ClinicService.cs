using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetHospital.Business.Exceptions;
using PetHospital.Business.Interfaces;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;
using PetHospital.Data.Entities;
using PetHospital.Data.Entities.Identity;
using PetHospital.Data.Interfaces;
using System.Linq.Expressions;
using PetHospital.Business.Infrastructure.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PetHospital.Business.Services
{
    public class ClinicService : IClinicService
    {

        private readonly IClinicRepository _clinicRepository;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ClinicService> _logger;
        private readonly IMapper _mapper;

        public ClinicService(IClinicRepository clinicRepository, UserManager<User> userManager,
            ILogger<ClinicService> logger, IMapper mapper)
        {
            _clinicRepository = clinicRepository;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<ClinicResponse>> GetAllClinicsByRequest(ClinicAllRequest request)
        {
            var predicate = CreateFilterPredicate(request);
            var source = await _clinicRepository.GetAsync(predicate, includes: new List<Expression<Func<Clinic, object>>>()
            {
                x => x.UserClinic
            });

            var result = _mapper.Map<List<Clinic>, List<ClinicResponse>>(source);
            return result;
        }

        public async Task<ClinicResponse> GetClinicById(string requestId, string userId)
        {
            var clinic = await _clinicRepository.GetByIdAsync(requestId);

            if (clinic == null)
            {
                throw new NotFoundException("Clinic is not found");
            }

            var result = _mapper.Map<Clinic, ClinicResponse>(clinic);

            if (await _clinicRepository.IsUserCreatorOfClinicAsync(userId, requestId))
            {
                result.IsCreatedByUser = true;
            }

            return result;
        }

        public async Task<ClinicResponse> CreateAsync(ClinicRequest request, string userId)
        {
            var owner = await _userManager.FindByIdAsync(userId);

            if (owner == null)
            {
                throw new NotFoundException("User is not found");
            }

            var clinic = _mapper.Map<ClinicRequest, Clinic>(request);

            clinic.CreatedDate = DateTime.Now;

            var insertResponse = await _clinicRepository.AddAsync(clinic, owner, true);

            _logger.LogInformation("Clinic {ClinicId} has been successfully registered", clinic.Id);

            var result = _mapper.Map<Clinic, ClinicResponse>(insertResponse);

            return result;
        }

        public async Task DeleteByIdAsync(string userId, string hospitalId)
        {
            var hospital = await _clinicRepository.GetByIdAsync(hospitalId);

            if (!await _clinicRepository.IsUserCreatorOfClinicAsync(userId, hospitalId))
            {
                throw new ValidationException("Hospital was not created by this user");
            }

            await _clinicRepository.DeleteById(hospitalId);

        }

        public async Task<ClinicResponse> UpdateByIdAsync(string userId, string hospitalId, ClinicRequest request)
        {
            var hospital = await _clinicRepository.GetByIdAsync(hospitalId);

            if (hospital == null)
            {
                throw new ValidationException("Hospital is not found");
            }

            if (hospital.IsBanned == true)
            {
                throw new ValidationException("Hospital was banned");
            }

            if (!await _clinicRepository.IsUserCreatorOfClinicAsync(userId, hospitalId))
            {
                throw new ValidationException("Hospital was not created by this user");
            }

            var updatedHospitalInfo = _mapper.Map<ClinicRequest, Clinic>(request);

            updatedHospitalInfo.Id = hospital.Id;
            updatedHospitalInfo.CreatedDate = hospital.CreatedDate;
            updatedHospitalInfo.LastModifiedDate = DateTime.Now;

            await _clinicRepository.UpdateAsync(updatedHospitalInfo);

            var result = _mapper.Map<Clinic, ClinicResponse>(updatedHospitalInfo);

            return result;
        }

        public async Task<UserResponse> RegisterDoctor(string userId, string hospitalId, DoctorRegistrationRequest userRequest)
        {
            var clinic = await _clinicRepository.GetByIdAsync(hospitalId);

            if (clinic == null)
            {
                throw new NotFoundException(nameof(clinic), hospitalId);
            }

            if (!await _clinicRepository.IsUserCreatorOfClinicAsync(userId, hospitalId))
            {
                throw new ValidationException("Hospital was not created by this user");
            }

            var isUserExists = await _userManager.Users.AnyAsync(x => x.Email == userRequest.Email || x.UserName == userRequest.UserName);

            if (isUserExists)
            {
                throw new ValidationException("User with such username or email already exists");
            }

            User user = new User();
            _mapper.Map(userRequest, user);

            UserClinic userClinic = new UserClinic()
            {
                ClinicId = hospitalId,
                UserId = user.Id,
                CreatedDate = DateTime.Now,
                IsCreator = false
            };

            user.UserClinic = new List<UserClinic>()
            {
                userClinic
            };

            var identityResult = await _userManager.CreateAsync(user, userRequest.Password);

            if (identityResult.Errors.Any())
                throw new Exception(identityResult.Errors.ToArray().ToString());

            identityResult = await _userManager.AddToRolesAsync(user, new List<string>
            {
                CustomRoles.UserRole,
                CustomRoles.DoctorRole
            });

            if (identityResult.Errors.Any())
                throw new Exception(identityResult.Errors.ToArray().ToString());



            _logger.LogInformation("User {UserId} has been successfully registered", user.Id);

            var result = _mapper.Map<User, UserResponse>(user);

            return result;
        }

        private Expression<Func<Clinic, bool>>? CreateFilterPredicate(ClinicAllRequest request)
        {
            Expression<Func<Clinic, bool>>? predicate = null;

            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                Expression<Func<Clinic, bool>> searchStringExpression = x =>
                    x.Name != null && x.Description.Contains(request.SearchString) ||
                    x.Name.Contains(request.SearchString);

                predicate = ExpressionsHelper.And(predicate, searchStringExpression);
            }

            if (request.StartDate.HasValue && request.EndDate.HasValue && request.StartDate < request.EndDate)
            {
                Expression<Func<Clinic, bool>> dateExpression = x => x.CreatedDate > request.StartDate.Value
                                                                     && x.CreatedDate < request.EndDate.Value;
                predicate = ExpressionsHelper.And(predicate, dateExpression);
            }

            return predicate;
        }
    }
}

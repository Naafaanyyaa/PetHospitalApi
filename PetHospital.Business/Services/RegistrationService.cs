using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHospital.Business.Exceptions;
using PetHospital.Business.Interfaces;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;
using PetHospital.Data.Entities.Identity;
using PetHospital.Data.Enums;

namespace PetHospital.Business.Services
{
    internal class RegistrationService : IRegistrationService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegistrationService> _logger;

        public RegistrationService(IMapper mapper, UserManager<User> userManager, ILogger<RegistrationService> logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<RegistrationResponse> Registration(RegistrationRequest request)
        {
            var isUserExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email || x.UserName == request.UserName);

            if (isUserExists)
            {
                throw new ValidationException("User with such username or email already exists");
            }

            User user = new User();

            _mapper.Map(request, user);

            var identityResult = await _userManager.CreateAsync(user, request.Password);

            if (identityResult.Errors.Any())
                throw new Exception(identityResult.Errors.ToArray().ToString());

            identityResult = await _userManager.AddToRolesAsync(user, new List<string>
            {
                CustomRoles.UserRole,
                request.Role switch
                {
                    RoleEnum.Admin => CustomRoles.AdminRole,
                    RoleEnum.Doctor => CustomRoles.DoctorRole,
                    RoleEnum.HospitalHost => CustomRoles.HospitalHost,
                    _ => CustomRoles.UserRole
                }
            });

            if (identityResult.Errors.Any())
                throw new Exception(identityResult.Errors.ToArray().ToString());

            _logger.LogInformation("User {UserId} has been successfully registered", user.Id);

            var result = _mapper.Map<User, RegistrationResponse>(user);

            return result;
        }
    }
}

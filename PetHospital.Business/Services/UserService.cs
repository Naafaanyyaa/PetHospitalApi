using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetHospital.Business.Exceptions;
using PetHospital.Business.Interfaces;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;
using PetHospital.Data.Entities.Identity;
using PetHospital.Data.Interfaces;

namespace PetHospital.Business.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IContactsRepository _contactsRepository;

        public UserService(ILogger<UserService> logger, IMapper mapper, UserManager<User> userManager, IContactsRepository contactsRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _contactsRepository = contactsRepository;
        }

        public async Task<UserResponse> GetUserInfoAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            var contacts = await _contactsRepository.GetByIdAsync(userId);

            var result = _mapper.Map<User, UserResponse>(user);

            return result;
        }

        public async Task<UserResponse> UpdateAsync(string userId, UserRequest userRequest)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            user.FirstName = userRequest.FirstName;
            user.LastName = userRequest.LastName;
            user.Email = userRequest.Email;
            user.UserName = userRequest.UserName;

            await _userManager.UpdateAsync(user);

            var result = _mapper.Map<User, UserResponse>(user);

            return result;
        }

        public async Task DeleteAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            var identityResult = await _userManager.DeleteAsync(user);

            if (identityResult.Errors.Any())
            {
                throw new Exception(identityResult.Errors.ToArray().ToString());
            }
        }

        public async Task<UserResponse> ChangePasswordAsync(string userId, ChangePasswordRequest passwordRequest)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            if (!await _userManager.CheckPasswordAsync(user, passwordRequest.OldPassword))
            {
                throw new ValidationException("Password doesn't match");
            }

            await _userManager.ChangePasswordAsync(user, passwordRequest.OldPassword, passwordRequest.NewPassword);

            var result = _mapper.Map<User, UserResponse>(user);

            return result;
        }
    }
}

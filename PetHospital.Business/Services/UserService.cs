using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                throw new NotFoundException(nameof(user), userId);
            }

            var contacts = await _contactsRepository.GetByIdAsync(userId);

            var result = _mapper.Map<User, UserResponse>(user);

            return result;
        }

        public Task<UserResponse> UpdateAsync(UserRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> DeleteAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> ChangePasswordAsync(ChangePasswordRequest passwordRequest)
        {
            throw new NotImplementedException();
        }
    }
}

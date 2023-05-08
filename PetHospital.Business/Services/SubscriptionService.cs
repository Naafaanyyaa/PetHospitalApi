

using Microsoft.AspNetCore.Identity;
using PetHospital.Business.Exceptions;
using PetHospital.Business.Interfaces;
using PetHospital.Data.Entities;
using PetHospital.Data.Entities.Identity;
using PetHospital.Data.Enums;
using PetHospital.Data.Interfaces;
using System.Data;

namespace PetHospital.Business.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly UserManager<User> _userManager;
        public SubscriptionService(ISubscriptionRepository subscriptionRepository, UserManager<User> userManager)
        {
            _subscriptionRepository = subscriptionRepository;
            _userManager = userManager;
        }

        public async Task<Subscription> AddSubscriptionAsync(string userId, SubscriptionType subscriptionType)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            var usersWithRole = await _userManager.GetUsersInRoleAsync(CustomRoles.HospitalHost);
            
            if (usersWithRole.Contains(user))
            {
                throw new ValidationException($"User with such id has already been Hospital Host");
            }

            var subscription = new Subscription()
            {
                SubscriptionType = subscriptionType,
                CreatedDate = DateTime.Now,
                User = user
            };

            var result = await _subscriptionRepository.AddAsync(subscription);

            await _userManager.AddToRolesAsync(user, new List<string>
            {
                 CustomRoles.HospitalHost
            });

            return result;
        }
    }
}
